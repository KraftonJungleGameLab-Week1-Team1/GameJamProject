using Unity.VisualScripting;
using UnityEngine;
using System.Collections; // <-- Add this line to fix the error!
using System.Collections.Generic;
using Unity.Mathematics;

public class SwordMove : MonoBehaviour
{

    public float preparetime = 0.35f;
    public float attacktime = 0.65f;

    public
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(-45, 0, 0);
        transform.position = new Vector3(14f, 7.9f, 6.7f);
        StartCoroutine(DelayTime());
        
        transform.position = new Vector3(13.8f, 6.51f, 8.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
     

    }


    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1.0f); // 2초 동안 대기
        Debug.Log("1초 후 호출됨");
        yield return null;
    }
}
