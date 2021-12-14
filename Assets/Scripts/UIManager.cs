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

    SpriteRenderer _spRder1;
    SpriteRenderer _spRder2;

    FadeSetter _fade;

    private void Awake()
    {
        _instance = this;

        _msgTxt = GameObject.Find("MSG").GetComponent<Text>();
        _nameTxt = GameObject.Find("Name").GetComponent<Text>();
        
        _txt = gameObject.AddComponent<TextCtrl>();
        _txt.WaitTime = _txtWaitTime;

        _fade = gameObject.AddComponent<FadeSetter>();

        _spRder1 = GameObject.Find("Chara1").GetComponent<SpriteRenderer>();
        _spRder1.sprite = null;
        _spRder2 = GameObject.Find("Chara2").GetComponent<SpriteRenderer>();
        _spRder2.sprite = null;
    }

    public static void Init()
    {
        Instance._txt.Break();
        Fade.Break();
    }

    public static IEnumerator IsEnd()
    {
        yield return new WaitUntil(() => Instance._txt.IsEnd);
        Debug.Log("End SetText");
    }

    public static void SetName(string name) => Instance._nameTxt.text = name;
    public static void SetMSG(string msg) => Instance._txt.SetText(msg, Instance._msgTxt);
    public static void SetSprite(Sprite sprite, int fadeType, int postion)
    {
        switch (postion)
        {
            case 1:
                Instance._spRder1.sprite = sprite;
                Instance._fade.SetFadeIDToRenderer(fadeType, Instance._spRder1);
                return;
            case 2:
                Instance._spRder2.sprite = sprite;
                Instance._fade.SetFadeIDToRenderer(fadeType, Instance._spRder2);
                return;
        }
    }
}
