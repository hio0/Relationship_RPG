using System;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class TargetContext
{
    public List<Character> targets;
    public SkillData useSkill;

    public void SetDefault(SkillData skill)
    {
        targets = null;
        useSkill = skill;
    }
}

public class SetSkillContext
{
    public Character performer;
    public List<TargetContext> targetContexts;
    public TargetContext nowSelectedContext;
}

public class SkillManagerData
{
    public List<SetSkillContext> setSkillList = new();
    public int nowSelectedHero { get; internal set; }

    public TargetContext lasetTargetContext { get; internal set; }

    public SetSkillContext FindCharactersContext(Character character)
    {
        for (int i = 0; i < setSkillList.Count; i++)
        {
            if (setSkillList[i].performer.id == character.id)
            {
                return setSkillList[i];
            }
        }

        return null;
    }
}

public class SkillManager : Manager_DataGiving<SkillManager, SkillManagerData>
{
    public event Action OnActerSelected;
    public Action OnSkillFind;
    public event Action OnSkillSelected;

    private void OnEnable()
    {
        GetData.skillM_Data += GiveData;
        RangeManager.manager.OnActingCharSelected += SetUsingSkill;
    }

    private void OnDisable()
    {
        GetData.skillM_Data -= GiveData;
        RangeManager.manager.OnActingCharSelected -= SetUsingSkill;
    }

    void SetKey()
    {
        InputManager.manager.OnPressA += BackHero;
        InputManager.manager.OnPressD += NextHero;
    }

    void RemoveKey()
    {
        InputManager.manager.OnPressA -= BackHero;
        InputManager.manager.OnPressD -= NextHero;
    }

    void SetUsingSkill()
    {
        managerData.setSkillList.Clear();

        foreach (Character character in GetData.rangeM_Data.Invoke().ourCharacterList)
        {
            DrawSkill(character);
        }

        managerData.nowSelectedHero = 0;
        SetKey();
        ActSelect();
    }

    void DrawSkill(Character character)
    {
        List<int> usedIndex = new();
        List<SkillData> skillDatas = new();
        List<TargetContext> contextList = new();

        int drawCount = 3;
        if(character.skillList.Count < 3)
        {
            drawCount = character.skillList.Count;
        }

        for (int i = 0; i < drawCount; i++)
        {
            int r = UnityEngine.Random.Range(0, character.skillList.Count);

            if (usedIndex.Contains(r))
            {
                i--;
                continue;
            }

            usedIndex.Add(r);
            skillDatas.Add(character.skillList[r]);
        }

        for (int i = 0; i < skillDatas.Count; i++)
        {
            TargetContext context = new TargetContext();
            context.SetDefault(skillDatas[i]);

            contextList.Add(context);
        }

        SetSkillContext setSkill = new SetSkillContext
        {
            performer = character,
            targetContexts = contextList
        };
        managerData.setSkillList.Add(setSkill);
    }

    void ActSelect()
    {
        OnActerSelected?.Invoke();
    }

    void NextHero()
    {
        managerData.nowSelectedHero++;
        if(managerData.nowSelectedHero >= managerData.setSkillList.Count)
        {
            managerData.nowSelectedHero = 0;
        }

        ActSelect();
    }

    void BackHero()
    {
        managerData.nowSelectedHero--;
        if (managerData.nowSelectedHero <= 0)
        {
            managerData.nowSelectedHero = managerData.setSkillList.Count - 1;
        }

        ActSelect();
    }

    public void SetTargetContext(TargetContext context)
    {
        List<TargetContext> contexts = managerData.setSkillList[managerData.nowSelectedHero].targetContexts;

        for (int i = 0; i < contexts.Count; i++)
        {
            if (contexts[i].useSkill == context.useSkill)
            {
                contexts[i].targets = context.targets;
                managerData.lasetTargetContext = contexts[i];
                break;
            }
        }

        OnSkillSelected?.Invoke();
        managerData.lasetTargetContext = null;

        NextHero();
    }
}
