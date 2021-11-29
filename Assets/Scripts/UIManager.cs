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

    bool _isEnd;

    private void Awake()
    {
        _instance = this;

        _msgTxt = GameObject.Find("MSG").GetComponent<Text>();
        _nameTxt = GameObject.Find("Name").GetComponent<Text>();
        
        _txt = gameObject.AddComponent<TextCtrl>();
        _txt.WaitTime = _txtWaitTime;
    }

    public static IEnumerator IsMove()
    {
        yield return null;
    }

    public static void SetName(string name) => Instance._nameTxt.text = name;
    public static void SetMSG(string msg) => Instance._txt.Set(msg, Instance._msgTxt);

    public static bool IsEndUI() => Instance._isEnd;
}
