using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text ComboText;

    //Expression에 넣은 아이템의 수
	public int AddCount = 0;
    //맨 마지막 Itemgroup을 제외한 수들의 연산 결과
	public int LastNum = 0;
	public int LastCombo = 0;
    public int CurrentCombo = 0;
    public float OriginFontSize;

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

            // 중간 결과 값 캐시


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

        GameManager.Instance.LastPreResult = LastNum;
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

    public IEnumerator PopUpComboText()
    {
        ComboText.gameObject.SetActive(true);
        ++CurrentCombo;
        ComboText.text = "x" + CurrentCombo;

        OriginFontSize = ComboText.fontSize;
        float flag = 0f;
        ComboText.fontSize = flag;
        while (flag < 1f)
        {
            flag += Time.deltaTime * 7f;
            yield return null;
            ComboText.fontSize = OriginFontSize * flag;
        }
        yield return new WaitForSeconds(0.15f);
        while (flag > 0f)
        {
            flag -= Time.deltaTime * 5f;
            yield return null;
            ComboText.fontSize = OriginFontSize * flag;
        }

        ComboText.gameObject.SetActive(false);
        ComboText.fontSize = OriginFontSize;
    }

    public IEnumerator CalculateExpression()
    {
        int result = ItemList[0].ItemData.NumberValue;
        int count = 0;

        StartCoroutine(AudioManager.Instance.PlaySequence(ItemGroupList.Count));
        foreach (ItemGroup itemGroup in ItemGroupList)
        {
            SetScrollPosition(itemGroup);
            StartCoroutine(PopUpComboText());
            yield return StartCoroutine(itemGroup.PopUpResult(result));
            count++;
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
        LastCombo = CurrentCombo;

        CurrentCombo = 0;
        ResetScrollPosition();
    }
}
