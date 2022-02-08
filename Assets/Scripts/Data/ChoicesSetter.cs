using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesSetter : MonoBehaviour
{
    [SerializeField] GameObject _buttonPrefabs;
    DataSettings _settings;

    public void SetUp(DataSettings dataSettings)
    {
        _settings = dataSettings;
    }

    public void Set(List<ChoicesData> datas)
    {
        for (int count = 0; count < datas[0].ChoicesCount; count++)
        {
            string text = datas[count].MSG;
            GameObject obj = Instantiate(_buttonPrefabs, transform);
            obj.GetComponentInChildren<Text>().text = text;
            ButtonManage button = obj.AddComponent<ButtonManage>();
            button.SetName = datas[count].NextTalkData;
            button.DataSettings = _settings;
            obj.GetComponent<Button>().onClick.AddListener(button.Call);
        }
    }
}