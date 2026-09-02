using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text survivalTimeText;

    [SerializeField] private ExpressionManager expressionManager;

    public void ShowResult(int totalscore, float survivalTime)
    {
        gameObject.SetActive(true);

        scoreText.text = totalscore.ToString();
        survivalTimeText.text = FormatTime(survivalTime);
    }

    private string FormatTime(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes:00}:{seconds:00}";
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}