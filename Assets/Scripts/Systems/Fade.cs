using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    List<Sprite> _setSprites = new List<Sprite>();

    public static void InSingle<T>(T target, float speed) where T : class
    {
        Image set = target as Image;
        if (set != null) Instance.FadeInForImage(set, speed);

        Sprite sprite = target as Sprite;
        if (sprite != null) Instance.FadeInForSprite(sprite, speed);

        else if (set == null && sprite == null)
        {
            Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image Ç‹ÇΩÇÕÅASpriteÇ≈Ç∑ÅB");
            return;
        }
    }
    public static void InMultipe<T>(IEnumerable<T> targets, float speed) where T : class
    {
        IEnumerator e = targets.GetEnumerator();
        while (e.MoveNext())
        {
            Image set = e.Current as Image;
            if (set != null) Instance.FadeInForImage(set, speed);

            Sprite sprite = e.Current as Sprite;
            if (sprite != null) Instance.FadeInForSprite(sprite, speed);

            else if (set == null && sprite == null)
            {
                Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image Ç‹ÇΩÇÕÅASpriteÇ≈Ç∑ÅB");
                return;
            }
        }
    }
    public static void InMultipeAsync<T>(IEnumerable<T> targets, float speed) where T : class
    {
        Instance._async = true;
        IEnumerator e = targets.GetEnumerator();
        while (e.MoveNext())
        {
            Image set = e.Current as Image;
            if (set != null) Instance._setImage.Add(set);

            Sprite sprite = e.Current as Sprite;
            if (sprite != null) Instance._setSprites.Add(sprite);

            else if (set == null && sprite == null)
            {
                Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image Ç‹ÇΩÇÕÅASpriteÇ≈Ç∑ÅB");
                return;
            }
        }

        if (Instance._setImage.Count > 0) Instance.FadeInForImage(null, speed);
        else if (Instance._setSprites.Count > 0) Instance.FadeInForSprite(null, speed);
    }

    public static void OutSingle(Image target, float speed) => Instance.FadeOutForImage(target, speed);
    public static void OutMultipe(IEnumerable<Image> targets, float speed)
    {
        IEnumerator e = targets.GetEnumerator();
        while (e.MoveNext())
        {
            Image set = (Image)e.Current;
            Instance.FadeOutForImage(set, speed);
        }
    }
    public static void OutMultipeAsync(IEnumerable<Image> targets, float speed)
    {
        Instance._async = true;
        IEnumerator e = targets.GetEnumerator();
        while (e.MoveNext())
        {
            Image set = (Image)e.Current;
            Instance._setImage.Add(set);
        }

        Instance.FadeOutForImage(null, speed);
    }


    void FadeInForImage(Image target = null, float speed = 0)
    {
        SetFadeInParam(speed);
        
        if (Instance._async) StartCoroutine(SetAsyncForImage(_setImage));
        else StartCoroutine(FadeToImage(target));
    }
    void FadeOutForImage(Image target = null, float speed = 0)
    {
        SetFadeOutParam(speed);

        if (Instance._async) StartCoroutine(SetAsyncForImage(_setImage));
        else StartCoroutine(FadeToImage(target));
    }

    void FadeInForSprite(Sprite target = null, float speed = 0)
    {
        SetFadeInParam(speed);

        if (Instance._async) StartCoroutine(SetAsyncForSprite(_setSprites));
        else StartCoroutine(FadeToSprite(target));
    }

    void SetFadeInParam(float speed)
    {
        Instance._startVal = 0;
        Instance._endVal = 1;
        Instance._fadeSpeed = speed;
    }
    void SetFadeOutParam(float speed)
    {
        Instance._startVal = 1;
        Instance._endVal = 0;
        Instance._fadeSpeed = speed;
    }
    
    IEnumerator SetAsyncForImage(List<Image> targets)
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

        ResetParam();
    }

    IEnumerator SetAsyncForSprite(List<Sprite> targets)
    {
        int count = 0;
        while (count <= targets.Count - 1)
        {
            if (!_currentFade)
            {
                yield return FadeToSprite(targets[count]);
                count++;
            }
            yield return null;
        }

        ResetParam();
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

            if (alfa == _endVal) isFade = true;
            yield return null;
        }

        _currentFade = false;
        _currentTime = 0;

        if (!_async) ResetParam();
    }

    IEnumerator FadeToSprite(Sprite target)
    {
        yield return null;
    }

    void ResetParam()
    {
        _currentFade = false;
        _async = false;
        _currentTime = 0;
        _setImage = new List<Image>();
        _setSprites = new List<Sprite>();
    }
}
