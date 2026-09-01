using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    public ParticleSystem LandingEffectPrefab;

    public Enemy_Die enemy_Die;


    void Start()
    {
        enemy_Die = GetComponent<Enemy_Die>();
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

        //Destroy(enemy);
    }

    public void Lose()
    {
        Destroy(player);
    }
}

