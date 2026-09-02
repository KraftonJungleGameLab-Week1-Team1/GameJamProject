using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EarnedScoreOverlay : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text earnedScoreText;

    public RectTransform destTr;

    void Start()
    {
    }

    public void SetTotalScoreText(int score)
    {
        StartCoroutine(SetTotalScore(score));
    }

    public IEnumerator SetTotalScore(int score)
    {
        for(int i = 0; i < score; ++i)
        {
            totalScoreText.text = $"Score : {i}";
            yield return null;
        }

        totalScoreText.text = $"Score : {score}";
    }
    public void SetEarnedScoreText(int score)
    {
        earnedScoreText.text = $"Score : {score}";
    }

    public void ShowEarnedScore(int score)
    {
        SetEarnedScoreText(score);

        SetTotalScoreText(score);
        //Start ShowCoroutine
    }

    //IEnumerator ShowCoroutine()
    //{

    //}
}
