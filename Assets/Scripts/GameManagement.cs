using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] UIManager _ui;
    [SerializeField] DataSettings _settings;

    private void Awake()
    {
        Instantiate(_ui.gameObject);
        Instantiate(_settings.gameObject);
        _settings.Init();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) _settings.SetDatas();
    }
}
