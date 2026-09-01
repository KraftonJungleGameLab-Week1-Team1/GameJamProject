using UnityEngine;
using System.Collections.Generic;

public class ExpressionManager : MonoBehaviour
{
	public List<Item> ItemList;
	public List<ItemGroup> ItemGroupList;
	public ItemGroup ItemGroupPrefab;
	public GameObject ExpressionPanel;
	
	void Start()
	{
 
    }

	void Update()
	{
		
	}

    public void AddItem(ItemData itemData)
    {
        ItemList.Add(ItemFactory.Instance.Instantiate(itemData));
    }


	public void MergeItem()
    {
        ItemList[0].transform.SetParent(ExpressionPanel.transform);
        for (int i = 1; i < ItemList.Count; i = i + 2)
		{
            ItemGroup itemGroup = Instantiate(ItemGroupPrefab, ExpressionPanel.transform);
            itemGroup.SetItemGroup(ItemList[i], ItemList[i + 1]);
            ItemGroupList.Add(itemGroup);

            ItemList[i].transform.SetParent(itemGroup.transform);
            ItemList[i + 1].transform.SetParent(itemGroup.transform);
        }
	}

	public void ClearExpression()
	{
	}

	public void CompleteExpression()
	{
		int result = ItemList[0].ItemData.NumberValue;

		foreach (ItemGroup itemGroup in ItemGroupList)
		{
			//루프마다 계산값을 PopUp
			itemGroup.PopUpPreResult(result);

            //기호별로 구분하여 수식 계산
            switch (itemGroup.OperatorItem.ItemData.OperatorType)
			{
				case EItemOperatorType.Plus:
				{
					result += itemGroup.OperatorItem.ItemData.NumberValue;
					break;
				}
				case EItemOperatorType.Minus:
				{
                    result -= itemGroup.OperatorItem.ItemData.NumberValue;
                    break;
				}
				case EItemOperatorType.Multiply:
				{
					result *= itemGroup.OperatorItem.ItemData.NumberValue;
                    break;
				}
			}

        }
	}
}
