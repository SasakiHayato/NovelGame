using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleFade;

public class DataSettings : MonoBehaviour
{
    [SerializeField] ExcelData _excel;
    [SerializeField] CharaDataBase _chara;

    int _id = 0;

    Dictionary<string, string> _charaDic;

    public bool IsMove { get; set; } = false;

    public void Init()
    {
        IsMove = false;
        _id = 0;

        UIManager.SetName("");
        UIManager.SetMSG("Init");
    }

    public void SetDatas()
    {
        if (IsMove) return;
        else IsMove = true;
        
        if (_id == _excel.Test.Count)
        {
            Debug.Log("EndExcel");
            Init();
            return;
        }
        _charaDic = new Dictionary<string, string>();

        Data data = _excel.Test[_id];
        string name = "Null";
        Sprite sprite = null;
        int charaID = 0;
        string getCharaData = data.CharaID.ToString();

        //foreach (CharaData chara in _chara.CharaData)
        //{
        //    if (data.CharaID.ToString("d2") == chara.ID)
        //    {
        //        name = chara.Name;
        //        sprite = chara.FaceID[data.SpriteID];
        //        charaID = int.Parse(chara.ID.ToString());
        //        break;
        //    }
        //}

        UIManager.SetName(name);
        UIManager.SetMSG(data.MSG);
        UIManager.SetSprite(sprite, charaID);

        _id++;
    }
    
    public void Break()
    {
        UIManager.Init();

        if (_id == _excel.Test.Count)
        {
            Debug.Log("EndExcel");
            Init();
            return;
        }
    }
}
