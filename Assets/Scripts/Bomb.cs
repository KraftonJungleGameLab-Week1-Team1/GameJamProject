using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class Bomb : MonoBehaviour
{
    public float Power = 10.0f;
    public float Upward = 1f;
    public float DestroyTime = 4f;
    private float radius;

    public void explode()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            float RandomOffset = Random.Range(1f, 2f);
            child.gameObject.AddComponent<Rigidbody>();
            if (child == transform) continue;
            //child.transform.SetParent(null);
            Rigidbody rb = child.GetComponent<Rigidbody>();
            radius = Random.Range(10,20);
            rb.AddExplosionForce(Power, new Vector3(9f - RandomOffset, 0.10f, -400f - RandomOffset), radius, Upward);
            Destroy(child.gameObject, DestroyTime);
        }
        Rigidbody parent = GetComponent<Rigidbody>();

        if (parent != null)
        {
            Destroy(parent);
        }
    }  
}
