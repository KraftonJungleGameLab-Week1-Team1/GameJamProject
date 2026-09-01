using UnityEngine;

public class Fly : MonoBehaviour
{
    public float flySpeed = 10f;

    public float radius = 5.0F;
    public float power = 10.0F;

    void Start()
    {
        Vector3 explosionPos = new Vector3(13f, 6.5f, 7.5f);
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }

    void Update()
    {
        {
            // Move the object forward along its z axis 1 unit/second.
            transform.Translate(Vector3.right * flySpeed * Time.deltaTime, Camera.main.transform);

            // Move the object upward in world space 1 unit/second.
            transform.Translate(Vector3.up * flySpeed * 0.6f * Time.deltaTime, Space.World);
        }
    }
}
