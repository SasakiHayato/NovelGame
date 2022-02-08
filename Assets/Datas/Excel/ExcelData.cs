using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public enum TalkName
{
    Test,
    Test2,
    Test3,
}

[ExcelAsset]
public class ExcelData : ScriptableObject
{
    public List<TalkData> Test = new List<TalkData>();
    public List<TalkData> Test2 = new List<TalkData>();
    public List<TalkData> Test3 = new List<TalkData>();

    public List<TalkData> GetTalkData(TalkName name)
    {
        switch (name)
        {
            case TalkName.Test:
                return Test;
            case TalkName.Test2:
                return Test2;
            case TalkName.Test3:
                return Test3;

            default: return null;
        }
    }
}


