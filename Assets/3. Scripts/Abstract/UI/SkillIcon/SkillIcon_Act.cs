using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class SkillIcon_Act : SkillIconUI
{
    public override void Initialize(SkillData skill, float duration)
    {
        base.Initialize(skill, duration);
        data.OnSelect += SetBlock;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        data.OnSelect -= SetBlock;
    }

    void SetBlock()
    {
        SkillManagerData data = GetData.skillM_Data.Invoke();

        if (data.setSkillList[data.nowSelectedHero].nowSelectedActSkill.useSkill != this.data.mySkill)
        {
            SetDefault();
        }
    }

    protected override void SetDefault()
    {
        base.SetDefault();
        SkillManagerData data = GetData.skillM_Data.Invoke();
        data.setSkillList[data.nowSelectedHero].nowSelectedActSkill = null;
    }

    protected override void SkillSet()
    {
        SkillManager.manager.OnSkillFind?.Invoke();
    }
}
