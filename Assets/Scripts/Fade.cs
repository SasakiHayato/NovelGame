using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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

        #region メンバ変数
        float _startVal = 0;
        float _endVal = 0;
        float _fadeSpeed = 0;

        bool _currentFade = false;
        bool _async = false;
        bool _isFade = false;

        bool _break = false;

        Image _thisImage = null;
        Action _action = null;

        List<Image> _setImage = new List<Image>();
        List<SpriteRenderer> _setSprites = new List<SpriteRenderer>();
        List<Material> _setMaterials = new List<Material>();
        #endregion

        /// <summary>
        /// 単体に対する同期的なフェードイン。
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="target">対象となるObject</param>
        /// <param name="speed">フェードする割合</param>
        /// <param name="start">初期値</param>
        /// <param name="end">終了値</param>
        public static void InSingle<T>(T target, float speed, float start = 0, float end = 1) where T : class
        {
            Instance.SetFadeParam(speed, start, end);
            Image set = target as Image;
            if (set != null) Instance.FadeForImage(set);

            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null) Instance.FadeForSprite(sprite);

            Material material = target as Material;
            if (material != null) Instance.FadeForMaterial(material);

            else if (set == null && sprite == null && material != null)
            {
                Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                return;
            }
        }

        /// <summary>
        /// 単体に対する非同期的なフェードイン
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="target">対象となるオブジェクト</param>
        /// <param name="speed">フェードする割合</param>
        /// <param name="strart">初期値</param>
        /// <param name="end">終了値</param>
        /// <returns>フェード</returns>
        public static IEnumerator InSingleAsync<T>(T target, float speed, float strart = 0, float end = 1)
        {
            Instance._async = true;
            Instance.SetFadeParam(speed, strart, end);
            Image set = target as Image;
            if (set != null)
            {
                Instance._setImage.Add(set);
                yield return Instance.SetAsyncForImage(Instance._setImage);
            }
            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null)
            {
                Instance._setSprites.Add(sprite);
                yield return Instance.SetAsyncForSprite(Instance._setSprites);
            }

            Material material = target as Material;
            if (material != null)
            {
                Instance._setMaterials.Add(material);
                yield return Instance.SetAsyncForMaterial(Instance._setMaterials);
            }
            else if (set == null && sprite == null && material != null)
            {
                Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                yield return null;
            }
        }

        /// <summary>
        /// 複数に対する同期的なフェードイン。
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="targets">対象となるObject</param>
        /// <param name="speed">フェードする割合</param>
        /// <param name="start">初期値</param>
        /// <param name="end">終了値</param>
        public static void InMultipe<T>(IEnumerable<T> targets, float speed, float start = 0, float end = 1) where T : class
        {
            Instance.SetFadeParam(speed, start, end);
            IEnumerator e = targets.GetEnumerator();
            while (e.MoveNext())
            {
                Image set = e.Current as Image;
                if (set != null) Instance.FadeForImage(set);

                SpriteRenderer sprite = e.Current as SpriteRenderer;
                if (sprite != null) Instance.FadeForSprite(sprite);

                Material material = e.Current as Material;
                if (material != null) Instance.FadeForMaterial(material);

                else if (set == null && sprite == null && material == null)
                {
                    Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                    return;
                }
            }
        }

        /// <summary>
        /// 複数に対する非同期的なフェードイン。
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="targets">対象となるObject</param>
        /// <param name="speed">フェードする割合</param>
        /// /// <param name="start">初期値</param>
        /// <param name="end">終了値</param>
        public static void InMultipeAsync<T>(IEnumerable<T> targets, float speed, float start = 0, float end = 1) where T : class
        {
            Instance._async = true;
            Instance.SetFadeParam(speed, start, end);
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
                    Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                    return;
                }
            }

            if (Instance._setImage.Count > 0) Instance.FadeForImage(null);
            else if (Instance._setSprites.Count > 0) Instance.FadeForSprite(null);
            else if (Instance._setMaterials.Count > 0) Instance.FadeForMaterial(null);
        }

        /// <summary>
        /// 単体に対する同期的なフェードアウト
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="target">対象となるObject</param>
        /// <param name="speed">フェードする割合</param>
        /// <param name="start">初期値</param>
        /// <param name="end">終了値</param>
        public static void OutSingle<T>(T target, float speed, float start = 1, float end = 0) where T : class
        {
            Instance.SetFadeParam(speed, start, end);
            Image set = target as Image;
            if (set != null) Instance.FadeForImage(set);

            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null) Instance.FadeForSprite(sprite);

            Material material = target as Material;
            if (material != null) Instance.FadeForMaterial(material);

            else if (set == null && sprite == null && material == null)
            {
                Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                return;
            }
        }

        /// <summary>
        /// 単体に対する非同期的なフェードアウト
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="target">対象となるObject</param>
        /// <param name="speed">フェードする割合</param>
        /// /// <param name="start">初期値</param>
        /// <param name="end">終了値</param>
        /// <returns>フェード</returns>
        public static IEnumerator OutSingleAsync<T>(T target, float speed, float start = 1, float end = 0) where T : class
        {
            Instance._async = true;
            Instance.SetFadeParam(speed, start, end);
            Image set = target as Image;
            if (set != null)
            {
                Instance._setImage.Add(set);
                yield return Instance.SetAsyncForImage(Instance._setImage);
            }
            SpriteRenderer sprite = target as SpriteRenderer;
            if (sprite != null)
            {
                Instance._setSprites.Add(sprite);
                yield return Instance.SetAsyncForSprite(Instance._setSprites);
            }

            Material material = target as Material;
            if (material != null)
            {
                Instance._setMaterials.Add(material);
                yield return Instance.SetAsyncForMaterial(Instance._setMaterials);
            }
            else if (set == null && sprite == null && material != null)
            {
                Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                yield return null;
            }
        }

        /// <summary>
        /// 複数に対する同期的なフェードアウト
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="targets">対象となるObject</param>
        /// <param name="speed">フェードする割合</param>
        /// <param name="start">初期値</param>
        /// <param name="end">終了値</param>
        public static void OutMultipe<T>(IEnumerable<T> targets, float speed, float start = 1, float end = 0) where T : class
        {
            Instance.SetFadeParam(speed, start, end);
            IEnumerator e = targets.GetEnumerator();
            while (e.MoveNext())
            {
                Image set = e.Current as Image;
                if (set != null) Instance.FadeForImage(set);

                SpriteRenderer sprite = e.Current as SpriteRenderer;
                if (sprite != null) Instance.FadeForSprite(sprite);

                Material material = e.Current as Material;
                if (material != null) Instance.FadeForMaterial(material);

                else if (set == null && sprite == null && material == null)
                {
                    Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                    return;
                }
            }
        }

        /// <summary>
        /// 複数に対する非同期的なフェードアウト
        /// </summary>
        /// <typeparam name="T">Image, SpriteRenderer, or Material</typeparam>
        /// <param name="targets">対象となるObject</param>
        /// <param name="speed">フェードする割合</param>
        /// <param name="start">初期値</param>
        /// <param name="end">終了値</param>
        public static void OutMultipeAsync<T>(IEnumerable<T> targets, float speed, float start = 1, float end = 0) where T : class
        {
            Instance._async = true;
            Instance.SetFadeParam(speed, start, end);
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
                    Debug.LogError("入れる型が合ってません。 型名は Image, Sprite または Material です。");
                    return;
                }
            }

            if (Instance._setImage.Count > 0) Instance.FadeForImage(null);
            else if (Instance._setSprites.Count > 0) Instance.FadeForSprite(null);
            else if (Instance._setMaterials.Count > 0) Instance.FadeForMaterial(null);
        }

        /// <summary>
        /// ImageCompornentに対するクロスフェード
        /// </summary>
        /// <param name="before">現在のImage</param>
        /// <param name="after">遷移後のImage</param>
        /// <param name="speed">フェードする割合</param>
        public static void ImageCrossFade(Image before, Image after, float speed)
            => Instance.FadeCrossForImage(before, after, speed);

        /// <summary>
        /// SpriteRendererに対するクロスフェード
        /// </summary>
        /// <param name="before">現在のSpriteRenderer</param>
        /// <param name="after">遷移後のSpriteRenderer</param>
        /// <param name="speed">フェードする割合</param>
        public static void SpriteCrossFade(SpriteRenderer before, SpriteRenderer after, float speed)
            => Instance.FadeCrossForSprite(before, after, speed);

        /// <summary>
        /// fade用のImageの生成
        /// </summary>
        /// <returns>FadeImage</returns>
        public static Image CreateFadeImage()
        {
            if (Instance._thisImage != null)
            {
                Destroy(GameObject.Find("FadeCanvas"));
                Instance._thisImage = null;
            }
            if (Instance._thisImage == null)
            {
                Canvas canvas = new GameObject("FadeCanvas").AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 10;
                CanvasScaler scaler = canvas.gameObject.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1000, 1600);

                RectTransform rect = new GameObject("FadeImage")
                    .AddComponent<Image>().GetComponent<RectTransform>();
                rect.transform.SetParent(canvas.transform);
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                rect.transform.localPosition = Vector2.zero;
                rect.GetComponent<Image>().color = Color.black;
                Instance._thisImage = rect.GetComponent<Image>();
            }

            return Instance._thisImage;
        }

        /// <summary>
        /// 現在行われているすべてのフェードを強制終了
        /// </summary>
        public static void FadeBreakAll() => Instance._break = true;

        /// <summary>
        /// フェードの終了を知らせる
        /// </summary>
        /// <returns>true</returns>
        public static bool EndFade { get => Instance._isFade; }

        public static string FageCanvasName { get => "FadeCanvas"; }
        public static string FadeImageName { get => "FadeImage"; }

        #region 処理
        void FadeForImage(Image target = null)
        {
            if (Instance._async) StartCoroutine(SetAsyncForImage(_setImage));
            else StartCoroutine(FadeToImage(target));
        }
        void FadeForSprite(SpriteRenderer target = null)
        {
            if (Instance._async) StartCoroutine(SetAsyncForSprite(_setSprites));
            else StartCoroutine(FadeToSprite(target));
        }
        void FadeForMaterial(Material target = null)
        {
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

        void SetFadeParam(float speed, float start, float end)
        {
            _isFade = false;
            Instance._startVal = start;
            Instance._endVal = end;
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

            _isFade = true;
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

            _isFade = true;
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
            float currentTime = 0;
            while (!isFade)
            {
                currentTime += Time.deltaTime;
                float rate = currentTime / _fadeSpeed;
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

            if (!_async)
            {
                _isFade = true;
                ResetParam();

            }
        }
        IEnumerator FadeToSprite(SpriteRenderer set)
        {
            _currentFade = true;
            bool isFade = false;
            float currentTime = 0;
            while (!isFade)
            {
                currentTime += Time.deltaTime;
                float rate = currentTime / _fadeSpeed;
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

            if (!_async)
            {
                _isFade = true;
                ResetParam();

            }
        }
        IEnumerator FadeToMaterial(Material set)
        {
            _currentFade = true;
            bool isFade = false;
            float currentTime = 0;
            while (!isFade)
            {
                currentTime += Time.deltaTime;
                float rate = currentTime / _fadeSpeed;
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

            if (!_async)
            {
                _isFade = true;
                ResetParam();

            }
        }

        IEnumerator FadeCrossToImage(Image before, Image after)
        {
            _currentFade = true;
            bool isFade = false;
            float currentTime = 0;
            while (!isFade)
            {
                currentTime += Time.deltaTime;
                float rate = currentTime / _fadeSpeed;
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

            _isFade = true;
            ResetParam();
        }
        IEnumerator FadeCrossToSprite(SpriteRenderer before, SpriteRenderer after)
        {
            _currentFade = true;
            bool isFade = false;
            float currentTime = 0;
            while (!isFade)
            {
                currentTime += Time.deltaTime;
                float rate = currentTime / _fadeSpeed;
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

            _isFade = true;
            ResetParam();
        }

        void ResetParam()
        {

            _currentFade = true;
            _async = false;
            _setImage = new List<Image>();
            _setSprites = new List<SpriteRenderer>();
            _setMaterials = new List<Material>();
            _break = false;
        }
        #endregion
    }
}
