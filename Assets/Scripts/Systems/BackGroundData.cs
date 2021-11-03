using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    namespace BackGrounds
    {
        [CreateAssetMenu(fileName = "BackGrounds")]
        public class BackGroundData : ScriptableObject
        {
            [SerializeField] List<Data> m_datas = new List<Data>();
            public Data GetBackData(int id) => m_datas[id];
        }

        [System.Serializable]
        public class Data
        {
            [SerializeField] string m_name;
            [SerializeField] Sprite m_back;


            public string Name { get => m_name; }
            public Sprite GetBackSprite { get => m_back; }
        }
    }
}
