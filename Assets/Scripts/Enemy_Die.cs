using UnityEngine;
using System.Collections;
public class Enemy_Die : MonoBehaviour
{

    public BattleManager battleManager;

    // public Animator animator;

    public int RandDieEffect;

    public Fly FlyEffct;
    public Bomb BombEffect;
    public Bonk BonkEffect;

    //public int attackNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Defeated()
    {
        // animator.SetTrigger("Stop");
        StartCoroutine(EnemyDieDelayTime());

        /* Debug.Log("pressed");
         RandDieEffect = Random.Range(1, 4);
         switch (RandDieEffect)
         {
             case 1:
                 FlyEffct.FlyFlagOn();
                 break;
             case 2:
                 BombEffect.explode();
                 break;
             case 3:
                 BonkEffect.BonkFlagOn();
                 break;
         }
         */
        //Destroy(enemy);
    }

    IEnumerator EnemyDieDelayTime()
    {
        yield return new WaitForSeconds(0.8f); // 초 동안 대기
        Debug.Log("pressed");
        //RandDieEffect = Random.Range(1, 4);
        //attackNumber = battleManager.RandomAttackIndex;
        switch (battleManager.RandomAttackIndex)
        {
            case 1:
                //FlyEffct.FlyFlagOn();
                BombEffect.explode();
                break;
            case 2:
                //BombEffect.explode();
                //FlyEffct.FlyFlagOn();
                BonkEffect.BonkFlagOn();
                break;
            case 3:
                //BonkEffect.BonkFlagOn();
                FlyEffct.FlyFlagOn();
                break;
        }
    }
}
