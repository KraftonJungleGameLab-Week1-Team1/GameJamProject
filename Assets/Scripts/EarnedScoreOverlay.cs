using System.Collections;
using TMPro;
using UnityEngine;

public class EarnedScoreOverlay : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text earnedScoreText;

    public RectTransform destTr;

    public void SetTotalScoreText(int score)
    {
        totalScoreText.text = $"Score : {score}";
    }
    public void SetEarnedScoreText(int score)
    {
        earnedScoreText.text = $"Score : {score}";
    }

    public void ShowEarnedScore(int score)
    {
        SetEarnedScoreText(score);
        //Start ShowCoroutine
    }

    //IEnumerator ShowCoroutine()
    //{

    //}
}
