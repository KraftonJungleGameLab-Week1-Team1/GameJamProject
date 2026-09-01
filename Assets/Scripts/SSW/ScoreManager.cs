using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    int score;

    public int Score {get {return score;} set {score = value; scoreText.text = score.ToString();}}

    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        score = score + ((int)Time.deltaTime);
        SetScore(score);
    }

    public int GetScore() {
        return Score;
    }

    public void SetScore(int value)
    {
        score = value;
        scoreText.text = score.ToString();
    }

}