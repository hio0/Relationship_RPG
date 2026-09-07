using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIcon_ReAct : SkillIconUI
{
    protected override void SetDefault()
    {
        base.SetDefault();

        SkillManagerData data = GetData.skillM_Data.Invoke();
        data.setSkillList[data.nowSelectedHero].perfomerFight.reActList.Remove((SkillData_ReAct)this.data.mySkill);
    }

    protected override void SkillSet()
    {
        this.data.OnSelect.Invoke();

        SkillManagerData data = GetData.skillM_Data.Invoke();
        data.setSkillList[data.nowSelectedHero].perfomerFight.reActList.Add((SkillData_ReAct)this.data.mySkill);

        SkillManager.manager.OnSkillFind?.Invoke();
    }
}
