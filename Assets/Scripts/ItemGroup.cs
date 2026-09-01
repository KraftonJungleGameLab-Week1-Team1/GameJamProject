using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemGroup : MonoBehaviour
{
    public Item OperatorItem;
    public Item ValueItem;


    public TMP_Text NumberText;

      

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemGroup(Item operatorItem, Item valueItem)
    {
        OperatorItem = operatorItem;
        ValueItem = valueItem;
    }

    public void PopUpPreResult(int result)
    {
        //기호별로 구분하여 수식 계산
        if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Plus)
        {
            result += ValueItem.ItemData.NumberValue;
        }
        else if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Minus)
        {
            result -= ValueItem.ItemData.NumberValue;
        }
        else if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Multiply)
        {
            result *= ValueItem.ItemData.NumberValue;
        }

        NumberText.text = result.ToString();
    }
}
