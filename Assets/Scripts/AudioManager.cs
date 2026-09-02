using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> ScoreSoundSources;
    public static AudioManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(int sequenceIndex)
    {
        StartCoroutine(PlaySequence(sequenceIndex));
    }
    public IEnumerator PlaySequence(int playLength)
    {
        for (int i = 1; i < playLength + 1; ++i)
        {
            if (i > 4)
            {
                ScoreSoundSources[4].Play();
                yield return new WaitWhile(() => ScoreSoundSources[4].isPlaying);

            }
            else
            {
                ScoreSoundSources[i].Play();
                yield return new WaitWhile(() => ScoreSoundSources[i].isPlaying);
            }

        }

        if(playLength > 2)
        {
            ScoreSoundSources[5].Play();
        }

        //for (int i = 1; i < playLength; ++i)
        //{
        //    ScoreSoundSources[i].Play();
        //    yield return new WaitWhile(() => ScoreSoundSources[i].isPlaying);
        //}
    }

    public IEnumerator PlaySequence()
    {
        //audioSource.clip = ScoreSoundSources[21];
        //audioSource.Play();
        //yield return new WaitWhile(() => audioSource.isPlaying);

        //audioSource.clip = ScoreSoundSources[7];
        //audioSource.pitch = 1.4f;
        //audioSource.Play();
        //yield return new WaitWhile(() => audioSource.isPlaying);

        //audioSource.pitch = 1f;
        yield return null;
    }
}
