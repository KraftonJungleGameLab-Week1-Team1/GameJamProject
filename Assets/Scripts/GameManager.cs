using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
    public int LastResult; // 가장 최근에 수식 성공했을 때의 결과 값

    [Header("References")]
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] BattleManager battleManager;
    [SerializeField] ExpressionManager expressionManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] EarnedScoreOverlay earnedScoreOverlay;

    [Header("UI")]
    [SerializeField] TMP_Text descriptionText;

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
        IsRunningRound = true;
        IsPause = true;

        TargetInputType = EItemType.Number;

        RoundData newRoundData = new RoundData();
    
        // 조건 설정
        if (RoundCount == 1)
        {
            newRoundData.MinNumber = UnityEngine.Random.Range(15, 21) * 4; // 15 ~ 20
        }
        else
        {
            newRoundData.MinNumber = Mathf.FloorToInt(UnityEngine.Random.Range(15, 21) + Mathf.Pow((float) (4 + (1.3f * RoundCount - 1)) , 2));
        }
        newRoundData.MulNumber = UnityEngine.Random.Range(3, 10); // 3 ~ 9

        descriptionText.text = $"{newRoundData.MinNumber}보다 높은 {newRoundData.MulNumber}의 배수 숫자를 만드시오.";

        // 인벤토리 비우기
        inventoryManager.DeleteItemList();

        // 인벤토리 아이템 설정
        // 숫자 32개
        for (int i = 0; i < 32; i++)
        {
            ItemData newItemData = new ItemData();

            newItemData.Type = EItemType.Number;
            int newNumber = UnityEngine.Random.Range(1, 10); // 1 ~ 9

            // MulNumber 로 나눠지는 숫자가 등장하는 것을 방지
            while (newNumber % newRoundData.MulNumber == 0)
            {
                newNumber = UnityEngine.Random.Range(1, 10);
            }
            newItemData.NumberValue = newNumber;

            newRoundData.ItemDataList.Add(newItemData);
        }
        // 연산자 8개
        for (int i = 0; i < 8; i++)
        {
            ItemData newItemData = new ItemData();

            newItemData.Type = EItemType.Operator;
            int randomOperatorType = UnityEngine.Random.Range(0, 3);
            if (randomOperatorType == 0)
            {
                newItemData.OperatorType = EItemOperatorType.Plus;
            }
            else if (randomOperatorType == 1)
            {
                newItemData.OperatorType = EItemOperatorType.Minus;
            }
            else if (randomOperatorType == 2)
            {
                newItemData.OperatorType = EItemOperatorType.Multiply;
            }

            newRoundData.ItemDataList.Add(newItemData);
        }

        var temp = newRoundData.ItemDataList.OrderBy(item => Guid.NewGuid()).ToList();
        newRoundData.ItemDataList = temp;

        CurrentRoundData = newRoundData;

        // 전투 등장 연출
        battleManager.SpawnEnemy();

        // 인벤토리 아이템 등장 연출
        inventoryManager.SetupItemList(CurrentRoundData.ItemDataList);
        inventoryManager.UpdateHighlight();

        IsPause = false;
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

        // 수식 계산 연출
        yield return expressionManager.CalculateExpression();

        IsPause = false;

        // 수식 성공 시
        int result = expressionManager.LastNum;
        if (result % CurrentRoundData.MulNumber == 0
            && result > CurrentRoundData.MinNumber)
        {
            // 점수 누적
            TotalScore += result;
            earnedScoreOverlay.ShowEarnedScore(TotalScore);
            RoundClear();

            LastResult = result;
        }
        else
        {
            // 수식 실패 시
            inventoryManager.RevisibleItemList();
        }
        // 수식 비우기
        expressionManager.ClearExpression();

        // 하이라이트
        inventoryManager.UpdateHighlight();
    }

    // 수식 완성
    public void CompleteExpression()
    {
        if (expressionManager.ValidateExpression())
            StartCoroutine(IECompleteExpression());
    }

    // 하나의 라운드 성공
    public void RoundClear()
    {
        // 전투 성공 연출
        battleManager.Win();
        PlayerHeal();
        // 다음 라운드
        RoundCount++;
        StartRound();
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
        healGuage = 20 + RoundCount * 3;
        if (healGuage > 50)
        {
            healGuage = 50;
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