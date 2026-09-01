using UnityEngine;

public class Fly : MonoBehaviour
{
    public float flySpeed = 10f;
    public float radius = 5.0F;
    public float power = 10.0F;

    void Start()
    {
       
    }

    void Update()
    {
        {
            DoFly();
        }
    }

    public void DoFly()
    {
        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(Vector3.forward * flySpeed * Time.deltaTime, Camera.main.transform);

        // Move the object upward in world space 1 unit/second.
        transform.Translate(Vector3.up * flySpeed * 0.6f * Time.deltaTime, Space.World);

        Destroy(gameObject,3f);
    }
}
