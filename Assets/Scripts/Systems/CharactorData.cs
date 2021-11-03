using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataBase
{
    namespace Charactors
    {
        [CreateAssetMenu(fileName = "Charactors")]
        public class CharactorData : ScriptableObject
        {
            [SerializeField] List<Data> m_datas = new List<Data>();
            public Data GetCharaData(int id) => m_datas[id];
        }

        [System.Serializable]
        public class Data
        {
            [SerializeField] string m_name;
            [SerializeField] Sprite m_defSprite;

            public string GetName() => m_name;
            public Sprite GetSprite() => m_defSprite;
        }
    }
}




