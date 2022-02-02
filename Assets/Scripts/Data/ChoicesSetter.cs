using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesSetter : MonoBehaviour
{
    [SerializeField] GameObject _buttonPrefabs;

    public void Set(List<ChoicesData> datas, int id)
    {
        for (int count = 0; count < datas[id].ChoicesCount; count++)
        {
            string text = datas[id].MSG;
            GameObject obj = Instantiate(_buttonPrefabs, transform);
            obj.GetComponent<Text>().text = text;
            //obj.GetComponent<Button>().onClick.AddListener
        }
    }
}
