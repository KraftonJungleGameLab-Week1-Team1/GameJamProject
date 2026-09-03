using UnityEngine;


public class Slash : MonoBehaviour
{
    public Transform Tnumbers;

    public float SlashSpeed = 10f;

    public float Increase = 10f;

    public GameManager gameManager;

    public int LastResult;
    void Start()
    {
        gameManager =  FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        {
            transform.position += new Vector3(SlashSpeed * Time.deltaTime, 0f,0f);
            transform.localScale +=  new Vector3(0f, Increase*Time.deltaTime, 0f);

            Destroy(gameObject,5f);
        }
    }
}
