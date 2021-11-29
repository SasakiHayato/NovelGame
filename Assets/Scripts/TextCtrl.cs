using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCtrl : MonoBehaviour
{
    public float WaitTime { private get; set; } = 0;
    public bool IsEnd { get; private set; } = false;

    int _setId = 0;
    int _insertCount = 0;
    float _currentTime = 0;
    string[] _saveColor;

    Text _setTxt;
    bool _colorSetUp;

    public void Set(string msg, Text text)
    {
        _saveColor = new string[3];
        IsEnd = false;

        if (msg == "Init")
        {
            text.text = "";
            _setTxt = text;
            return;
        }
        
        StartCoroutine(TextUpdate(msg));
    }

    IEnumerator TextUpdate(string msg)
    {
        _setTxt.text = "";
        string check = "<";
        _colorSetUp = false;
        int insertCount = 0;

        for (int count = 0; count < msg.Length; count++)
        {
            
            if (msg[count] == check[0]) _colorSetUp = true;
            if (_colorSetUp)
            {
                SetColor(msg[count], ref count);
                continue;
            }

            if (_saveColor[1] == null)
            {
                _setTxt.text += msg[count];
            }
            else
            {
                count--;
                if (insertCount == _insertCount)
                {
                    _saveColor = new string[3];
                    insertCount = 0;
                    _insertCount = 0;
                    continue;
                }

                string insert = _saveColor[1][insertCount].ToString();
                _setTxt.text = _setTxt.text.Insert(_setTxt.text.LastIndexOf(_saveColor[2]), insert);
                insertCount++;
            }
            
            
            bool wait = false;
            while (!wait)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= WaitTime)
                {
                    wait = true;
                    _currentTime = 0;
                }
                yield return null;
            }
        }

        IsEnd = true;
    }
    
    void SetColor(char set, ref int count)
    {
        string check = ">";
        string check2 = "<";

        switch (_setId)
        {
            case 0:
                _saveColor[0] += set;
                if (check[0] == _saveColor[0][_saveColor[0].Length - 1]) _setId++;
                break;
            case 1:
                _saveColor[1] += set;
                _insertCount++;
                if (check2[0] == _saveColor[1][_saveColor[1].Length - 1])
                {
                    _saveColor[1] = _saveColor[1].Remove(_saveColor[1].Length - 1, 1);
                    _saveColor[2] += set;
                    _setId++;
                    _insertCount--;
                }
                break;
            case 2:
                _saveColor[2] += set;
                if (check[0] == _saveColor[2][_saveColor[2].Length - 1]) _setId++;
                break;
            case 3:
                _setTxt.text += _saveColor[0];
                _setTxt.text += _saveColor[2];
                _setId = 0;
                count--;
                _colorSetUp = false;
                break;
        }
    }
}
