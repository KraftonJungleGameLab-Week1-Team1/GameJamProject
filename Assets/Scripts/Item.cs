using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum EItemType
{
    Number,
    Operator
}

public class ItemData
{
    public EItemType Type;
    public string Value;
}