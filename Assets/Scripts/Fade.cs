using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private static Fade _instance = null;
    public static Fade Instance
    {
        get
        {
            object instance = FindObjectOfType(typeof(Fade));
            if (instance != null) _instance = (Fade)instance;
            else
            {
                GameObject obj = new GameObject("Fade");
                _instance = obj.AddComponent<Fade>();
                obj.hideFlags = HideFlags.HideInHierarchy;
            }

            return _instance;
        }
    }

    float _start;
    float _end;

    Image _image;

    bool _break;

    public static void SetSngle<T>(T t, float start, float end) where T : Object
    {
        Instance._break = false;

        Image image = t as Image;
        SpriteRenderer renderer = t as SpriteRenderer;
        Material material = t as Material;

        if (image != null) Instance.SetImage(image);
        if (renderer != null) Instance.SetSprite(renderer, start, end);
    }

    public static void Break()
    {
        Instance._break = true;
    }

    void SetImage(Image image)
    {
        _image = image;
        StartCoroutine(FadeImage());
    }

    void SetSprite(SpriteRenderer sprite, float strat, float end)
    {
        StartCoroutine(FadeSprite(sprite, strat, end));
    }

    IEnumerator FadeImage()
    {
        Color color = _image.color;
        bool isFade = false;
        float time = 0;
        while (!isFade)
        {
            time += Time.deltaTime;
            float rate = Mathf.Lerp(_start, _end, time);
            _image.color = new Color(color.r, color.g, color.b, rate);
            if (rate == _end) isFade = true;
            yield return null;
        }
    }

    IEnumerator FadeSprite(SpriteRenderer renderer, float start, float end)
    {
        Color color = renderer.color;
        bool isFade = false;
        float time = 0;
        while (!isFade)
        {
            time += Time.deltaTime;
            float rate = Mathf.Lerp(start, end, time);
            renderer.color = new Color(color.r, color.g, color.b, rate);
            if (rate == end) isFade = true;

            if (_break)
            {
                renderer.color = new Color(color.r, color.g, color.b, end);
                isFade = true;
            }

            yield return null;
        }
    }
}
