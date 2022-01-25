using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] float _txtWaitTime = 0.5f;

    private static UIManager _instance = null;
    public static UIManager Instance => _instance;

    TextCtrl _txt;

    Text _msgTxt;
    Text _nameTxt;

    SpriteRenderer[] _spRder1;
    SpriteRenderer[] _spRder2;

    SpriteRenderer _back;

    FadeSetter _fade;

    private void Awake()
    {
        _instance = this;

        _msgTxt = GameObject.Find("MSG").GetComponent<Text>();
        _nameTxt = GameObject.Find("Name").GetComponent<Text>();
        
        _txt = gameObject.AddComponent<TextCtrl>();
        _txt.WaitTime = _txtWaitTime;

        _fade = gameObject.AddComponent<FadeSetter>();

        _spRder1 = GameObject.Find("Chara1").GetComponentsInChildren<SpriteRenderer>();
        _spRder1[0].sprite = null;
        _fade.AddSpriteRederer(_spRder1[1], 1);

        _spRder2 = GameObject.Find("Chara2").GetComponentsInChildren<SpriteRenderer>();
        _spRder2[0].sprite = null;
        _fade.AddSpriteRederer(_spRder2[1], 2);

        _back = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        _back.sprite = null;
    }

    public static void Break()
    {
        Instance._txt.Break();
        Fade.Break();
        Instance._fade.SaveSpriteRederer(Instance._spRder1[0], 1);
        Instance._fade.SaveSpriteRederer(Instance._spRder2[0], 2);
    }

    public static IEnumerator IsEnd()
    {
        yield return new WaitUntil(() => Instance._txt.IsEnd);
        Debug.Log("End SetText");
        yield return new WaitUntil(() => Fade.End());
        Instance._fade.SaveSpriteRederer(Instance._spRder1[0], 1);
        Instance._fade.SaveSpriteRederer(Instance._spRder2[0], 2);
        Debug.Log("EndFade");
    }

    public static void SetName(string name) => Instance._nameTxt.text = name;
    public static void SetMSG(string msg) => Instance._txt.SetText(msg, Instance._msgTxt);
    public static void SetSprite(Sprite sprite, int fadeType, int postion)
    {
        switch (postion)
        {
            case 1:
                Instance._spRder1[0].sprite = sprite;
                Instance._fade.SetFadeIDToRenderer(fadeType, Instance._spRder1[0], postion);
                return;
            case 2:
                Instance._spRder2[0].sprite = sprite;
                Instance._fade.SetFadeIDToRenderer(fadeType, Instance._spRder2[0], postion);
                return;
        }
    }

    public static void SpriteAsyncSetter(Sprite[] sprite, int[] fadeType, string[] postion, int count)
    {
        Instance.StartCoroutine(Instance.SetSpriteAsync(sprite, fadeType, postion, count));
    }

    IEnumerator SetSpriteAsync(Sprite[] sprite, int[] fadeType, string[] postion, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetSprite(sprite[i], fadeType[i], int.Parse(postion[i]));
            yield return new WaitUntil(() => Fade.End());
        }
    }

    public static void SetBackGround(Sprite sprite, int fadeType)
    {
        Instance._back.sprite = sprite;
        Instance._fade.SetFadeIDToRenderer(fadeType, Instance._back);
    }
}
