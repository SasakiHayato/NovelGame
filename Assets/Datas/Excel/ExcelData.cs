using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ExcelData : ScriptableObject
{
    public List<TalkData> Test = new List<TalkData>();
}

[Serializable]
public class TalkData
{
    public int ID;
    public string MSG;
    public string CharaID;
    public bool CharaFadeSync;
    public string Talk;
    public string Postion;
    public string BackGroundID;
    public string GetChoicesData;
}


