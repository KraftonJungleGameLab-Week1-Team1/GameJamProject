using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using TMPro;

// Applies an explosion force to all nearby rigidbodies
public class Bomb : MonoBehaviour
{
    float radius;
    public float power = 10.0F;
    public float upward = 1f;
    public float destroyTime = 4f;

    void Start()
    {
        /*
        Transform[] allChildren = GetComponentsInChildren<Transform>();

        Vector3 explosionPos = transform.position;
        foreach (Transform child in allChildren)
        {
            Debug.Log(child);
            
            child.gameObject.AddComponent<Rigidbody>();
                if (child == transform) continue;
                child.transform.SetParent(null);
                Rigidbody rb = child.GetComponent<Rigidbody>();
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
                Debug.Log("모든자식리지드바디넣고폭발!");
                Destroy(child.gameObject, 4f);            
        }

        Rigidbody parent = GetComponent<Rigidbody>();
        if (parent != null)
        {
            Destroy(parent);
        }*/
    }


    public void explode()
    {
        /*Rigidbody parent = GetComponent<Rigidbody>();
        if (parent != null)
        {
            Destroy(parent);
        }

        Rigidbody[] allChildren = GetComponentsInChildren<Rigidbody>();
       
        Vector3 explosionPos = transform.position;
        foreach(Rigidbody child in allChildren){
            child.gameObject.AddComponent<Rigidbody>();
            if (child == transform) continue;
            child.transform.SetParent(null);
            Rigidbody rb = child.GetComponent<Rigidbody>();
            rb.AddExplosionForce(power, explosionPos, radius, upward, ForceMode.Impulse);
            Debug.Log("모든자식리지드바디넣고폭발!");*/
        Transform[] allChildren = GetComponentsInChildren<Transform>();

        
        foreach (Transform child in allChildren)
        {
            Debug.Log(child);
            float RandomOffset = Random.Range(1f, 2f);
            child.gameObject.AddComponent<Rigidbody>();
            if (child == transform) continue;
            //child.transform.SetParent(null);
            Rigidbody rb = child.GetComponent<Rigidbody>();
            radius = Random.Range(10,20);
            rb.AddExplosionForce(power, new Vector3(9f - RandomOffset, 0.10f, -400f - RandomOffset), radius, upward);
            Destroy(child.gameObject, destroyTime);
        }

        Rigidbody parent = GetComponent<Rigidbody>();
        if (parent != null)
        {
            Destroy(parent);
        }
    }  
}
