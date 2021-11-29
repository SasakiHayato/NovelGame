using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCtrl : MonoBehaviour
{
    public void Set(string msg, Text text)
    {
        if (msg == "Init") text.text = "";
        else TextUpdate();
    }

    void TextUpdate()
    {

    }
}
