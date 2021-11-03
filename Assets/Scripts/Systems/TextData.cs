using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class TextData : ScriptableObject
{
    public List<Data> m_prologue = new List<Data>();
    public List<Data> m_ending = new List<Data>();
    public List<Data> m_test = new List<Data>();
}

[Serializable]
public class Data
{
    public string m_charaId;
    public string m_msg;
    public bool m_isEvent;
    public string m_pos;
    public string m_selector;
    public int m_backGroundId;
}

public enum EventType
{
    Opening,
    Ending,

    Test,

    None,
}

public class Opening : IData
{
    static TextData m_data;

    public TextData SetTextData(TextData set) => m_data = set;
    public Data GetData(int id) => m_data.m_prologue[id];
}

public class Ending : IData
{
    static TextData m_data;

    public TextData SetTextData(TextData set) => m_data = set;
    public Data GetData(int id) => m_data.m_ending[id];
}

public class Test : IData
{
    static TextData m_data;

    public TextData SetTextData(TextData set) => m_data = set;
    public Data GetData(int id) => m_data.m_test[id];
}

