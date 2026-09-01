using UnityEngine;

public class Enemy_Die : MonoBehaviour
{

    public BattleManager BattleManager;

    public int RandDieEffect;

    public Fly FlyEffct;
    public Bomb BombEffect;
    public Bonk BonkEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Defeated()
    {
        Debug.Log("pressed");
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

        //Destroy(enemy);
    }
}
