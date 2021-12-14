using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSetter : MonoBehaviour
{
    public void SetFadeIDToRenderer(int fadeType, SpriteRenderer getData)
    {
        switch (fadeType)
        {
            case 0:
                Fade.SetSngle(getData, 0, 1);
                break;
            case 1:
                Fade.SetSngle(getData, 1, 0);
                break;
            case 2:
                Debug.Log("ClossFade");
                break;
            case 3:
                Fade.SetSngle(getData, 1, 1);
                break;
        }
    }
}
