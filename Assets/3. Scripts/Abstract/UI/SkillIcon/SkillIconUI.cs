using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconData
{
    public SkillData mySkill;
    public Action OnSelect;
}

public abstract class SkillIconUI : GiveDataClass<SkillIconData>
{
    [SerializeField] RectTransform rect;
    [SerializeField] CanvasGroup can;

    [SerializeField] Image bg;
    [SerializeField] TMP_Text skillNameT;

    [SerializeField] Color32 normalCol;
    [SerializeField] Color32 selectCol;

    [SerializeField] float defaultPos;
    [SerializeField] float startPos;
    [SerializeField] float selectedPos;

    protected bool isclick;
    protected static Action OnClicked;

    protected float animationSpeed;

    public virtual void Initialize(SkillData skill, float duration)
    {
        data.mySkill = skill;
        animationSpeed = duration;

        data.OnSelect += SetSelect;
        OnClicked += RemoveEvent;

        IconSet();
        can.alpha = 0;
    }

    protected virtual void OnDisable()
    {
        data.OnSelect -= SetSelect;
        OnClicked -= RemoveEvent;
    }

    void IconSet()
    {
        skillNameT.text = data.mySkill.skillID;

        SetSelect();
    }

    void RemoveEvent()
    {
        GetData.nowSelectedSkill -= GiveData;
    }

    public void FirstSetMove()
    {
        rect.anchoredPosition = new Vector2(300f, rect.anchoredPosition.y);
        MoveTo(defaultPos);
        Movement.DOFade(can, 1f, animationSpeed);
    }

    public void SetMove()
    {
        SkillManagerData skillData = GetData.skillM_Data.Invoke();

        if (skillData.setSkillList[skillData.nowSelectedHero].nowSelectedActSkill.useSkill == data.mySkill)
        {
            rect.anchoredPosition = new Vector2(-50f, rect.anchoredPosition.y);
        }
        else
        {
            rect.anchoredPosition = Vector2.zero;
        }
    }

    void MoveTo(float posX)
    {
        Movement.DoAnchorMove(rect, new Vector2(posX, rect.anchoredPosition.y), animationSpeed);
    }

    void SetColor(Color32 color)
    {
        bg.color = color;
    }

    public void OnEnter()
    {
        MoveTo(selectedPos);
    }

    public void OnClick()
    {
        OnClicked?.Invoke();

        if (!isclick)
        {
            isclick = true;

            GetData.nowSelectedSkill += GiveData;
            SkillSet();
        }
        else
        {
            SetDefault();
        }
    }

    protected abstract void SkillSet();

    void SetSelect()
    {
        isclick = true;

        OnEnter();
        SetColor(selectCol);
    }

    protected virtual void SetDefault()
    {
        SkillManagerData skillData = GetData.skillM_Data.Invoke();

        isclick = false;

        MoveTo(defaultPos);
        SetColor(normalCol);
    }

    public void OnExit()
    {
        if (!isclick)
        {
            MoveTo(defaultPos);
        }
    }
}
