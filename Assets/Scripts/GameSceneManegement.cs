using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManegement : MonoBehaviour
{
    [SerializeField] TextData m_data;
    [SerializeField] Text m_msgText;
    [SerializeField] float m_waitTime;
    
    ExcelManager m_excel = new ExcelManager();
    DataSettings m_settingData = new DataSettings();
    UIManager m_ui;

    string m_msgData;
    int m_idCount = 0;

    bool m_setting = false;
    bool m_break = false;
    
    void Start()
    {
        m_ui = FindObjectOfType<UIManager>();

        foreach (IData get in m_excel.Requests<IData>())
        {
            get.SetTextData(m_data);
            m_excel.AddDic(get);
        }
        
        m_msgText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !m_setting)
        {
            IData set = m_excel.GetEvent(EventType.Test);

            SetText(set);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instnace().IsBreak = true;
            m_break = GameManager.Instnace().IsBreak;
            SimpleFade.Fade.FadeBreak();
        }
    }

    void SetText(IData set)
    {
        m_setting = true;

        m_msgText.text = "";
        Data data = set.GetData(m_idCount);
        m_settingData.Set(data);
        m_msgData = data.m_msg;

        if (m_msgData == "None") return;
        else
        {
            // å„Ç≈ïœçX
            m_ui.GetData(m_settingData.Keys, m_settingData.CharaSync, data.m_backGroundId);
            StartCoroutine(Set());

            if (data.m_isEvent)
            {
                Debug.Log("Eventî≠ê∂");
            }
        }
    }

    IEnumerator Set()
    {
        for (int count = 0; count < m_msgData.Length; count++)
        {
            m_msgText.text += m_msgData[count];
            bool load = true;
            float time = 0;
            while (load)
            {
                time += Time.deltaTime;
                if (time > m_waitTime) load = false;

                if (m_break)
                {
                    m_break = false;
                    m_msgText.text = "";
                    m_msgText.text = m_msgData;
                    m_setting = false;
                    m_idCount++;
                    yield break;
                }
                yield return null;
            }
        }

        m_setting = false;
        m_idCount++;
    }
}
