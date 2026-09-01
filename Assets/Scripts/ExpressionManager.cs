using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
	public List<Item> ItemList;
	public List<ItemGroup> ItemGroupList;
	public ItemGroup ItemGroupPrefab;
	public GameObject ExpressionPanel;

    public ScrollRect HorizontalScrollRect;

    //Expression에 넣은 아이템의 수
	public int AddCount = 0;
    //맨 마지막 Itemgroup을 제외한 수들의 연산 결과
	public int LastNum = 0;
	
	void Start()
	{
 
    }

	void Update()
	{
		
	}

    public void SetScrollPosition()
    {
        HorizontalScrollRect.content.transform.localPosition = Vector3.zero;
    }

    public void AddItem(ItemData itemData)
    { 

		Item itemObject = ItemFactory.Instance.Instantiate(itemData);
		itemObject.transform.SetParent(ExpressionPanel.transform);
        ItemList.Add(itemObject);
		++AddCount;


        if (AddCount == 1)
        {
            ItemGroup itemGroup = Instantiate(ItemGroupPrefab, ExpressionPanel.transform);
            ItemGroupList.Add(itemGroup);
            ItemList[0].transform.SetParent(itemGroup.transform);
            LastNum = itemData.NumberValue;
        }
        else if (AddCount % 2 == 1)
        {
            ItemList[ItemList.Count - 1].transform.SetParent(ItemGroupList[ItemGroupList.Count - 1].transform);
            ItemGroupList[ItemGroupList.Count - 1].SetItemGroup(ItemList[ItemList.Count - 2], ItemList[ItemList.Count - 1]);
            ItemGroupList[ItemGroupList.Count - 1].SetBoxColor();

            StartCoroutine(ItemGroupList[ItemGroupList.Count - 1].PopUpPreResult(LastNum));

            switch (ItemGroupList[ItemGroupList.Count - 1].OperatorItem.ItemData.OperatorType)
            {
                case EItemOperatorType.Plus:
                    {
                        LastNum += ItemGroupList[ItemGroupList.Count - 1].ValueItem.ItemData.NumberValue;
                        break;
                    }
                case EItemOperatorType.Minus:
                    {
                        LastNum -= ItemGroupList[ItemGroupList.Count - 1].ValueItem.ItemData.NumberValue;
                        break;
                    }
                case EItemOperatorType.Multiply:
                    {
                        LastNum *= ItemGroupList[ItemGroupList.Count - 1].ValueItem.ItemData.NumberValue;
                        break;
                    }
            }
        }
        else if(AddCount == 2)
        {
            ItemList[ItemList.Count - 1].transform.SetParent(ItemGroupList[ItemGroupList.Count - 1].transform);
        }
        else
        {
            ItemGroup itemGroup = Instantiate(ItemGroupPrefab, ExpressionPanel.transform);
            ItemGroupList.Add(itemGroup);
            ItemList[ItemList.Count - 1].transform.SetParent(itemGroup.transform);
        }


        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ExpressionPanel.transform);
        SetScrollPosition();




    }


	public void GroupItems()
    {
        ItemGroup itemGroup = Instantiate(ItemGroupPrefab, ExpressionPanel.transform);
        itemGroup.SetItemGroup(ItemList[ItemList.Count - 2], ItemList[ItemList.Count - 1]);
        ItemGroupList.Add(itemGroup);
        ItemList[ItemList.Count - 2].transform.SetParent(itemGroup.transform);
        ItemList[ItemList.Count - 1].transform.SetParent(itemGroup.transform);
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ExpressionPanel.transform);

        StartCoroutine(itemGroup.PopUpPreResult(LastNum));

        switch (itemGroup.OperatorItem.ItemData.OperatorType)
        {
            case EItemOperatorType.Plus:
                {
                    LastNum += itemGroup.ValueItem.ItemData.NumberValue;
                    break;
                }
            case EItemOperatorType.Minus:
                {
                    LastNum -= itemGroup.ValueItem.ItemData.NumberValue;
                    break;
                }
            case EItemOperatorType.Multiply:
                {
                    LastNum *= itemGroup.ValueItem.ItemData.NumberValue;
                    break;
                }
        }
    }

    public void GroupFirstItems()
    {
        ItemGroup itemGroup = Instantiate(ItemGroupPrefab, ExpressionPanel.transform);
        itemGroup.SetItemGroup(ItemList[1], ItemList[2]);
        ItemGroupList.Add(itemGroup);
        ItemList[0].transform.SetParent(itemGroup.transform);
        ItemList[1].transform.SetParent(itemGroup.transform);
        ItemList[2].transform.SetParent(itemGroup.transform);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ExpressionPanel.transform);

        StartCoroutine(itemGroup.PopUpPreResult(LastNum));

        switch (itemGroup.OperatorItem.ItemData.OperatorType)
        {
            case EItemOperatorType.Plus:
                {
                    LastNum += itemGroup.ValueItem.ItemData.NumberValue;
                    break;
                }
            case EItemOperatorType.Minus:
                {
                    LastNum -= itemGroup.ValueItem.ItemData.NumberValue;
                    break;
                }
            case EItemOperatorType.Multiply:
                {
                    LastNum *= itemGroup.ValueItem.ItemData.NumberValue;
                    break;
                }
        }
    }

    

    public void ClearExpression()
	{
		LastNum = 0;
		AddCount = 0;

    }

	public void CompleteExpression()
	{
        StartCoroutine(CalculateExpression());
    }

    public IEnumerator CalculateExpression()
    {
        int result = ItemList[0].ItemData.NumberValue;
        foreach (ItemGroup itemGroup in ItemGroupList)
        {
            yield return StartCoroutine(itemGroup.PopUpPreResult(result));

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
    }
}
