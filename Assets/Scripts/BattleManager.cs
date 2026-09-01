using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    public ParticleSystem LandingEffectPrefab;

    public Enemy_Die enemy_Die;

    public Player player_attack;


    void Start()
    {
        enemy_Die = GetComponent<Enemy_Die>();
        player_attack = GetComponent<Player>();
    }

    GameObject player;
    GameObject enemy;

    public void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnPoint.position, playerPrefab.transform.rotation);
        Instantiate(LandingEffectPrefab, playerSpawnPoint.position, LandingEffectPrefab.transform.rotation);
    }

    public void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, enemyPrefab.transform.rotation);
        Instantiate(LandingEffectPrefab, enemySpawnPoint.position, LandingEffectPrefab.transform.rotation);
    }

    public void Win()
    {

        enemy_Die.Defeated();
        player_attack.DoAttack();
        //Destroy(enemy);
    }

    public void Lose()
    {
        Destroy(player);
    }
}

