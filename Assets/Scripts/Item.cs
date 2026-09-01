using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData ItemData;

    [SerializeField] Button button;
    [SerializeField] Animator animator;

    public void SetClickEvent(UnityAction onClick)
    {
        if (onClick != null)
        {
            button.onClick.AddListener(onClick);
        }
    }

    public void SetAnimTrigger(string param)
    {
        animator.SetTrigger(param);
    }
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