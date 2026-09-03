using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class BattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject PlayerPrefab;
    public Transform PlayerSpawnPoint;

    public GameObject EnemyPrefab;
    public Transform EnemySpawnPoint;

    public VisualEffect SmokeEffectPrefab;

    public EnemyDie enemyDie;

    public Player playerAttack;

    public int RandomAttackIndex;

    public int randomAttackStartIndex;

    public int randomAttackEndIndex;

    private GameObject player;
    private GameObject enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnEnemy();
        }
    }

    public void SpawnPlayer()
    {
        player = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, PlayerPrefab.transform.rotation);
        playerAttack = player.GetComponent<Player>();
        Instantiate(SmokeEffectPrefab, PlayerSpawnPoint.position, SmokeEffectPrefab.transform.rotation);
    }

    public void SpawnEnemy()
    {
        enemy = Instantiate(EnemyPrefab, EnemySpawnPoint.position, EnemyPrefab.transform.rotation);
        enemyDie = enemy.GetComponent<EnemyDie>();
        Instantiate(SmokeEffectPrefab, EnemySpawnPoint.position, SmokeEffectPrefab.transform.rotation);
    }

    public void Win()
    {
        enemyDie.Defeated();
        RandomAttackIndex = Random.Range(randomAttackStartIndex, randomAttackEndIndex);
        playerAttack.DoAttack(RandomAttackIndex);
    }

    public void Lose()
    {
        playerAttack.Defeated();
    }
}

