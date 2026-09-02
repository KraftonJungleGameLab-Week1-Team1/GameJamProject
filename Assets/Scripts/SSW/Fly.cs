using UnityEngine;

public class Fly : MonoBehaviour
{
    public float flySpeed = 10f;
    public float radius = 5.0F;
    public float power = 50.0F;

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
        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(Vector3.forward * power * Time.deltaTime, Camera.main.transform);

        // Move the object upward in world space 1 unit/second.
        transform.Translate(Vector3.up * flySpeed * 0.6f * Time.deltaTime, Space.World);

        Destroy(gameObject,3f);
    }

    public void FlyFlagOn()
    {
        FlyFlag = true;
    }
}
