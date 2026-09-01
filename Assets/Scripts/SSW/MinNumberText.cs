using UnityEngine;
using TMPro;

public class MinNumberText : MonoBehaviour
{
    public Transform enemyHead;
    public TMP_Text enemyHealthText;

    private void Start()
    {
        
    }

    private void Update()
    {

        
        if (enemyHead != null && enemyHealthText != null)
        {

            
            Vector3 offset = new Vector3(0, 1f, 0);

            enemyHealthText.transform.position = enemyHead.position + offset;

            enemyHealthText.text = GameManager.Instance.CurrentRoundData.MinNumber.ToString(); 
        }
    }
}
