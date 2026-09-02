using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


// Applies an explosion force to all nearby rigidbodies
public class Slash : MonoBehaviour
{

    public Transform Tnumbers;

    public TMP_Text Ttext;

    public float SlashSpeed = 10f;

    public float increase = 10f;

    public GameManager gamemanager;


    public int LastResult;
    void Start()
    {
        gamemanager =  FindFirstObjectByType<GameManager>();
        //gamemanager = GameObject.Find("@GameManager");
        //gamemanager = GetComponent<GameManager>();
        
    }

    void Update()
    {
        {
            transform.position += new Vector3(SlashSpeed * Time.deltaTime, 0f,0f);
            transform.localScale +=  new Vector3(0f, increase*Time.deltaTime, 0f);

            //transform.Translate(Vector3.up * SlashSpeed * 0.6f * Time.deltaTime, Space.World);

            if (Tnumbers != null && Ttext != null)
            {


                Vector3 offset = new Vector3(0, 1f, 0);

                Ttext.transform.position = Tnumbers.position + offset;

                Ttext.text = gamemanager.LastResult.ToString();
            }

            Destroy(gameObject,5f);
        }
    }
}
