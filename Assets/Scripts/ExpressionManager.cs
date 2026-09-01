using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
	public List<Item> ItemList;
	public List<ItemGroup> ItemGroupList;
	public ItemGroup ItemGroupPrefab;
	public GameObject ExpressionPanel;
	public int AddCount = 0;
	
	void Start()
	{
 
    }

	void Update()
	{
		
	}

    public void AddItem(ItemData itemData)
    {

		Item itemObject = ItemFactory.Instance.Instantiate(itemData);
		itemObject.transform.SetParent(ExpressionPanel.transform);
        ItemList.Add(itemObject);
		++AddCount;

		if (AddCount % 2 == 1 && AddCount != 1)
		{
			MergeItem();
		}
    }


	public void MergeItem()
    {
        ItemGroup itemGroup = Instantiate(ItemGroupPrefab, ExpressionPanel.transform);
        itemGroup.SetItemGroup(ItemList[ItemList.Count - 2], ItemList[ItemList.Count - 1]);
        ItemGroupList.Add(itemGroup);
        ItemList[ItemList.Count - 2].transform.SetParent(itemGroup.transform);
        ItemList[ItemList.Count - 1].transform.SetParent(itemGroup.transform);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ExpressionPanel.transform);


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
					result += itemGroup.ValueItem.ItemData.NumberValue;
					break;
				}
				case EItemOperatorType.Minus:
				{
                    result -= itemGroup.ValueItem.ItemData.NumberValue;
                    break;
				}
				case EItemOperatorType.Multiply:
				{
					result *= itemGroup.ValueItem.ItemData.NumberValue;
                    break;
				}
			}
        }
        Debug.Log(result);
    }
}
