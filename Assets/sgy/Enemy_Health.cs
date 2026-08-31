using UnityEngine;

public class Enemy_Health : Enemy
{
  
    void start()
    {
        if(Enemy_count == 0)
        {
            Enemy_Health = Random.Range(15, 20);
        }
        else (){
            Enemy_Health = Random.Range(15, 20) * (4 + (0.7 * Enemy_count)^2);
        }

        Enemy_mult = Random.Range(3, 9);
        
    }


    void update()
    {
        /*
         
         */
        if (Playerattack == 1)
            Enemy_die();
    }
}
