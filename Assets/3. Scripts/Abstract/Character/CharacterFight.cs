using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투용 스프라이트에만 부착.
/// </summary>
public class CharacterFight : CharacterComponent
{
    public List<SkillData_ReAct> reActList = new();
    public List<SkillData_ReAct> selectedReActList = new();

    private void OnEnable()
    {
        BeforeSetting();
        CombatManager.manager.OnActerFind += SetActing;
        SkillManager.manager.OnSkillFind += EventSet_Targeted;

        CombatManager.manager.OnActStart += ReActSet;
        CombatManager.manager.OnActFinish += ResetAct;
    }

    private void OnDisable()
    {
        CombatManager.manager.OnActerFind -= SetActing;
        SkillManager.manager.OnSkillFind -= EventSet_Targeted;

        CombatManager.manager.OnActStart -= ReActSet;
        CombatManager.manager.OnActFinish -= ResetAct;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    KeyValuePair<int, Character> SetActing()
    {
        myChar.SetSpeed();
        return new KeyValuePair<int, Character>(myChar.speed, myChar);
    }

    void BeforeSetting()
    {
        myChar.actCount = 1;
    }

    void EventSet_Targeted()
    {
        myChar.trigger.OnClick += Targeted;
    }

    void Targeted()
    {
        SkillManagerData data = GetData.skillM_Data.Invoke();
        SkillIconData targetedSkill = GetData.nowSelectedSkill.Invoke();

        List<Character> list = new();
        list.Add(myChar);

        ActSkillContext context = new ActSkillContext
        {
            targets = list,
            useSkill = targetedSkill.mySkill
        };

        data.setSkillList[data.nowSelectedHero].nowSelectedActSkill = context;
        targetedSkill.OnSelect.Invoke();
    }

    void ReActSet()
    {
        for (int i = 0; i < reActList.Count; i++)
        {
            Skill_ReAct skill = new();

            SkillContext context = new SkillContext
            {
                performer = myChar,
                skillData = reActList[i]
            };

            skill.Initialize(context);
        }
    }

    void ResetAct()
    {
        reActList.Clear();
        selectedReActList.Clear();
    }
}
