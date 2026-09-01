using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HPbar : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public Slider HPBar;

    void Start()
    {
        currentHP = MaxHP;
        HPBar.maxValue = MaxHP; //slider의 MaxValue를 우리가 원하는 체력 최대치로 초기화
        HPBar.value = currentHP; //slider의 value 값을 현재의 HP 값으로 초기화
    }

    //에너미 코드
    void enemy_die()
    {
        //적의 hp가 0보다 작다면
        //적이 죽는 효과
        //heal_Player();
        //다음적으로 넘어간다
    }
    //플레이어 코드
    void heal_Player()
    {
        //currentHP = currentHP + (log(enemy.MaxHealth) * (6 + 0.4 * enemy_count));
    }
    void Update()
    {
        HPBar.value = currentHP;
        currentHP = currentHP - (Time.deltaTime * 0.3f);
        /*
         * 최대체력은 60초
        deltatime 에 따라 일정하게 감소
        적을 잡으면 log(적의 체력) * (6 + 0.4 * 퇴치한 적의 수) 만큼 시간 회복
        if(enemy.die)
        currentHP = currentHP + (log(enemy.MaxHealth) * (6 + 0.4 * enemy_count)); 
        */
    }

    public void Player_die()
    {
       // if(currntHP < 0)
        {
            //리트라이UI 띄우기
        }
        //die_Effect();
        //리트라이
    }
}
