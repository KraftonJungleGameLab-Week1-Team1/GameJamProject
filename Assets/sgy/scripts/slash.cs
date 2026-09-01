using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Applies an explosion force to all nearby rigidbodies
public class Slash : MonoBehaviour
{
    public float SlashSpeed = 10f;

    public void Start()
    {
        
    }

    void Update()
    {
        {
            // Move the object forward along its z axis 1 unit/second.
            transform.Translate(Vector3.right * SlashSpeed * Time.deltaTime, Camera.main.transform);

            // Move the object upward in world space 1 unit/second.
            //transform.Translate(Vector3.up * SlashSpeed * 0.6f * Time.deltaTime, Space.World);

            if(Time.deltaTime >= 5)
            {
                Destroy(this);
            }
        }
    }
}
