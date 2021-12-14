using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSetter : MonoBehaviour
{
    public void SetFadeIDToRenderer(int fadeType, SpriteRenderer getData)
    {
        switch (fadeType)
        {
            case 0:
                NewFade.SetSngle(getData, 0, 1);
                break;
            case 1:
                NewFade.SetSngle(getData, 0, 1);
                break;
            case 2:
                //Fade.SpriteCrossFade(getData, 1);
                break;
        }
    }

}
