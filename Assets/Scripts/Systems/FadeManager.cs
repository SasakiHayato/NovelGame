using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    float m_endVal;
    float m_startVal;
    float m_currentTime = 0;

    bool m_isFade;
    bool m_currentFade;

    public void SyncFadeIn(List<Image> sets, float fadeSpeed)
    {
        m_isFade = false;
        m_startVal = 0;
        m_endVal = 1;
        m_currentTime = 0;
        foreach (var item in sets)
            StartCoroutine(Fade(item, fadeSpeed));
    }

    public void AsyncFadeIn(List<Image> sets, List<Image> charas, float fadeSpeed)
    {
        m_isFade = false;
        m_currentFade = false;
        m_startVal = 0;
        m_endVal = 1;

        StartCoroutine(Async(sets, charas, fadeSpeed));
    }

    public void SyncFadeOut(List<Image> sets, float fadeSpeed)
    {
        m_isFade = false;
        m_startVal = 1;
        m_endVal = 0;
        m_currentTime = 0;
        foreach (var item in sets)
            StartCoroutine(Fade(item, fadeSpeed));
    }

    public void AsyncFadeOut(List<Image> sets, List<Image> charas, float fadeSpeed)
    {
        m_isFade = false;
        m_currentFade = false;
        m_startVal = 1;
        m_endVal = 0;

        StartCoroutine(Async(sets, charas, fadeSpeed));
    }

    IEnumerator Async(List<Image> sets, List<Image> charas, float fadeSpeed)
    {
        int count = 1;
        while (count <= sets.Count)
        {
            if (!m_isFade && !m_currentFade)
            {
                m_currentTime = 0;
                StartCoroutine(Fade(charas[count - 1], fadeSpeed));
                count++;
            }
            
            yield return null;
        }
        
    }

    IEnumerator Fade(Image set, float fadeSpeed)
    {
        m_currentFade = true;
        while (!m_isFade)
        {
            m_currentTime += Time.deltaTime;
            float rate = m_currentTime / fadeSpeed;
            float alfa = Mathf.Lerp(m_startVal, m_endVal, rate);

            set.color = new Color(set.color.r, set.color.g, set.color.b, alfa);
            if (alfa == m_endVal)
            {
                m_isFade = true;
            }
            yield return null;
        }
        m_currentFade = false;
        m_isFade = false;
    }
}
