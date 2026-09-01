using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemListParentTr;

    List<Item> itemList = new List<Item>();

    public void SetupItemList(List<ItemData> dataList)
    {
        // 생성
        foreach (ItemData data in dataList)
        {
            Item newItem = ItemFactory.Instance.Instantiate(data);
            newItem.transform.SetParent(ItemListParentTr);
        }

        // 보여주기

    }
}
