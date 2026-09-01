using System.Collections;
using UnityEngine;
public class SwordSwing : MonoBehaviour
{
    public float swingTime = 2.0f;
    float t;
    void Start()
    {
        StartCoroutine(SwingAttack(new Vector3(-44.595f, -48.9f, -109.27f), new Vector3(0.783f, 1.628f, 0.498f)));
    }

    // void Update()
    // {
    //     t += Time.deltaTime;
    //     transform.rotation = Quaternion.Euler(0, 0, t*rotateSpeed);
    //     if (t * rotateSpeed >= 180) t = 0; //t값이 너무 커지지 않게 조절
        
    // }
    IEnumerator SwingAttack(Vector3 angle, Vector3 position)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angle);
        Vector3 startPosition = transform.position;
        // Vector3 endPosition = Vector3.(position);
        float timer = 0f;

        while (timer < swingTime)
        {
            timer += Time.deltaTime;
            
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timer/swingTime);
            
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
