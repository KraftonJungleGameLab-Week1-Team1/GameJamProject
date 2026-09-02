using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
	public List<Item> ItemList;
	public List<ItemGroup> ItemGroupList;
	public ItemGroup ItemGroupPrefab;
	public GameObject ExpressionPanel;

    public ScrollRect HorizontalScrollRect;

    public Vector2 OriginPosition;

    //Expression에 넣은 아이템의 수
	public int AddCount = 0;
    //맨 마지막 Itemgroup을 제외한 수들의 연산 결과
	public int LastNum = 0;
	
	void Start()
	{
        OriginPosition = HorizontalScrollRect.content.anchoredPosition;
    }

	void Update()
	{

    }

    public void SetScrollPosition(ItemGroup itemGroup)
    {
        int groupIndex = ItemGroupList.IndexOf(itemGroup);
        float spacing = HorizontalScrollRect.content.GetComponent<HorizontalLayoutGroup>().spacing;
        //HorizontalScrollRect.content.anchoredPosition -= new Vector2(itemGroup.GetComponent<RectTransform>().rect.width, 0);
        ResetScrollPosition();
        Vector2 newPosition = Vector2.zero;
        for(int i = 0; i < groupIndex - 2; ++i)
        {
            newPosition += new Vector2(ItemGroupList[i].GetComponent<RectTransform>().rect.width / 2, 0) + new Vector2(spacing, 0);
        }
        HorizontalScrollRect.content.anchoredPosition -= newPosition;
    }

    public void ResetScrollPosition()
    { 
        HorizontalScrollRect.content.anchoredPosition = OriginPosition;
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

            if (AddCount >= 11)
            {
                SetScrollPosition(ItemGroupList[ItemGroupList.Count - 1]);
            }
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

        itemObject.transform.localScale = Vector3.one;


        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ExpressionPanel.transform);


    }

    public void ClearExpression()
	{
		LastNum = 0;
		AddCount = 0;

        foreach(Item item in ItemList)
        {
            Destroy(item.gameObject);
        }

        foreach (ItemGroup itemGroup in ItemGroupList)
        {
            Destroy(itemGroup.gameObject);
        }
        ItemList.Clear();
        ItemGroupList.Clear();
        GameManager.Instance.TargetInputType = EItemType.Number;
        ResetScrollPosition();
    }

    public bool ValidateExpression()
    {
        if (ItemList[ItemList.Count - 1].ItemData.Type == EItemType.Number)
        { return true; }
        else
        { return false; }
    }

    public IEnumerator CalculateExpression()
    {
        int result = ItemList[0].ItemData.NumberValue;
        foreach (ItemGroup itemGroup in ItemGroupList)
        {
            SetScrollPosition(itemGroup);
            yield return StartCoroutine(itemGroup.PopUpResult(result));

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
        LastNum = result;

        ResetScrollPosition();
    }
}
