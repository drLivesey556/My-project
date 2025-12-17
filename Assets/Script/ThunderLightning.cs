using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThunderLightning : MonoBehaviour
{
    [Header("Lightning")]
    public Image flash;
    public float flashPeak = 0.9f;
    public float flashFade = 0.8f;

    [Header("Timing")]
    public float minDelay = 6f;
    public float maxDelay = 15f;

    [Header("Thunder")]
    public AudioSource audioSource;
    public AudioClip[] thunderClips;

    [Header("VHS Boost")]
    public RawImage noise;
    public float baseNoiseAlpha = 0.25f;
    public float boostNoiseAlpha = 0.45f;
    public float boostTime = 0.6f;

    void Start()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            // иногда двойная молния
            int strikes = Random.value < 0.3f ? 2 : 1;

            for (int i = 0; i < strikes; i++)
            {
                Trigger();
                if (i == 0 && strikes > 1)
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
            }
        }
    }

    void Trigger()
    {
        StartCoroutine(Flash());
        StartCoroutine(NoiseBoost());
        PlayThunder();
    }

    IEnumerator Flash()
    {
        var c = flash.color;
        c.a = flashPeak;
        flash.color = c;

        float t = 0;
        while (t < flashFade)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(flashPeak, 0, t / flashFade);
            flash.color = c;
            yield return null;
        }

        c.a = 0;
        flash.color = c;
    }

    IEnumerator NoiseBoost()
    {
        var c = noise.color;
        c.a = boostNoiseAlpha;
        noise.color = c;

        yield return new WaitForSeconds(boostTime);

        c.a = baseNoiseAlpha;
        noise.color = c;
    }

    void PlayThunder()
    {
        if (audioSource && thunderClips.Length > 0)
            audioSource.PlayOneShot(thunderClips[Random.Range(0, thunderClips.Length)]);
    }
}

