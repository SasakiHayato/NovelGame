using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSettings : MonoBehaviour
{
    [SerializeField] ExcelData _excel;
    [SerializeField] CharaDataBase _chara;

    SpriteRenderer _sprite1;
    SpriteRenderer _sprite2;

    int _id = 0;

    public bool IsMove { get; set; } = false;

    public void Init()
    {
        IsMove = false;

        UIManager.SetName("");
        UIManager.SetMSG("Init");

        _sprite1 = GameObject.Find("Chara1").GetComponent<SpriteRenderer>();
        _sprite1.sprite = null;
        _sprite2 = GameObject.Find("Chara2").GetComponent<SpriteRenderer>();
        _sprite2.sprite = null;
    }

    public void SetDatas()
    {
        if (IsMove) Break();
        else IsMove = true;

        Data data = _excel.Test[_id];
        string name = "Null";
        foreach (CharaData chara in _chara.CharaData)
        {
            if (data.CharaID.ToString("d2") == chara.ID)
            {
                name = chara.Name;
                _sprite1.sprite = chara.FaceID[0];
                
                break;
            }
        }
        
        UIManager.SetName(name);
        UIManager.SetMSG(data.MSG);
    }
    
    void Break()
    {
        
    }
}
