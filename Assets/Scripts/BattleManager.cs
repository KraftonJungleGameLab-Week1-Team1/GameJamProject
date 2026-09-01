using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    public ParticleSystem LandingEffectPrefab;

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
        Destroy(enemy);
    }

    public void Lose()
    {
        Destroy(player);
    }
}
