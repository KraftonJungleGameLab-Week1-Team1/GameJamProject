using UnityEngine;

public class Fly : MonoBehaviour
{
    public float FlySpeed = 10f;
    public float Radius = 5f;
    public float Power = 50f;
    public bool FlyFlag = false;

    void Start()
    {
        FlyFlag = false;
    }

    void Update()
    {
        if(FlyFlag == true)
        {
            DoFly();
        }
    }

    public void DoFly()
    {
        transform.Translate(Vector3.forward * Power * Time.deltaTime, Camera.main.transform);

        transform.Translate(Vector3.up * FlySpeed * 0.6f * Time.deltaTime, Space.World);

        Destroy(gameObject,3f);
    }

    public void FlyFlagOn()
    {
        FlyFlag = true;
    }
}
