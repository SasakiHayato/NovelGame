using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] UIManager _ui;
    [SerializeField] DataSettings _settings;
    [SerializeField] TalkName _talkName;
    [SerializeField] ChoiceDataName _choiceName;
    
    Coroutine _setCoroutine;
    
    private void Awake()
    {
        Instantiate(_ui.gameObject);
        Instantiate(_settings.gameObject);
        _settings.SetUp();
        _settings.Init();
        _settings.TalkName = _talkName;
        _settings.ChoiceName = _choiceName;
    }

    void Update()
    {
       
        if (Input.GetButtonDown("Fire1") && _settings.ChoiceFlag)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && !_settings.IsMove)
        {
            _settings.SetDatas();
            _setCoroutine = StartCoroutine(CurrentSetting());
        }
        else if (Input.GetButtonDown("Fire1") && _settings.IsMove)
        {
            StopCoroutine(_setCoroutine);
            _settings.Break();
            StartCoroutine(IsBreak());
            Debug.Log("SetBreak");
        }
    }

    IEnumerator CurrentSetting()
    {
        yield return UIManager.IsEnd();
        Debug.Log("IsSetting UIData");
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        Debug.Log("IsPressed. GetNextData");
        _settings.IsMove = false;
    }

    IEnumerator IsBreak()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        Debug.Log("IsPressed. GetNextData");
        _settings.IsMove = false;
        UIManager.Break();
    }
}
