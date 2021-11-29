using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (fileName = "CharaData")]
public class CharaDataBase : ScriptableObject
{
    public List<CharaData> CharaData = new List<CharaData>();
}

[Serializable]
public class CharaData
{
    public string Name;
    public string ID;
    public Sprite[] FaceID;
}
