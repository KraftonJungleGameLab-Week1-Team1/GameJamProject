using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public Slash slash;

    public GameObject slash;

    public Transform Body;

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Instantiate(slash, Body.transform.position, Body.transform.rotation);
    }

    public void DoAttack()
    {

        Instantiate(slash, Body.transform.position, Body.transform.rotation);
    }
}