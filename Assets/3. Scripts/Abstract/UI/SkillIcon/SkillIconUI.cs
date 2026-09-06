using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillIconUI : MonoBehaviour
{
    protected TargetContext context;

    [SerializeField] RectTransform rect;
    [SerializeField] CanvasGroup can;

    [SerializeField] Image bg;
    [SerializeField] TMP_Text skillNameT;

    [SerializeField] Color32 normalCol;
    [SerializeField] Color32 selectCol;

    protected bool isclick;
    protected static Action OnClicked;

    protected float animationSpeed;

    public virtual void Initialize(TargetContext context, float duration)
    {
        this.context = context;
        animationSpeed = duration;

        OnClicked += RemoveEvent;
        SkillManager.manager.OnSkillSelected += SetSelect;

        ContextSet();
        can.alpha = 0;
    }

    protected virtual void OnDisable()
    {
        OnClicked -= RemoveEvent;
        SkillManager.manager.OnSkillSelected -= SetSelect;
    }

    protected TargetContext GiveData()
    {
        return context;
    }

    void ContextSet()
    {
        skillNameT.text = context.useSkill.skillID;

        SetSelect();
    }

    void RemoveEvent()
    {
        GetData.nowSelectedSkill -= GiveData;
    }

    public void FirstSetMove()
    {
        ResetPos();
        MoveAside();
        Movement.DOFade(can, 1f, animationSpeed);
    }

    public void SetMove()
    {
        SkillManagerData skillData = GetData.skillM_Data.Invoke();

        if (skillData.setSkillList[skillData.nowSelectedHero].nowSelectedContext == context)
        {
            rect.anchoredPosition = new Vector2(-50f, rect.anchoredPosition.y);
        }
        else
        {
            rect.anchoredPosition = Vector2.zero;
        }

        SetSelect();
    }

    void MoveAside()
    {
        Vector2 targetPos = Vector2.zero;

        Movement.DoAnchorMove(rect, targetPos, animationSpeed);
    }

    void ResetPos()
    {
        rect.anchoredPosition = new Vector2(300f, rect.anchoredPosition.y);
    }

    public void OnEnter()
    {
        Movement.DoAnchorMove(rect, new Vector2(-50f, rect.anchoredPosition.y), animationSpeed);
    }

    public void OnClick()
    {
        OnClicked?.Invoke();

        if (!isclick)
        {
            isclick = true;

            SkillSet();
        }
        else
        {
            SetDefault();
        }
    }

    protected abstract void SkillSet();

    void SetColor(Color32 color)
    {
        bg.color = color;
    } 

    protected void SetSelect()
    {
        SkillManagerData skillData = GetData.skillM_Data.Invoke();

        if (skillData.setSkillList[skillData.nowSelectedHero].nowSelectedContext == context)
        {
            isclick = true;

            OnEnter();
            SetColor(selectCol);
        }
        else
        {
            return;
        }
    }

    protected void SetDefault()
    {
        TargetContext targetCon = new TargetContext();
        targetCon.SetDefault(context.useSkill);

        SkillManagerData skillData = GetData.skillM_Data.Invoke();
        skillData.setSkillList[skillData.nowSelectedHero].nowSelectedContext = targetCon;

        SkillManager.manager.SetTargetContext(targetCon);

        isclick = false;

        rect.anchoredPosition = Vector2.zero;
        SetColor(normalCol);
    }

    public void OnExit()
    {
        if (!isclick)
        {
            MoveAside();
        }
    }
}
