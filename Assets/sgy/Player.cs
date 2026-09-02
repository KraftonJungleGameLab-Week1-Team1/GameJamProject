using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public Slash slash;

    public GameObject slash;

    public Transform Body;

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
        Instantiate(slash, Body.transform.position, Body.transform.rotation);
    }

    public void Defeated()
    {
        BombEffect.explode();
    }
}