using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public Slash slash;

    public GameObject slash;

    public ParticleSystem LandingEffect;

    public Transform Body;
    
    // public Transform EnemySpawnPoint;

    public Bomb BombEffect;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
          //  Instantiate(slash, Body.transform.position, Body.transform.rotation);
    }

    public void DoAttack(int attackIndex)
    {
        switch (attackIndex) {
            case 1:
                animator.SetTrigger("SwordAttack1");
                break;
            case 2:
                animator.SetTrigger("SwordAttack2");
                break;
            case 3:
                animator.SetTrigger("SwordAttack3");
                break;
            
        }
        
    }

    public void PlaySlash(int attackIndex)
    {
        if (attackIndex == 1)
        {
            Instantiate(slash, Body.transform.position, Quaternion.Euler(-24.406f, -2.407f, -0.063f));     
        }
        if (attackIndex == 3)
        {
            Instantiate(slash, Body.transform.position, Quaternion.Euler(-43.407f, 14.734f, 8.395f));    
        }
        
    }

    public void PlayLandingEffect()
    {
        Instantiate(LandingEffect, new Vector3(9.74f, 0f, -400.501f), LandingEffect.transform.rotation);
    }

    public void Defeated()
    {
        BombEffect.explode();
    }
}