using TMPro;
using UnityEngine;

public class PlayerAttackPoint : MonoBehaviour
{
    public TMP_Text text;

    void Update()
    {
        text.text = GameManager.Instance.LastPreResult.ToString();
    }
}
