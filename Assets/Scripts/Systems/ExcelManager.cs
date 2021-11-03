using System.Collections.Generic;
// AssemblyUtils
using System;
using System.Reflection;
using System.Linq;

public class ExcelManager
{
    Dictionary<EventType, IData> m_iventDic = new Dictionary<EventType, IData>();
    AssemblyUtils m_utils = new AssemblyUtils();

    public void AddDic(IData iData)
    {
        EventType type = FindId(iData);
        m_iventDic.Add(type, iData);
    }

    EventType FindId(IData iData)
    {
        string iventName = iData.ToString();
        EventType type = EventType.None;
        bool loop = true;
        int count = 0;

        while (loop)
        {
            type = (EventType)count;
            string typeName = Enum.GetName(typeof(EventType), type);

            if (typeName == "None") return EventType.None;
            if (iventName == typeName) loop = false;

            count++;
        }

        return type;
    }

    public IData GetEvent(EventType type) => m_iventDic[type];

    // 下記は、データの初期設定
    public void SetIventData<T>(T setT, TextData setData) where T : IData 
        => setT.SetTextData(setData);
    public T[] Requests<T>() where T : class => m_utils.Request<T>();

    // https://baba-s.hatenablog.com/entry/2014/06/10/200710 参照
    class AssemblyUtils
    {
        public T[] Request<T>() where T : class => CreateInterfaceInstances<T>();
        /// <summary>
        /// 現在実行中のコードを格納しているアセンブリ内の指定された
        /// インターフェイスが実装されているすべての Type を返します
        /// </summary>
        public static Type[] GetInterfaces<T>()
        {
            return Assembly.GetExecutingAssembly().
                GetTypes().Where(c => c.GetInterfaces().Any(t => t == typeof(T))).ToArray();
        }

        /// <summary>
        /// 現在実行中のコードを格納しているアセンブリ内の指定された
        /// インターフェイスが実装されているすべての Type のインスタンスを作成して返します
        /// </summary>
        public static T[] CreateInterfaceInstances<T>() where T : class
            => GetInterfaces<T>().Select(c => Activator.CreateInstance(c) as T).ToArray();
    }
}
