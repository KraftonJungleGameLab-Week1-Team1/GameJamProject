using TMPro;
using UnityEngine;

public class PlayerAttackPoint : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GameManager.Instance.LastPreResult.ToString();
    }
}
