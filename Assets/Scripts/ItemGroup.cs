using UnityEngine;
using TMPro;

public class ItemGroup : MonoBehaviour
{
    public Item OperatorItem;
    public Item ValueItem;


    public TMP_Text numberText;

    


    public ItemGroup(Item operatorItem, Item valueItem)
    {
        OperatorItem = operatorItem;
        ValueItem = valueItem;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUpPreResult(int result)
    {
        //기호별로 구분하여 수식 계산
        switch (OperatorItem.ItemData.OperatorType)
        {
            case EItemOperatorType.Plus:
            {
                result += OperatorItem.ItemData.NumberValue;
                break;
            }
            case EItemOperatorType.Minus:
            {
                result -= OperatorItem.ItemData.NumberValue;
                    break;
            }
            case EItemOperatorType.Multiply:
            {
                result *= OperatorItem.ItemData.NumberValue;
                    break;
            }
        }

        numberText.text = result.ToString();
    }
}
