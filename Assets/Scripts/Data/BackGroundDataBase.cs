using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackGroundDatas")]
public class BackGroundDataBase : ScriptableObject
{
    public List<BGData> BGDatas = new List<BGData>();
}

[System.Serializable]
public class BGData
{
    public string Name;
    public int ID;
    public Sprite Sprite;
}
