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

        #region ÉÅÉìÉoïœêî
        float _startVal = 0;
        float _endVal = 0;
        float _fadeSpeed = 0;
        float _currentTime = 0;

        bool _currentFade = false;
        bool _async = false;

        bool _break = false;

        List<Image> _setImage = new List<Image>();
        List<SpriteRenderer> _setSprites = new List<SpriteRenderer>();
        List<Material> _setMaterials = new List<Material>();
        #endregion

        public static void InSingle<T>(T target, float speed) where T : class
        {
            Image set = target as Image;
            if (set != null) Instance.FadeInForImage(set, speed);

            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null) Instance.FadeInForSprite(sprite, speed);

            Material material = target as Material;
            if (material != null) Instance.FadeInForMaterial(material, speed);

            else if (set == null && sprite == null && material != null)
            {
                Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image, Sprite Ç‹ÇΩÇÕ Material Ç≈Ç∑ÅB");
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

                Material material = e.Current as Material;
                if (material != null) Instance.FadeInForMaterial(material, speed);

                else if (set == null && sprite == null && material == null)
                {
                    Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image, Sprite Ç‹ÇΩÇÕ Material Ç≈Ç∑ÅB");
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

                Material material = e.Current as Material;
                if (material != null) Instance._setMaterials.Add(material);

                else if (set == null && sprite == null)
                {
                    Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image, Sprite Ç‹ÇΩÇÕ Material Ç≈Ç∑ÅB");
                    return;
                }
            }

            if (Instance._setImage.Count > 0) Instance.FadeInForImage(null, speed);
            else if (Instance._setSprites.Count > 0) Instance.FadeInForSprite(null, speed);
            else if (Instance._setMaterials.Count > 0) Instance.FadeInForMaterial(null, speed);
        }

        public static void OutSingle<T>(T target, float speed) where T : class
        {
            Image set = target as Image;
            if (set != null) Instance.FadeOutForImage(set, speed);

            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null) Instance.FadeOutForSprite(sprite, speed);

            Material material = target as Material;
            if (material != null) Instance.FadeOutForMaterial(material, speed);

            else if (set == null && sprite == null && material == null)
            {
                Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image, Sprite Ç‹ÇΩÇÕ Material Ç≈Ç∑ÅB");
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

                Material material = e.Current as Material;
                if (material != null) Instance.FadeOutForMaterial(material, speed);

                else if (set == null && sprite == null)
                {
                    Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image, Sprite Ç‹ÇΩÇÕ Material Ç≈Ç∑ÅB");
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

                Material material = e.Current as Material;
                if (material != null) Instance._setMaterials.Add(material);

                else if (set == null && sprite == null && material == null)
                {
                    Debug.LogError("ì¸ÇÍÇÈå^Ç™çáÇ¡ÇƒÇ‹ÇπÇÒÅB å^ñºÇÕ Image, Sprite Ç‹ÇΩÇÕ Material Ç≈Ç∑ÅB");
                    return;
                }
            }

            if (Instance._setImage.Count > 0) Instance.FadeOutForImage(null, speed);
            else if (Instance._setSprites.Count > 0) Instance.FadeOutForSprite(null, speed);
            else if (Instance._setMaterials.Count > 0) Instance.FadeOutForMaterial(null, speed);
        }

        public static void ImageCrossFade(Image before, Image after, float speed)
            => Instance.FadeCrossForImage(before, after, speed);
        public static void SpriteCrossFade(SpriteRenderer before, SpriteRenderer after, float speed)
            => Instance.FadeCrossForSprite(before, after, speed);

        public static void FadeBreak() => Instance._break = true;

        #region èàóù
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

        void FadeInForMaterial(Material target = null, float speed = 0)
        {
            SetFadeInParam(speed);

            if (Instance._async) StartCoroutine(SetAsyncForMaterial(_setMaterials));
            else StartCoroutine(FadeToMaterial(target));
        }
        void FadeOutForMaterial(Material target = null, float speed = 0)
        {
            SetFadeOutParam(speed);

            if (Instance._async) StartCoroutine(SetAsyncForMaterial(_setMaterials));
            else StartCoroutine(FadeToMaterial(target));
        }

        void FadeCrossForImage(Image before, Image after, float speed)
        {
            Instance._startVal = 1;
            Instance._endVal = 0;
            Instance._fadeSpeed = speed;

            StartCoroutine(FadeCrossToImage(before, after));
        }
        void FadeCrossForSprite(SpriteRenderer before, SpriteRenderer after, float speed)
        {
            Instance._startVal = 1;
            Instance._endVal = 0;
            Instance._fadeSpeed = speed;

            StartCoroutine(FadeCrossToSprite(before, after));
        }

        void SetFadeInParam(float speed)
        {
            Instance._startVal = 0;
            Instance._endVal = 1;
            Instance._fadeSpeed = speed;
            if (Instance._break) Instance._break = false;
        }
        void SetFadeOutParam(float speed)
        {
            Instance._startVal = 1;
            Instance._endVal = 0;
            Instance._fadeSpeed = speed;
            if (Instance._break) Instance._break = false;
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
        IEnumerator SetAsyncForMaterial(List<Material> targets)
        {
            int count = 0;
            while (count <= targets.Count - 1)
            {
                if (!_currentFade)
                {
                    yield return FadeToMaterial(targets[count]);
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

                if (_break)
                {
                    set.color = new Color(set.color.r, set.color.g, set.color.b, _endVal);
                    _currentFade = false;
                    yield break;
                }

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

                if (_break)
                {
                    set.color = new Color(set.color.r, set.color.g, set.color.b, _endVal);
                    _currentFade = false;
                    yield break;
                }

                if (alfa == _endVal) isFade = true;
                yield return null;
            }

            _currentFade = false;
            _currentTime = 0;

            if (!_async) ResetParam();
        }
        IEnumerator FadeToMaterial(Material set)
        {
            _currentFade = true;
            bool isFade = false;
            while (!isFade)
            {
                _currentTime += Time.deltaTime;
                float rate = _currentTime / _fadeSpeed;
                float alfa = Mathf.Lerp(_startVal, _endVal, rate);

                set.color = new Color(set.color.r, set.color.g, set.color.b, alfa);

                if (_break)
                {
                    set.color = new Color(set.color.r, set.color.g, set.color.b, _endVal);
                    _currentFade = false;
                    yield break;
                }

                if (alfa == _endVal) isFade = true;
                yield return null;
            }

            _currentFade = false;
            _currentTime = 0;

            if (!_async) ResetParam();
        }

        IEnumerator FadeCrossToImage(Image before, Image after)
        {
            _currentFade = true;
            bool isFade = false;
            while (!isFade)
            {
                _currentTime += Time.deltaTime;
                float rate = _currentTime / _fadeSpeed;
                float alfa = Mathf.Lerp(_startVal, _endVal, rate);
                float alfa2 = Mathf.Lerp(_endVal, _startVal, rate);

                before.color = new Color(before.color.r, before.color.g, before.color.b, alfa);
                after.color = new Color(after.color.r, after.color.g, after.color.b, alfa2);

                if (_break)
                {
                    before.color = new Color(before.color.r, before.color.g, before.color.b, 0);
                    after.color = new Color(after.color.r, after.color.g, after.color.b, 1);
                    ResetParam();
                    yield break;
                }

                if (alfa == _endVal && alfa2 == _startVal) isFade = true;
                yield return null;
            }

            _currentFade = false;
            _currentTime = 0;

            ResetParam();
        }
        IEnumerator FadeCrossToSprite(SpriteRenderer before, SpriteRenderer after)
        {
            _currentFade = true;
            bool isFade = false;
            while (!isFade)
            {
                _currentTime += Time.deltaTime;
                float rate = _currentTime / _fadeSpeed;
                float alfa = Mathf.Lerp(_startVal, _endVal, rate);
                float alfa2 = Mathf.Lerp(_endVal, _startVal, rate);

                before.color = new Color(before.color.r, before.color.g, before.color.b, alfa);
                after.color = new Color(after.color.r, after.color.g, after.color.b, alfa2);

                if (_break)
                {
                    before.color = new Color(before.color.r, before.color.g, before.color.b, 0);
                    after.color = new Color(after.color.r, after.color.g, after.color.b, 1);
                    ResetParam();
                    yield break;
                }

                if (alfa == _endVal && alfa2 == _startVal) isFade = true;
                yield return null;
            }

            _currentFade = false;
            _currentTime = 0;

            ResetParam();
        }

        void ResetParam()
        {
            _currentFade = false;
            _async = false;
            _currentTime = 0;
            _setImage = new List<Image>();
            _setSprites = new List<SpriteRenderer>();
            _setMaterials = new List<Material>();
            _break = false;
        }
        #endregion
    }
}
