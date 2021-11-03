using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFade
{
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
        List<SpriteRenderer> _setSprites = new List<SpriteRenderer>();

        public static void InSingle<T>(T target, float speed) where T : class
        {
            Image set = target as Image;
            if (set != null) Instance.FadeInForImage(set, speed);

            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null) Instance.FadeInForSprite(sprite, speed);

            else if (set == null && sprite == null)
            {
                Debug.LogError("�����^�������Ă܂���B �^���� Image �܂��́ASprite�ł��B");
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

                SpriteRenderer sprite = e.Current as SpriteRenderer;
                if (sprite != null) Instance.FadeInForSprite(sprite, speed);

                else if (set == null && sprite == null)
                {
                    Debug.LogError("�����^�������Ă܂���B �^���� Image �܂��́ASprite�ł��B");
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

                SpriteRenderer sprite = e.Current as SpriteRenderer;
                if (sprite != null) Instance._setSprites.Add(sprite);

                else if (set == null && sprite == null)
                {
                    Debug.LogError("�����^�������Ă܂���B �^���� Image �܂��́ASprite�ł��B");
                    return;
                }
            }

            if (Instance._setImage.Count > 0) Instance.FadeInForImage(null, speed);
            else if (Instance._setSprites.Count > 0) Instance.FadeInForSprite(null, speed);
        }

        public static void OutSingle<T>(T target, float speed) where T : class
        {
            Image set = target as Image;
            if (set != null) Instance.FadeOutForImage(set, speed);

            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null) Instance.FadeOutForSprite(sprite, speed);

            else if (set == null && sprite == null)
            {
                Debug.LogError("�����^�������Ă܂���B �^���� Image �܂��́ASprite�ł��B");
                return;
            }
        }
        public static void OutMultipe<T>(IEnumerable<T> targets, float speed) where T : class
        {
            IEnumerator e = targets.GetEnumerator();
            while (e.MoveNext())
            {
                Image set = e.Current as Image;
                if (set != null) Instance.FadeOutForImage(set, speed);

                SpriteRenderer sprite = e.Current as SpriteRenderer;
                if (sprite != null) Instance.FadeOutForSprite(sprite, speed);

                else if (set == null && sprite == null)
                {
                    Debug.LogError("�����^�������Ă܂���B �^���� Image �܂��́ASprite�ł��B");
                    return;
                }
            }
        }
        public static void OutMultipeAsync<T>(IEnumerable<T> targets, float speed) where T : class
        {
            Instance._async = true;
            IEnumerator e = targets.GetEnumerator();
            while (e.MoveNext())
            {
                Image set = e.Current as Image;
                if (set != null) Instance._setImage.Add(set);

                SpriteRenderer sprite = e.Current as SpriteRenderer;
                if (sprite != null) Instance._setSprites.Add(sprite);

                else if (set == null && sprite == null)
                {
                    Debug.LogError("�����^�������Ă܂���B �^���� Image �܂��́ASprite�ł��B");
                    return;
                }
            }

            if (Instance._setImage.Count > 0) Instance.FadeOutForImage(null, speed);
            else if (Instance._setSprites.Count > 0) Instance.FadeOutForSprite(null, speed);
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

        void FadeInForSprite(SpriteRenderer target = null, float speed = 0)
        {
            SetFadeInParam(speed);

            if (Instance._async) StartCoroutine(SetAsyncForSprite(_setSprites));
            else StartCoroutine(FadeToSprite(target));
        }
        void FadeOutForSprite(SpriteRenderer target = null, float speed = 0)
        {
            SetFadeOutParam(speed);

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
        IEnumerator SetAsyncForSprite(List<SpriteRenderer> targets)
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
        IEnumerator FadeToSprite(SpriteRenderer set)
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

        void ResetParam()
        {
            _currentFade = false;
            _async = false;
            _currentTime = 0;
            _setImage = new List<Image>();
            _setSprites = new List<SpriteRenderer>();
        }
    }

}
