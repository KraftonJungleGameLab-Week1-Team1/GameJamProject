using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
}

public enum EItemType
{
    None,
    Number,
    Operator
}

public enum EItemOperatorType
{
    None,
    Plus,
    Minus,
    Multiply
}

[Serializable]
public class ItemData
{
    public EItemType Type;

    // Number일 경우
    public int NumberValue;

    // Operator일 경우
    public EItemOperatorType OperatorType;
}