using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSettings
{
    /// <summary>
    /// ˆø”‚P, charaId
    /// ˆø”2, charaPos
    /// </summary>
    Dictionary<int, int> m_charaKey = new Dictionary<int, int>();
    public Dictionary<int, int> Keys { get => m_charaKey; }

    // ƒLƒƒƒ‰‚ğ“¯‚É‚¾‚·‚©‚Ç‚¤‚©
    public bool CharaSync { get; private set; } = false;
    // MSG‚ğƒLƒƒƒ‰‚Æˆê‚Éo‚·‚©‚Ç‚¤‚©
    public bool MSGSync { get; private set; } = false;
    
    public void Set(Data data)
    {
        GetCharaData(data);
        GetSelectData(data);
    }

    void GetCharaData(Data data)
    {
        string[] chara = data.m_charaId.Split(',');
        int[] charaId = new int[chara.Length];

        for (int i = 0; i < chara.Length; i++) charaId[i] = int.Parse(chara[i]);

        string[] poses = data.m_pos.Split(',');
        int[] charaPos = new int[poses.Length];

        for (int i = 0; i < poses.Length; i++) charaPos[i] = int.Parse(poses[i]);
        for (int i = 0; i < charaId.Length; i++) m_charaKey.Add(charaId[i], charaPos[i]);
    }

    void GetSelectData(Data data)
    {
        string[] select = data.m_selector.Split(',');
        int[] check = new int[select.Length];
        for (int i = 0; i < select.Length; i++)
            check[i] = int.Parse(select[i]);

        SetData(check);
    }

    void SetData(int[] check)
    {
        if (check[0] == 0)
            CharaSync = true;
        else
            CharaSync = false;

        if (check[1] == 0)
            MSGSync = true;
        else
            MSGSync = false;
    }
}
