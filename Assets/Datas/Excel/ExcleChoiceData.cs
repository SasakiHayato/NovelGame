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
}

[ExcelAsset]
public class ExcleChoiceData : ScriptableObject
{
    List<ChoicesData> TestChoices = new List<ChoicesData>();

    public List<ChoicesData> GetChoicesData(ChoiceDataName dataName)
    {
        switch (dataName)
        {
            case ChoiceDataName.TestChoices:
                return TestChoices;
        }

        return null;
    }
}
