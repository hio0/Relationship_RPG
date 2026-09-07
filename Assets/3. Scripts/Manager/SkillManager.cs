using System;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ActSkillContext
{
    public SkillData useSkill;
    public List<Character> targets;

    public void SetDefault(SkillData skill)
    {
        targets = null;
        useSkill = skill;
    }
}

public class SetSkillContext
{
    public Character performer { get; internal set; }
    public CharacterFight perfomerFight { get; internal set; }
    public List<SkillData> drawSkillList { get; internal set; } = new();

    public ActSkillContext nowSelectedActSkill { get; set; }

    public bool almostSet;

    public void SetDefault()
    {
        nowSelectedActSkill = null;

        almostSet = false;
    }
}

public class SkillManagerData
{
    public List<SetSkillContext> setSkillList = new();
    public int nowSelectedHero { get; internal set; }

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
    public Action OnSkillSelected;

    private void OnEnable()
    {
        GetData.skillM_Data += GiveData;
        CombatManager.manager.OnActingCharSelected += SetUsingSkill;
    }

    private void OnDisable()
    {
        GetData.skillM_Data -= GiveData;
        CombatManager.manager.OnActingCharSelected -= SetUsingSkill;
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
        List<ActSkillContext> contextList = new();

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
            ActSkillContext context = new ActSkillContext();
            context.SetDefault(skillDatas[i]);

            contextList.Add(context);
        }

        SetSkillContext setSkill = new SetSkillContext
        {
            performer = character,
            perfomerFight = character.gameObject.GetComponent<CharacterFight>(),
            drawSkillList = skillDatas,
        };
        setSkill.SetDefault();

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
}
