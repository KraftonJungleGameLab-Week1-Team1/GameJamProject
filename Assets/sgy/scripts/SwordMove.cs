using Unity.VisualScripting;
using UnityEngine;

public class SwordMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(-45, 0, 0);
        transform.position = new Vector3(14f, 7.9f, 6.7f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0f, 90 * Time.deltaTime, 180 * Time.deltaTime));
        //transform.Rotate(new Vector3(0f, 90 * Time.deltaTime, 0));
        
        if(Time.deltaTime <= 1)
        {
            transform.Rotate(new Vector3(0f, 0f, -180 * Time.deltaTime));
            transform.Translate(new Vector3(0f, -1f * Time.deltaTime, 1.5f * Time.deltaTime));
        }

    }
}
