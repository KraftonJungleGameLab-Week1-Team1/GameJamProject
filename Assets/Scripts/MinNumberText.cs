using UnityEngine;
using TMPro;

public class MinNumberText : MonoBehaviour
{
    public Transform EnemyHead;
    public TMP_Text enemyHealthText;

    private void Update()
    {
        if (EnemyHead != null && enemyHealthText != null)
        {
            Vector3 offset = new Vector3(0f, 1f, 0f);

            enemyHealthText.transform.position = EnemyHead.position + offset;

            enemyHealthText.text = GameManager.Instance.CurrentRoundData.TargetNumber.ToString(); 
        }
    }
}
