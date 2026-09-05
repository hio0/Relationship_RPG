using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting_UI : CharacterChildUI
{
    [SerializeField] CanvasGroup can;

    protected override void OnEnable()
    {
        base.OnEnable();

        ResetAlpha();
        SkillManager.manager.OnSkillFind += EventSet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void EventSet()
    {
        myChar.trigger.OnEnter += Targeted;
        myChar.trigger.OnExit += ResetAlpha;
    }

    protected override void RemoveEvent()
    {
        myChar.trigger.OnEnter -= Targeted;
        myChar.trigger.OnExit -= ResetAlpha;
    }

    void ResetAlpha()
    {
        can.alpha = 0;
    }

    void Targeted()
    {
        can.alpha = 1;
    }
}
