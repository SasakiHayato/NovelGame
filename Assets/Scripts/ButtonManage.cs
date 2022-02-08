using UnityEngine;

public class ButtonManage : MonoBehaviour
{
    public DataSettings DataSettings { get; set; }
    public string SetName { get; set; }
    public void Call()
    {
        Debug.Log(SetName);
        DataSettings.CallExcelData(SetName);
    }
}
