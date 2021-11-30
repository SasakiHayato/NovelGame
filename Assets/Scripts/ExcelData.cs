using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ExcelData : ScriptableObject
{
    public List<Data> Test = new List<Data>(); // Replace 'EntityType' to an actual type that is serializable.
}

[Serializable]
public class Data
{
    public int ID;
    public string MSG;
    public string CharaID;
    public bool FadeSync;
}

