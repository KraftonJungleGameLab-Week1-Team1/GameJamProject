using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemListParentTr;

    List<Item> itemList = new List<Item>();

    [SerializeField] ExpressionManager expressionManager;

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
                    expressionManager.AddItem(data);
                    Debug.Log($"아이템 클릭 ({data.Type.ToString()} / {data.NumberValue} / {data.OperatorType.ToString()}");
                });

            itemList.Add(newItem);
        }

        // 보여주기
    }

    public void DeleteItemList()
    {
        // 지우기
        foreach (Item item in itemList)
        {
            Destroy(item);
        }

        itemList.Clear();
    }
}
