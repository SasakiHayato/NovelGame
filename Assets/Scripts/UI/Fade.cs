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

    Image _fadeImage;

    bool _isFade;
    public static bool End() => Instance._isFade;

    public static void SetSngle<T>(T t, float start, float end) where T : Object
    {
        Instance._isFade = false;

        Image image = t as Image;
        SpriteRenderer renderer = t as SpriteRenderer;
        Material material = t as Material;

        if (image != null) Instance.SetImage(image, start, end);
        if (renderer != null) Instance.SetSprite(renderer, start, end);
    }

    public static void SetCloss<T>(T[] t, float start, float end) where T : Object
    {
        Instance._isFade = false;

        Image image = t as Image;
        SpriteRenderer renderer = t as SpriteRenderer;
        Material material = t as Material;

        if (image != null) Instance.SetImage(image, start, end);
        if (renderer != null) Instance.SetSprite(renderer, start, end);
    }

    public static void Break() => Instance._isFade = true;
    public static Image CreateFadeImage(string findName = "Canvas")
    {
        if (Instance._fadeImage == null)
        {
            Instance._fadeImage = new GameObject("FadeImage").AddComponent<Image>();
            GameObject canvas = GameObject.Find(findName);
            Instance._fadeImage.transform.SetParent(canvas.transform);
        }

        RectTransform rect = Instance._fadeImage.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        return Instance._fadeImage;
    }

    void SetImage(Image image, float start, float end)
    {
        StartCoroutine(FadeImage(image, start, end));
    }

    void SetSprite(SpriteRenderer sprite, float strat, float end)
    {
        StartCoroutine(FadeSprite(sprite, strat, end));
    }

    IEnumerator FadeImage(Image image, float start, float end)
    {
        Color color = image.color;
        bool isFade = false;
        float time = 0;
        while (!isFade)
        {
            time += Time.deltaTime;
            float rate = Mathf.Lerp(start, end, time);
            image.color = new Color(color.r, color.g, color.b, rate);
            if (rate == end) isFade = true;
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

            if (_isFade)
            {
                renderer.color = new Color(color.r, color.g, color.b, end);
                isFade = true;
            }

            yield return null;
        }

        _isFade = true;
    }
}
