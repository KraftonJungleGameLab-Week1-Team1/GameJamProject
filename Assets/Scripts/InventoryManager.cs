using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemListParentTr;

    List<Item> itemList = new List<Item>();

    [SerializeField] ExpressionManager expressionManager;
    [SerializeField] GridLayoutGroup gridLayoutGroup;

    public void SetupItemList(List<ItemData> dataList)
    {
        // 생성
        foreach (ItemData data in dataList)
        {
            Item newItem = ItemFactory.Instance.Instantiate(data);
            newItem.transform.SetParent(ItemListParentTr);

            newItem.SetClickEvent(
                () =>
                {
                    // 선택 가능한 타입이어야지 클릭 가능
                    if (GameManager.Instance.TargetInputType == data.Type)
                    {
                        expressionManager.AddItem(data);
                        Debug.Log($"아이템 클릭 ({data.Type.ToString()} / {data.NumberValue} / {data.OperatorType.ToString()})");

                        newItem.SetVisible(false);

                        // 선택 가능한 거 바꿔주기
                        if (GameManager.Instance.TargetInputType == EItemType.Number)
                        {
                            GameManager.Instance.TargetInputType = EItemType.Operator;
                        }
                        else if (GameManager.Instance.TargetInputType == EItemType.Operator)
                        {
                            GameManager.Instance.TargetInputType = EItemType.Number;
                        }

                        UpdateHighlight();
                    }
                });

            itemList.Add(newItem);
        }

        // 보여주기
        //StartCoroutine(ShowCoroutine());
    }

    public void RevisibleItemList()
    {
        foreach (Item item in itemList)
        {
            item.SetVisible(true);
        }
    }

    public void UpdateHighlight()
    {
        foreach (Item item in itemList)
        {
            if (item.ItemData.Type == GameManager.Instance.TargetInputType)
            {
                item.SetHighlight(false);
            }
            else
            {
                item.SetHighlight(true);
            }
        }
    }

    //IEnumerator ShowCoroutine()
    //{
    //    // 순차적으로 보여주기
    //    for (int i = 0; i < 5; i++)
    //    {
    //        // 한 줄씩 딜레이
    //        ShowRow(i);

    //        yield return new WaitForSeconds(0.05f);
    //    }

    //    // 한 줄 보여주기
    //    IEnumerator ShowRow(int rowIndex)
    //    {
    //        int columnCount = gridLayoutGroup.constraintCount;

    //        int start = columnCount * rowIndex;
    //        int end = start + columnCount;
    //        for (int i = start; i < end; i++)
    //        {
    //            itemList[i].SetAnimTrigger("Show");
    //            yield return new WaitForSeconds(0.05f);
    //        }
    //    }
    //}

    public void DeleteItemList()
    {
        // 지우기
        foreach (Item item in itemList)
        {
            item.SetClickEvent(null);
            Destroy(item);
        }

        itemList.Clear();
    }
}
