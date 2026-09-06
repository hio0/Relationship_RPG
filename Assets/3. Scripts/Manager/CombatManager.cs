using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerData
{
    public List<KeyValuePair<Character, TargetContext>> nowSkillActs = new();
    public List<Character> alsoSettingActers;
    public int nowActNum;

    public void ResetData()
    {
        nowActNum = 0;
        alsoSettingActers.Clear();
        nowSkillActs.Clear();
    }
}

public class CombatManager : Manager_DataGiving<CombatManager, CombatManagerData>
{
    [SerializeField] GameObject ActP;

    public event Action OnSetAct;
    public event Action OnSetNextAct;
    public Action<Character, TargetContext> OnSetNewAct;

    // Start is called before the first frame update
    void Start()
    {
        GetData.combatM_Data += GiveData;
        RangeManager.manager.OnSelectedChar += SetActP;
        OnSetNewAct += SetAct;
    }

    private void OnDisable()
    {
        GetData.combatM_Data -= GiveData;
        RangeManager.manager.OnSelectedChar -= SetActP;
        OnSetNewAct -= SetAct;
    }

    void SetActP(Character character)
    {
        managerData.ResetData();
        ActP.SetActive(true);

        SetFirstSkill(character);
        OnSetAct?.Invoke();
        SetInput();
    }

    void SetFirstSkill(Character character)
    {
        SkillManagerData skillData = GetData.skillM_Data.Invoke();

        SetAct(character, skillData.FindCharactersContext(character).nowSelectedContext);
        SetAlsoActer();
    }

    void SetAct(Character character, TargetContext context)
    {
        managerData.nowSkillActs.Add(new KeyValuePair<Character, TargetContext>(character, context));
    }

    void SetAlsoActer()
    {
        managerData.alsoSettingActers.Add(managerData.nowSkillActs[managerData.nowActNum].Key);
    }

    void SetInput()
    {
        InputManager.manager.OnPressSpace += NextAct;
    }

    void RemoveInput()
    {
        InputManager.manager.OnPressSpace -= NextAct;
    }

    void NextAct()
    {
        OnSetNextAct?.Invoke();

        managerData.nowActNum++;
        if(managerData.nowActNum >= managerData.nowSkillActs.Count)
        {
            RemoveInput();
        }
    }
}
