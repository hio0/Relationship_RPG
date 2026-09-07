using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ReAct : Skill
{
    public override void Initialize(SkillContext context)
    {
        base.Initialize(context);

        SetTrigger();
        CombatManager.manager.OnActFinish += RemoveEvent;
    }

    void SetTrigger()
    {
        SkillData_ReAct skill = (SkillData_ReAct)data.skillData;

        switch (skill.skillTerms)
        {
            case SkillData_ReAct.SkillTerms.damaged:
                data.performer.OnDamaged += SetReAct;
                break;
        }
    }

    void SetReAct()
    {
        SkillData_ReAct skill = (SkillData_ReAct)data.skillData;
        int chance = Random.Range(1, 101);

        if (skill.actChance >= chance)
        {
            data.performer.gameObject.GetComponent<CharacterFight>().selectedReActList.Add(skill);
        }
    }

    KeyValuePair<Character, SkillData_ReAct> GiveReAct()
    {
        return new KeyValuePair<Character, SkillData_ReAct>(data.performer, (SkillData_ReAct)data.skillData);
    }

    void RemoveEvent()
    {
        CombatManager.manager.OnReActFind -= GiveReAct;
        CombatManager.manager.OnActFinish -= RemoveEvent;
    }
}
