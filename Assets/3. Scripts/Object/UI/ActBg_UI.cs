using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActBg_UI : MonoBehaviour
{
    [SerializeField] Image bg;
    [SerializeField] Image bgLine;

    bool isHero;
    float targetPos;
    float rotation;
    [SerializeField] RectTransform rect;
    
    public void Initialize(Character character, Color32 col, bool isOur)
    {
        bg.color = col;
        bgLine.color = col;

        isHero = isOur;

        SetMove();
    }

    void OnEnable()
    {
            
    }

    void SetMove()
    {
        if(isHero)
        {
            targetPos = 800;
            rotation = 65f;
        }
        else
        {
            targetPos = -800;
            rotation = -115f;
        }
    }

    void Move()
    {
        Movement.DoAnchorMove(rect, new Vector2(targetPos, rect.anchoredPosition.y), MainData.characterSkill_moveSpeed);
    }
}
