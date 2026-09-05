using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon_UI : MonoBehaviour
{
    TargetContext context;

    [SerializeField] RectTransform rect;
    [SerializeField] CanvasGroup can;

    [SerializeField] Image bg;
    [SerializeField] TMP_Text skillNameT;

    [SerializeField] Color32 normalCol;
    [SerializeField] Color32 selectCol;

    bool isclick;
    static Action OnClicked;

    float animationSpeed;

    public void Initialize(TargetContext context, float duration)
    {
        this.context = context;
        animationSpeed = duration;

        OnClicked += RemoveEvent;
        SkillManager.manager.OnSkillSelected += SetBlock;
        SkillManager.manager.OnSkillSelected += SetSelect;

        ContextSet();
        can.alpha = 0;
    }

    private void OnDisable()
    {
        OnClicked -= RemoveEvent;
        SkillManager.manager.OnSkillSelected -= SetBlock;
        SkillManager.manager.OnSkillSelected -= SetSelect;
    }

    TargetContext GiveData()
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
        if(context.selected)
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

            GetData.nowSelectedSkill += GiveData;
            SkillManager.manager.OnSkillFind?.Invoke();
        }
        else
        {
            SetDefault();
        }
    }

    void SetColor(Color32 color)
    {
        bg.color = color;
    }

    void SetBlock()
    {
        SkillManagerData data = GetData.skillM_Data.Invoke();

        if(data.lasetTargetContext.useSkill.skillType == SkillData.SkillType.Act && context.useSkill.skillType == SkillData.SkillType.Act)
        {
            SetDefault();
        }
    }

    void SetSelect()
    {
        if (context.selected)
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

    void SetDefault()
    {
        TargetContext targetCon = new TargetContext();
        targetCon.SetDefault(context.useSkill);

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
