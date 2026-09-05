using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OurCharacterInfo_UI : CharacterUI
{
    [SerializeField] RectTransform rect;

    [SerializeField] Image icon;
    [SerializeField] TMP_Text nameT;
    [SerializeField] Image hp;
    [SerializeField] Image mana;

    // Start is called before the first frame update
    void Start()
    {
        SkillManager.manager.OnActerSelected += Selected;
    }

    private void OnDisable()
    {
        SkillManager.manager.OnActerSelected -= Selected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void MoveAside(float duration)
    {
        float targetX = rect.anchoredPosition.x - 750f;

        rect.DOAnchorPosX(targetX, duration).SetEase(Ease.OutExpo);
    }

    void MoveDefault()
    {
        rect.anchoredPosition = Vector2.zero;
    }

    public void Selected()
    {
        SkillManagerData data = GetData.skillM_Data.Invoke();
        float targetY = 0f;

        Debug.Log(data.nowSelectedHero);
        if (data.setSkillList[data.nowSelectedHero].performer.id == myChar.id)
        {
            targetY = 25;
        }
        else
        {
            targetY = 0;
        }

        rect.DOAnchorPosY(targetY, 0.3f);
    }
}
