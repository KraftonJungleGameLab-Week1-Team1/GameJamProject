using UnityEngine;

public class Bonk : MonoBehaviour
{
    public float BonkSpeed = 5f;
    public bool BonkFlag = false;
    void Start()
    {
        BonkFlag = false;
    }
    void Update()
    {
        if (BonkFlag == true)
        {
            DoBonk();
            Destroy(gameObject, 0.5f);
        }
    }
    public void DoBonk()
    {
        transform.localScale += new Vector3(0f, -BonkSpeed * Time.deltaTime, 0f);
    }

    public void BonkFlagOn()
    {
        BonkFlag = true;
    }
}
