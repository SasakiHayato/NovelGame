using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleFade;

public class UIManager : MonoBehaviour
{
    [SerializeField] float _txtWaitTime = 0.5f;

    private static UIManager _instance = null;
    public static UIManager Instance => _instance;

    TextCtrl _txt;

    Text _msgTxt;
    Text _nameTxt;

    SpriteRenderer _sprite1;
    SpriteRenderer _sprite2;

    int _saveCharaID1 = int.MaxValue;
    int _saveCharaID2 = int.MaxValue;

    private void Awake()
    {
        _instance = this;

        _msgTxt = GameObject.Find("MSG").GetComponent<Text>();
        _nameTxt = GameObject.Find("Name").GetComponent<Text>();
        
        _txt = gameObject.AddComponent<TextCtrl>();
        _txt.WaitTime = _txtWaitTime;

        _sprite1 = GameObject.Find("Chara1").GetComponent<SpriteRenderer>();
        _sprite1.sprite = null;
        _sprite2 = GameObject.Find("Chara2").GetComponent<SpriteRenderer>();
        _sprite2.sprite = null;
    }

    public static void Init()
    {
        Fade.FadeBreakAll();
        Instance._txt.Break();
    }

    public static IEnumerator IsEnd()
    {
        yield return new WaitUntil(() => Instance._txt.IsEnd);
        //Debug.Log("End SetText");
        yield return new WaitUntil(() => Fade.EndFade);
        //Debug.Log("End Fade");
    }

    public static void SetName(string name) => Instance._nameTxt.text = name;
    public static void SetMSG(string msg) => Instance._txt.Set(msg, Instance._msgTxt);
    public static void SetSprite(Sprite sprite, int charaID)
    {
        Instance._sprite1.sprite = sprite;
        if (charaID != Instance._saveCharaID1)
        {
            Instance._saveCharaID1 = charaID;
            Fade.InSingle(Instance._sprite1, 1);
        }
    }
}
