using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private static Fade _instance = new Fade();
    public static Fade Instance => _instance;

    float _startVal = 0;
    float _endVal = 0;
    float _fadeSpeed = 0;
    float _currentTime = 0;

    List<SpriteRenderer> _setRenderers = new List<SpriteRenderer>();

    public static void InSingle(Image target, float speed)
    {
        Instance._startVal = 0;
        Instance._endVal = 1;
        Instance._fadeSpeed = speed;

        Instance.StartCoroutine(Instance.FadeToImage(target));
    }

    public static void InMultipe(List<Image> target, float speed)
    {
        if (target.Count <= 1)
        {
            Debug.Log("Image‚ª•¡”‚ ‚è‚Ü‚¹‚ñ");
            return;
        }

        Instance._startVal = 0;
        Instance._endVal = 1;
        Instance._fadeSpeed = speed;

        target.ForEach(t => Instance.StartCoroutine(Instance.FadeToImage(t)));
    }

    public static void Out(float speed)
    {
        Instance._startVal = 0;
        Instance._endVal = 1;
        Instance._fadeSpeed = speed;
    }

    IEnumerator FadeToImage(Image set)
    {
        bool isFade = false;
        while (!isFade)
        {
            _currentTime += Time.deltaTime;
            float rate = _currentTime / _fadeSpeed;
            float alfa = Mathf.Lerp(_startVal, _endVal, _fadeSpeed);

            set.color = new Color(set.color.r, set.color.g, set.color.b, alfa);

            if (alfa == _endVal) isFade = true;
            yield return null;
        }

        _currentTime = 0;
    }
}
