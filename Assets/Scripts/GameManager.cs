using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("References")]
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] BattleManager battleManager;
    [SerializeField] ExpressionManager expressionManager;

    bool isRunningRound = false;
    bool isPause = false;

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
        CurrentHP = MaxHP;

        // 플레이어 생성
        battleManager.SpawnPlayer();

        StartRound();
    }

    // 하나의 라운드 시작
    public void StartRound()
    {
        isRunningRound = true;
        isPause = true;

        TargetInputType = EItemType.Number;

        RoundData newRoundData = new RoundData();

        // 조건 설정
        newRoundData.MinNumber = UnityEngine.Random.Range(15, 21); // 15 ~ 20
        newRoundData.MulNumber = UnityEngine.Random.Range(3, 10); // 3 ~ 9

        // 인벤토리 비우기
        inventoryManager.DeleteItemList();

        // 인벤토리 아이템 설정
        for (int i = 0; i < 40; i++)
        {
            ItemData newItemData = new ItemData();
            int randomItemType = UnityEngine.Random.Range(0, 2);
            if (randomItemType == 0)
            {
                newItemData.Type = EItemType.Number;
                newItemData.NumberValue = UnityEngine.Random.Range(1, 10); // 1 ~ 9
            }
            else
            {
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
            }

            newRoundData.ItemDataList.Add(newItemData);
        }

        CurrentRoundData = newRoundData;

        // 전투 등장 연출
        battleManager.SpawnEnemy();

        // 인벤토리 아이템 등장 연출
        inventoryManager.SetupItemList(CurrentRoundData.ItemDataList);
        inventoryManager.UpdateHighlight();

        isPause = false;
    }

    private void Update()
    {
        // 게임 중이면
        // 게이지 줄어들기

        if (isRunningRound)
        {
            if (!isPause)
            {
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
        // 수식 계산 연출
        yield return expressionManager.CalculateExpression();

        // 수식 성공 시 RoundClear()
        int result = expressionManager.LastNum;

        if(result % CurrentRoundData.MulNumber == 0
            && result > CurrentRoundData.MinNumber)
        {
            RoundClear();
        }
        else
        {
            // 수식 실패 시 인벤토리 원상 복구
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
        StartCoroutine(IECompleteExpression());
    }

    // 하나의 라운드 성공
    public void RoundClear()
    {
        // 전투 성공 연출
        battleManager.Win();

        // 다음 라운드
        RoundCount++;
        StartRound();
    }

    // 하나의 라운드 실패
    public void RoundFail()
    {
        // 전투 실패 연출
        battleManager.Lose();

        isRunningRound = false;

        EndGame();
    }

    // 게임 끝
    public void EndGame()
    {
        // 결과 화면 UI 보여주기
    }
}

[Serializable]
public class RoundData
{
    public float MinNumber; // 최소 숫자 조건
    public float MulNumber; // 배수 숫자 조건

    public List<ItemData> ItemDataList = new List<ItemData>(); // 아이템
}