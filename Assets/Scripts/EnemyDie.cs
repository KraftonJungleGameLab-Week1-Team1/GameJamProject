using UnityEngine;
using System.Collections;
public class EnemyDie : MonoBehaviour
{
    public BattleManager battleManager;
    public int RandomDieEffect;
    public Fly flyEffect;
    public Bomb bombEffect;
    public Bonk bonkEffect;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void Defeated()
    {
        StartCoroutine(EnemyDieDelayTime());
    }

    IEnumerator EnemyDieDelayTime()
    {
        yield return new WaitForSeconds(0.8f);
        Debug.Log("pressed");
        switch (battleManager.RandomAttackIndex)
        {
            case 1:
                bombEffect.explode();
                break;
            case 2:
                bonkEffect.BonkFlagOn();
                break;
            case 3:
                flyEffect.FlyFlagOn();
                break;
            case 4:
                yield return new WaitForSeconds(1.1f);
                bonkEffect.BonkFlagOn();
                break;
        }
    }
}
