using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main");    
    }

    public void Exit()
    {
        Application.Quit();
    }
}
