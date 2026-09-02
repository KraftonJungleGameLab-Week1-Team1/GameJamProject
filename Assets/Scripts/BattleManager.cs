using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    public ParticleSystem LandingEffectPrefab;

    public Enemy_Die enemy_Die;

    public Player player_attack;

    public int RandomAttackIndex;

    public GameManager gameManager;


    void Start()
    {
        //enemy_Die = GetComponent<Enemy_Die>();
        //player_attack = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("pressed");
            Win();
            //Instantiate(slash, Body.transform.position, Body.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("pressed");
            SpawnEnemy();
            //Instantiate(slash, Body.transform.position, Body.transform.rotation);
        }
    }

    GameObject player;
    GameObject enemy;

    public void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnPoint.position, playerPrefab.transform.rotation);
        player_attack = player.GetComponent<Player>();
        Instantiate(LandingEffectPrefab, playerSpawnPoint.position, LandingEffectPrefab.transform.rotation);
    }

    public void SpawnEnemy()
    {
        //enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, enemyPrefab.transform.rotation);
        //enemy_Die = enemy.GetComponent<Enemy_Die>();
        //Instantiate(LandingEffectPrefab, enemySpawnPoint.position, LandingEffectPrefab.transform.rotation);
        StartCoroutine(EnemyDelayTime());
    }

    public void Win()
    {

        enemy_Die.Defeated();
        RandomAttackIndex = Random.Range(1, 4);
        player_attack.DoAttack(RandomAttackIndex);
        //Destroy(enemy);
    }

    public void Lose()
    {
        player_attack.Defeated();
        //Destroy(player);
    }
    IEnumerator EnemyDelayTime()
    {
        yield return new WaitForSeconds(1.6f); // 2초 동안 대기
        enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, enemyPrefab.transform.rotation);
        enemy_Die = enemy.GetComponent<Enemy_Die>();
        Instantiate(LandingEffectPrefab, enemySpawnPoint.position, LandingEffectPrefab.transform.rotation);
        Debug.Log("지연실행됨");
    }

}

