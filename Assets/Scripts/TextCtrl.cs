using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCtrl : MonoBehaviour
{
    public float WaitTime { private get; set; } = 0;
    float _currentTime = 0;

    public void Set(string msg, Text text)
    {
        if (msg == "Init")
        {
            text.text = "";
            return;
        }
        
        StartCoroutine(TextUpdate(msg, text));
    }

    IEnumerator TextUpdate(string msg, Text text)
    {
        text.text = "";
        for (int count = 0; count < msg.Length; count++)
        {
            text.text += msg[count];

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
    }
}
