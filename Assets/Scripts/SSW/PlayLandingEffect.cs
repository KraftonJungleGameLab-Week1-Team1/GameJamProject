using UnityEngine;

public class PlayLandingEffect : MonoBehaviour
{
    public ParticleSystem LandingEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        Debug.Log("발동");
        Instantiate(LandingEffect, LandingEffect.transform.position, LandingEffect.transform.rotation);
    }
}
