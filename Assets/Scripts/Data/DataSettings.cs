using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataSettings : MonoBehaviour
{
    [SerializeField] ExcelData _excel;
    [SerializeField] CharaDataBase _chara;
    [SerializeField] BackGroundDataBase _back;
    [SerializeField] ExcleChoiceData _choiceData;

    DataSaver _dataSaver = new DataSaver();
    ChoicesSetter _choicesSetter = null;

    int _id = 0;
    public bool IsMove { get; set; } = false;
    public bool ChoiceFlag { get; private set; } = false;

    public TalkName TalkName { get; set; } = TalkName.Test;

    public void SetUp()
    {
        _choicesSetter = FindObjectOfType<ChoicesSetter>();
        ChoiceFlag = false;
        _choicesSetter.SetUp(this);
        _choicesSetter.gameObject.SetActive(false);
    }

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
        
        if (_id == _excel.GetTalkData(TalkName).Count - 1)
        {
            Init();
            return;
        }

        TalkData data = _excel.GetTalkData(TalkName)[_id];

        if (data.GetChoicesData != "")
        {
            string[] choicesData = data.GetChoicesData.Split(',');
            ChoiceDataName choice = (ChoiceDataName)Enum
                .Parse(typeof(ChoiceDataName), choicesData[0], true);
            _choicesSetter.gameObject.SetActive(true);
            _choicesSetter.Set(_choiceData.GetChoicesData(choice));
            ChoiceFlag = true;
            return;
        }

        string name = "Null";
        if (data.CharaID != "None")
        {
            List<string> charaList = new List<string>(data.CharaID.Split('#'));
            if (_dataSaver.CharaID == null || data.CharaID != "")
            {
                _dataSaver.CharaID = data.CharaID;
            }
            else
            {
                if (charaList.Count <= 1) 
                    charaList = new List<string>(_dataSaver.CharaID.Split('#'));
            }

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
            
            if (_dataSaver.PositionID == null || data.Postion != "")
                _dataSaver.PositionID = data.Postion;
            else
            {
                if (posID.Length <= 1) posID = _dataSaver.PositionID.Split(',');
            }

            string talkID = data.Talk;
            if (_dataSaver.Talk == null || data.Talk != "")
            {
                _dataSaver.Talk = data.Talk;
            }
            else
            {
                if (data.Talk.Length <= 1) talkID = _dataSaver.Talk;
            }

            switch (int.Parse(talkID))
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

        if (_dataSaver.BackGroundID == null || data.BackGroundID != "")
        {
            _dataSaver.BackGroundID = data.BackGroundID;
        }
        else
        {
                back = _dataSaver.BackGroundID.Split(',');
        }

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

    public void CallExcelData(string name)
    {
        TalkName talkName = (TalkName)Enum.Parse(typeof(TalkName), name, true);
        Init();
        ChoiceFlag = false;
        TalkName = talkName;
        _excel.GetTalkData(talkName);
        SetDatas();
        foreach (Transform child in _choicesSetter.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        _choicesSetter.gameObject.SetActive(false);
    }
}
