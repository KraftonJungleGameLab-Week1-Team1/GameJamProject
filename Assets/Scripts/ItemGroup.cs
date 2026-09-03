using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGroup : MonoBehaviour
{
    public Item OperatorItem;
    public Item ValueItem;
    public TMP_Text NumberText;
    public Image GroupBox;

    public Color PlusColor;
    public Color MinusColor;
    public Color MultiplyColor;

    public float OriginFontSize;

    private float sizeUpSpeed;
    private float sizeDownSpeed;

    public void SetItemGroup(Item operatorItem, Item valueItem)
    {
        OperatorItem = operatorItem;
        ValueItem = valueItem;
        SetBoxColor();
    }

    public void SetBoxColor()
    {
        if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Plus)
        {
            GroupBox.color = PlusColor;
        }
        else if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Minus)
        {
            GroupBox.color = MinusColor;
        }
        else if (OperatorItem.ItemData.OperatorType == EItemOperatorType.Multiply)
        {
            GroupBox.color = MultiplyColor;
        }
    }

    public IEnumerator PopUpResult(int result)
    {
        NumberText.gameObject.SetActive(true);
        OriginFontSize = NumberText.fontSize;
        NumberText.alpha = 1f;
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
        float flag = 0f;
        NumberText.fontSize = flag;
        while (flag < 1f)
        {
            flag += Time.deltaTime * 7f;
            yield return null;
            NumberText.fontSize = OriginFontSize * flag;
        }
        yield return new WaitForSeconds(0.15f);

        while (flag > 0f)
        {
            flag -= Time.deltaTime * 5f;
            yield return null;
            NumberText.fontSize = OriginFontSize * flag;
            NumberText.alpha = flag * 0.6f;
        }

        NumberText.gameObject.SetActive(false);
        NumberText.fontSize = OriginFontSize;
    }

    public IEnumerator PopUpPreResult(int result)
    {
        NumberText.gameObject.SetActive(true);
        OriginFontSize = NumberText.fontSize;
        NumberText.alpha = 1f;
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
        float flag = 0f;
        NumberText.fontSize = flag;
        while (flag < 1f)
        {
            flag += Time.deltaTime * 6f;
            yield return null;
            NumberText.fontSize = OriginFontSize * flag;
        }
        yield return new WaitForSeconds(0.2f);

        while (flag > 0f)
        {
            flag -= Time.deltaTime * 6f;
            yield return null;
            NumberText.fontSize = OriginFontSize * flag;
            NumberText.alpha = flag * 0.6f;
        }

        NumberText.gameObject.SetActive(false);
        NumberText.fontSize = OriginFontSize;

        if(GameManager.Instance.CurrentRoundData.TargetNumber == result)
        { GameManager.Instance.CompleteExpression(); }
    }
}
