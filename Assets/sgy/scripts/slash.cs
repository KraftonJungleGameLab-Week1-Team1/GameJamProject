using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Applies an explosion force to all nearby rigidbodies
public class Slash : MonoBehaviour
{
    public float SlashSpeed = 10f;

    public float increase = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        {
            transform.position += new Vector3(SlashSpeed * Time.deltaTime, 0f,0f);
            transform.localScale +=  new Vector3(0f, increase*Time.deltaTime, 0f);

            //transform.Translate(Vector3.up * SlashSpeed * 0.6f * Time.deltaTime, Space.World);

            Destroy(gameObject,5f);
        }
    }
}
