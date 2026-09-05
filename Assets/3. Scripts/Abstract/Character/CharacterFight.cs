using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투용 스프라이트에만 부착.
/// </summary>
public class CharacterFight : CharacterComponent
{
    private void OnEnable()
    {
        BeforeSetting();
        RangeManager.manager.OnActerFind += SetActing;
        SkillManager.manager.OnSkillFind += EventSet_Targeted;
    }

    private void OnDisable()
    {
        RangeManager.manager.OnActerFind -= SetActing;
        SkillManager.manager.OnSkillFind -= EventSet_Targeted;
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
        TargetContext data = GetData.nowSelectedSkill.Invoke();

        List<Character> list = new();
        list.Add(myChar);

        TargetContext context = new TargetContext
        {
            selected = true,
            useSkill = data.useSkill,
            targets = list
        };
        SkillManager.manager.SetTargetContext(context);
    }
}
