using UnityEngine;

public class Bonk : MonoBehaviour
{

    public float BonkSpeed = 40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoBonk();
        Destroy(gameObject, 0.5f);
    }

    public void DoBonk()
    {
        transform.localScale += new Vector3(0f, -BonkSpeed * Time.deltaTime, 0f);

        
    }
}
