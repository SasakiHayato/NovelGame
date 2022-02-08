using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoicesData
{
    public int ChoicesCount;
    public string MSG;
    public string NextTalkData;
}

public enum ChoiceDataName
{
    TestChoices,
    TestChoices2,

    None,
}

[ExcelAsset]
public class ExcleChoiceData : ScriptableObject
{
    public List<ChoicesData> TestChoices = new List<ChoicesData>();
    public List<ChoicesData> TestChoices2 = new List<ChoicesData>();

    public List<ChoicesData> GetChoicesData(ChoiceDataName dataName)
    {
        switch (dataName)
        {
            case ChoiceDataName.TestChoices:
                return TestChoices;
            case ChoiceDataName.TestChoices2:
                return TestChoices2;

            default: return null;
        }
    }
}
