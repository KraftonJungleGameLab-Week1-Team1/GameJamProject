using UnityEngine;
using System.Collections.Generic;

public class ExpressionManager : MonoBehaviour
{
	public List<Item> itemList;
	public List<ItemGroup> itemGroupList;
	
	void Start()
	{
	}

	void Update()
	{
		
	}


	public void MergeItem()
	{
		for (int i = 1; i < itemList.Count; i = i + 2)
		{
			itemGroupList.Add(new ItemGroup(itemList[i], itemList[i + 1])); 
		}
	}

	public int CalculateExpression()
	{
		int result = itemList[0].ItemData.NumberValue;

		foreach (ItemGroup itemGroup in itemGroupList)
		{

			//루프마다 계산의 중간값을 PopUp
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

		return result;
	}
}
