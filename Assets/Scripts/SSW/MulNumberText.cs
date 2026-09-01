using UnityEngine;
using TMPro;

public class MulNumberText : MonoBehaviour
{
    public TMP_Text multiplierText;

    private void Start()
    {
        
    }

    private void Update()
    {
        multiplierText.text = GameManager.Instance.CurrentRoundData.MulNumber.ToString(); 
    }
}
