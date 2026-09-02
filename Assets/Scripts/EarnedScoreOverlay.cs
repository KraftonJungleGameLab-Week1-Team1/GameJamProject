using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class EarnedScoreOverlay : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text earnedScoreText;

    public Transform DestTransform;
    public int totalScore = 0;


    public void SetTotalScoreText(int score)
    {
        totalScoreText.text = $"Score : {score}";
    }

    public IEnumerator SetTotalScore(int score)
    {
        for (int i = totalScore; i < score; ++i)
        {
            totalScoreText.text = $"Score : {i}";
            yield return null;
        }
        totalScore += score;
        totalScoreText.text = $"Score : {score}";
    }
    public IEnumerator SetEarnedScoreText(int score)
    {
        float originFontSize = earnedScoreText.fontSize;
        Vector3 originPosition = earnedScoreText.gameObject.transform.position;
        earnedScoreText.text = score.ToString();
        earnedScoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);

        float flag = 1f;

        while(flag > 0)
        {
            flag -= Time.deltaTime * 2f;
            earnedScoreText.fontSize = originFontSize * flag;
            earnedScoreText.gameObject.transform.position = DestTransform.position - ((DestTransform.position - originPosition) * flag);
            yield return null;
        }
        earnedScoreText.gameObject.SetActive(false);
        earnedScoreText.gameObject.transform.position = originPosition;
        earnedScoreText.fontSize = originFontSize;
    }

    public IEnumerator ShowEarnedScore(int score)
    {

        StartCoroutine(AudioManager.Instance.PlaySequence());
        yield return StartCoroutine(SetEarnedScoreText(score));
        yield return StartCoroutine(SetTotalScore(totalScore + score));
    }

    //IEnumerator ShowCoroutine()
    //{

    //}
}
