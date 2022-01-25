using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSettings : MonoBehaviour
{
    [SerializeField] ExcelData _excel;
    [SerializeField] CharaDataBase _chara;
    [SerializeField] BackGroundDataBase _back;

    int _id = 0;
    public bool IsMove { get; set; } = false;

    public void Init()
    {
        IsMove = false;
        _id = 0;

        UIManager.SetName("");
        UIManager.SetMSG("Init");
        for (int i = 1; i <= 2; i++)
            UIManager.SetSprite(null, 3, i);
    }

    public void SetDatas()
    {
        if (IsMove) return;
        else IsMove = true;
        
        if (_id == _excel.Test.Count - 1)
        {
            Init();
            return;
        }

        Data data = _excel.Test[_id];
        string name = "Null";
        if (data.CharaID != "None")
        {
            List<string> charaList = new List<string>(data.CharaID.Split('#'));
            int[] charaID = new int[charaList.Count];
            Sprite[] sprite = new Sprite[charaList.Count];
            int[] fadeType = new int[charaList.Count];

            int count = 0;
            charaList.ForEach(c =>
            {
                if (c.Length > 0)
                {
                    string[] get = c.Split(',');
                    charaID[count] = int.Parse(get[0]) - 1;
                    sprite[count] = _chara.CharaData[charaID[count]].FaceID[int.Parse(get[1]) - 1];
                    fadeType[count] = int.Parse(get[2]);

                    count++;
                }
            });

            string[] posID = data.Postion.Split(',');

            switch (data.Talk)
            {
                case 0:
                    name = _chara.CharaData[charaID[0]].Name;
                    break;
                case 1:
                    name = _chara.CharaData[charaID[1]].Name;
                    break;
                case 2:
                    name = "‘Sˆõ";
                    break;
            }

            if (data.CharaFadeSync)
            {
                for (int i = 0; i < count; i++)
                    UIManager.SetSprite(sprite[i], fadeType[i], int.Parse(posID[i]));
            }
            else
            {
                UIManager.SpriteAsyncSetter(sprite, fadeType, posID, count);
            }
        }
        else
        {
            for (int i = 1; i <= 2; i++)
                UIManager.SetSprite(null, 3, i);
            name = "???";
        }

        string[] back = data.BackGroundID.Split(',');
        UIManager.SetName(name);
        UIManager.SetMSG(data.MSG);
        if (back[0] != "None")
        {
            UIManager.SetBackGround(_back.BGDatas[int.Parse(back[0])].Sprite, int.Parse(back[1]));
        }
        
        _id++;
    }
    
    public void Break()
    {
        if (_id == _excel.Test.Count - 1)
        {
            Debug.Log("EndExcel");
            Init();
            return;
        }
    }
}
