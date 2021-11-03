using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private static Fade _instance;
    public static Fade Instance
    { 
        get
        {
            object instance = FindObjectOfType(typeof(Fade));
            if (instance == null)
            {
                GameObject fade = new GameObject("Fade");
                _instance = fade.AddComponent<Fade>();
                fade.hideFlags = HideFlags.HideInHierarchy;
            }
            else
            {
                _instance = (Fade)instance;
            }

            return _instance;
        }
    }

    float _startVal = 0;
    float _endVal = 0;
    float _fadeSpeed = 0;
    float _currentTime = 0;

    bool _currentFade = false;
    bool _async = false;

    List<Image> _setImage = new List<Image>();

    public static void InSingle(Image target, float speed) => Instance.FadeIn(target, speed);
    public static void InMultipe(List<Image> targets, float speed) => targets.ForEach(t => Instance.FadeIn(t, speed));

    public static void InMultipeAsync(List<Image> targets, float speed)
    {
        Instance._async = true;
        Instance._setImage = targets;
        Instance.FadeIn(null, speed);
    }

    public static void OutSingle(float speed)
    {
        Instance._startVal = 0;
        Instance._endVal = 1;
        Instance._fadeSpeed = speed;
    }

    void FadeIn(Image targer = null, float speed = 0)
    {
        Instance._startVal = 0;
        Instance._endVal = 1;
        Instance._fadeSpeed = speed;
        
        if (Instance._async) StartCoroutine(SetAsync(_setImage));
        else StartCoroutine(FadeToImage(targer));
    }

    IEnumerator SetAsync(List<Image> targets)
    {
        int count = 0;
        while (count <= targets.Count - 1)
        {
            if (!_currentFade)
            {

                yield return FadeToImage(targets[count]);

                count++;
            }
            yield return null;
        }
    }

    IEnumerator FadeToImage(Image set)
    {
        _currentFade = true;
        bool isFade = false;
        while (!isFade)
        {
            _currentTime += Time.deltaTime;
            float rate = _currentTime / _fadeSpeed;
            float alfa = Mathf.Lerp(_startVal, _endVal, rate);

            set.color = new Color(set.color.r, set.color.g, set.color.b, alfa);

            if (alfa >= _endVal) isFade = true;
            yield return null;
        }

        _currentFade = false;
        _currentTime = 0;
    }
}
