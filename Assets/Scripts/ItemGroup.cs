using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemGroup : MonoBehaviour
{
    public Item OperatorItem;
    public Item ValueItem;
    public TMP_Text NumberText;
    public Image GroupBox;

      

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

        if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Plus)
        {
            GroupBox.color = Color.red;
        }
        else if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Minus)
        {
            GroupBox.color = Color.green;
        }
        else if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Multiply)
        {
            GroupBox.color = Color.blue;
        }
    }

    public IEnumerator PopUpPreResult(int result)
    {
        NumberText.gameObject.SetActive(true);
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
        yield return new WaitForSeconds(0.5f);
        NumberText.gameObject.SetActive(false);


    }
}
