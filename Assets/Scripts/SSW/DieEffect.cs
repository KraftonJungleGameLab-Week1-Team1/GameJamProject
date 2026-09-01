using UnityEngine;

public class DieEffect : MonoBehaviour
{
    public Rigidbody[] bodyParts;
    public float force = 8f;
    void PlayDieEffect(Vector3 direction)
    {
        foreach (Rigidbody rb in bodyParts)
        {
            rb.transform.SetParent(null);
            rb.isKinematic = false;
            rb.AddForce(direction * force, ForceMode.Impulse);
        }    
    }
}
