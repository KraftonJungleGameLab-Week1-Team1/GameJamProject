using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Play()
    {
        SceneManager.LoadScene("Main");    
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }
}
