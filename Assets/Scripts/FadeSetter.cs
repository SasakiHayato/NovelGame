using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSetter : MonoBehaviour
{
    Dictionary<int, SpriteRenderer> _spriteRendererDic = new Dictionary<int, SpriteRenderer>();

    public void AddSpriteRederer(SpriteRenderer renderer, int posID)
    {
        _spriteRendererDic.Add(posID, renderer);
    }

    public void SaveSpriteRederer(SpriteRenderer renderer, int posID)
    {
        foreach (var dic in _spriteRendererDic)
        {
            if (dic.Key == posID)
            {
                SpriteRenderer sprite = dic.Value.gameObject.GetComponent<SpriteRenderer>();
                sprite.sprite = renderer.sprite;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
                _spriteRendererDic.Remove(dic.Key);
                _spriteRendererDic.Add(posID, sprite);
                return;
            }
        }
    }

    public void SetFadeIDToRenderer(int fadeType, SpriteRenderer getData, int posID = 0)
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
                SetSpriteActive(posID);
                Fade.SetSngle(getData, 0, 1);
                break;
            case 3:
                Fade.SetSngle(getData, 1, 1);
                break;
        }
    }

    void SetSpriteActive(int posID)
    {
        foreach (var dic in _spriteRendererDic)
        {
            if (dic.Key == posID)
            {
                SpriteRenderer sprite = dic.Value.gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
                _spriteRendererDic.Remove(dic.Key);
                _spriteRendererDic.Add(posID, sprite);
                return;
            }
        }
    }
}
