using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform parent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 적군 사망시
        if (Input.GetKeyDown(KeyCode.S))
        {   
            Instantiate(enemyPrefab, parent.position, parent.rotation);
        }
    }
}
