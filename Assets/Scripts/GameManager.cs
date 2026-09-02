using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Settings")]
    public float MaxHP = 100;
    public float DecreaseSpeed = 0.3f;

    [Header("InGame")]
    public int RoundCount;
    public RoundData CurrentRoundData;
    public float CurrentHP;
    public EItemType TargetInputType = EItemType.Number;
    public int TotalScore;
    public float SurvivalTime;
    public bool IsRunningRound = false;
    public bool IsPause = false;
    public int LastPreResult;
    public int LastResult; // 가장 최근에 수식 성공했을 때의 결과 값

    [Header("References")]
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] BattleManager battleManager;
    [SerializeField] ExpressionManager expressionManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] EarnedScoreOverlay earnedScoreOverlay;

    [Header("UI")]
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] UnityEngine.UI.Button resetButton;
    [SerializeField] UnityEngine.UI.Button emptyButton;

    private void Start()
    {
        StartMain();

        /* @@테스트용@@ 에디터 실행 시 바로 시작 */
        StartGame();
    }

    void StartMain()
    {
        // 메인 화면 UI 보여주기
    }

    // 게임 시작
    public void StartGame()
    {
        RoundCount = 1;
        SurvivalTime = 0;
        TotalScore = 0;
        LastPreResult = 0;
        LastResult = 0;
        earnedScoreOverlay.SetTotalScoreText(TotalScore);
        CurrentHP = MaxHP;

        // 플레이어 생성
        battleManager.SpawnPlayer();

        StartRound();
    }

    // 하나의 라운드 시작
    public void StartRound()
    {
        LastPreResult = 0;
        TargetInputType = EItemType.Number;

        RoundData newRoundData = new RoundData();
        CurrentRoundData = newRoundData;

        // 조건 설정
        if (RoundCount == 1)
        {
            newRoundData.MinNumber = UnityEngine.Random.Range(15, 21) * 4; // 15 ~ 20
            //newRoundData.MinNumber = 15;
        }
        else
        {
            newRoundData.MinNumber = Mathf.FloorToInt(UnityEngine.Random.Range(15, 21) *4 + Mathf.Pow((float) (7 + (0.7f * RoundCount - 1)) , 2));
            //newRoundData.MinNumber = 15 + Mathf.Pow((float)(4 + (0.7f * RoundCount - 1)), 2);
        }
        newRoundData.MulNumber = UnityEngine.Random.Range(3, 10); // 3 ~ 9

        descriptionText.text = string.Empty;

        IEnumerator ShowProgress()
        {
            IsRunningRound = true;
            IsPause = true;
            resetButton.interactable = false;
            emptyButton.interactable = false;

            yield return new WaitForSeconds(1);

            // 전투 등장 연출
            battleManager.SpawnEnemy();

            yield return new WaitForSeconds(1);

            // 인벤토리 아이템 생성
            List<ItemData> newItemDataList = inventoryManager.ResetInventory();
            newRoundData.ItemDataList = newItemDataList;
            inventoryManager.UpdateHighlight();

            //yield return new WaitForSeconds(1);

            // 설명
            descriptionText.text = $"{newRoundData.MinNumber}을 만들어라";

            resetButton.interactable = true;
            emptyButton.interactable = true;
            IsPause = false;
        }

        StartCoroutine(ShowProgress());
    }

    private void Update()
    {
        // 게임 중이면
        if (IsRunningRound)
        {
            // 생존 시간 증가
            SurvivalTime += Time.deltaTime;

            // 연출 중이거나 잠시 멈춰야할 때는 멈추기
            if (!IsPause)
            {
                // 게이지 줄어들기
                CurrentHP -= Time.deltaTime * DecreaseSpeed;

                if (CurrentHP <= 0)
                {
                    RoundFail();
                }
            }
        }
    }

    // 수식 완성
    public IEnumerator IECompleteExpression()
    {
        IsPause = true;
        resetButton.interactable = false;
        emptyButton.interactable = false;

        // 수식 계산 연출
        yield return expressionManager.CalculateExpression();

        // 수식 성공 시
        int result = expressionManager.LastNum;
        //if (result % CurrentRoundData.MulNumber == 0
        //    && result >= CurrentRoundData.MinNumber)
        if (result == CurrentRoundData.MinNumber)
        {
            // 콤보 계산
            result *= expressionManager.LastCombo;

            // 점수 누적
            TotalScore += result;
            yield return StartCoroutine(earnedScoreOverlay.ShowEarnedScore(TotalScore));

            expressionManager.ResetComboTextSize();
            RoundClear();

            LastResult = result;
        }
        else
        {
            // 수식 실패 시
            inventoryManager.SetVisibleItemList(true);
        }
        // 수식 비우기
        expressionManager.ClearExpression();

        // 하이라이트
        inventoryManager.UpdateHighlight();
    }

    // 수식 완성
    public void CompleteExpression()
    {
        inventoryManager.SetVisibleItemList(false);

        StartCoroutine(IECompleteExpression());
    }

    // 하나의 라운드 성공
    public void RoundClear()
    {
        IEnumerator ClearProgress()
        {
            // 전투 성공 연출
            battleManager.Win();
            PlayerHeal();

            yield return new WaitForSeconds(2);

            // 다음 라운드
            RoundCount++;
            StartRound();
        }

        StartCoroutine(ClearProgress());
    }

    // 하나의 라운드 실패
    public void RoundFail()
    {
        // 전투 실패 연출
        battleManager.Lose();

        IsRunningRound = false;

        EndGame();
    }

    // 게임 끝
    public void EndGame()
    {
        // 결과 화면 UI 보여주기
        scoreManager.ShowResult(TotalScore, SurvivalTime);
    }

    public void PlayerHeal()
    {
        int healGuage = 0;
        healGuage = 30 + RoundCount * 3;
        if (healGuage > 60)
        {
            healGuage = 60;
        }
        CurrentHP += healGuage;
        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
    }
}

[Serializable]
public class RoundData
{
    public float MinNumber; // 최소 숫자 조건
    public float MulNumber; // 배수 숫자 조건

    public List<ItemData> ItemDataList = new List<ItemData>(); // 아이템
}