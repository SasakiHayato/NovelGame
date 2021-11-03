using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataBase.Charactors;
using DataBase.BackGrounds;
using SimpleFade;

public class UIManager : MonoBehaviour
{
    [SerializeField] CharactorData m_charaData;
    [SerializeField] BackGroundData m_backData;
    [SerializeField] Text m_nameText;

    [SerializeField] Image[] m_charaPoses = new Image[0];
    [SerializeField] Image[] m_backImage = new Image[2];

    [SerializeField] float m_fadeSpeed;
    int m_charaCount = 0;

    List<Image> m_charaList = new List<Image>();
    List<Image> m_imageList = new List<Image>();

    void Start()
    {
        //Charaのアルファ値
        foreach (Image set in m_charaPoses)
            set.color = new Color(set.color.r, set.color.g, set.color.b, 0);
    }

    /// <summary>
    /// 引数１, charaId
    /// 引数2, charaPos
    /// </summary>
    /// // Note ; charaSync キャラを同時にだすかどうか。Trueなら同時
    public void GetData(Dictionary<int, int> keys, bool charaSync, int backId)
    {
        SetBackImage(backId);

        foreach (var set in keys)
        {
            Sprite getChara = m_charaData.GetCharaData(set.Key).GetSprite();
            m_charaPoses[set.Value].sprite = getChara;
            m_charaList.Add(m_charaPoses[set.Value]);
            m_charaCount++;
        }

        foreach (Image set in m_charaPoses)
        {
            if (set.sprite == null) continue;
            m_imageList.Add(set);
        }

        if (charaSync) Fade.InMultipe(m_imageList, m_fadeSpeed);
        else Fade.InMultipeAsync(m_charaList, m_fadeSpeed);
    }

    void SetBackImage(int id)
    {
        Sprite get = m_backData.GetBackData(id).GetBackSprite;
        m_backImage[0].sprite = get;
        Color color = m_backImage[0].color;
        m_backImage[0].color = new Color(color.r, color.g, color.b, 1);
    }
}