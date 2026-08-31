using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Enemy_health;
    public float Enemy_count;
    public float Enemy_mult;
    void start()
    {
        /*
         
         */
    }


    void update()
    {
        /*
         
         */
    }
    public void Enemy_die()
    { 
        //die_Effect();
        Enemy_count = Enemy_count + 1;
        //다음적 불러오기;
    }
}
