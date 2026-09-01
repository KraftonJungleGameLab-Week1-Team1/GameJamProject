using UnityEngine;
using UnityEngine.Events;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory Instance;

    [Header("순서 지켜줘야 함 1 ~ 9")]
    public Item[] ItemNumberPrefabs;

    public Item ItemPlusPrefab;
    public Item ItemMinusPrefab;
    public Item ItemMultiplyPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public Item Instantiate(ItemData data, UnityAction onClick = null)
    {
        Item item = null;

        if (data.Type == EItemType.Number)
        {
            int index = data.NumberValue - 1;
            item = Instantiate(ItemNumberPrefabs[index]);
        }
        else if (data.Type == EItemType.Operator)
        {
            if (data.OperatorType == EItemOperatorType.Plus)
            {
                item = Instantiate(ItemPlusPrefab);
            }
            else if (data.OperatorType == EItemOperatorType.Minus)
            {
                item = Instantiate(ItemMinusPrefab);
            }
            else if (data.OperatorType == EItemOperatorType.Multiply)
            {
                item = Instantiate(ItemMultiplyPrefab);
            }
        }

        item.SetClickEvent(onClick);

        return item;
    }
}
