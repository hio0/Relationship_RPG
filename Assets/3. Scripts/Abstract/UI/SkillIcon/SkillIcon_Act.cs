using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class SkillIcon_Act : SkillIconUI
{
    public override void Initialize(TargetContext context, float duration)
    {
        base.Initialize(context, duration);
        SkillManager.manager.OnSkillSelected += SetSelect;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SkillManager.manager.OnSkillSelected -= SetBlock;
    }

    void SetBlock()
    {
        SkillManagerData data = GetData.skillM_Data.Invoke();

        if (data.lasetTargetContext.useSkill.skillType == SkillData.SkillType.Act && context.useSkill.skillType == SkillData.SkillType.Act)
        {
            SetDefault();
        }
    }

    protected override void SkillSet()
    {
        GetData.nowSelectedSkill += GiveData;
        SkillManager.manager.OnSkillFind?.Invoke();
    }
}
