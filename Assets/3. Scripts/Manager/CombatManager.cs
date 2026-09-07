using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerData
{
    public List<Character> actingCharacterList = new();
    public int nowSelectedChar;

    public List<KeyValuePair<Character, ActSkillContext>> actList { get; set; } = new();

    public List<Character> alsoSettingActers = new();
    public List<Character> alsoSettingTargets = new();
    public SkillData_ReAct nowSelectedReAct;
    public int nowActNum;

    public void ResetTurnData()
    {
        nowSelectedChar = 0;

        actingCharacterList.Clear();
    }

    public void ResetActData()
    {
        nowActNum = 0;
        nowSelectedReAct = null;

        actList.Clear();
        alsoSettingActers.Clear();
        alsoSettingTargets.Clear();
    }
}

public class CombatManager : Manager_DataGiving<CombatManager, CombatManagerData>
{
    [SerializeField] GameObject ActP;

    public Action OnActStart;
    public event Action OnActFinish;

    public event Func<KeyValuePair<int, Character>> OnActerFind;
    public event Action OnActingCharSelected;
    public event Action<Character> OnSelectedChar;

    public event Action OnSetAct;
    public event Func<KeyValuePair<Character, SkillData_ReAct>> OnReActFind;
    public event Action OnSetNextAct;

    // Start is called before the first frame update
    void Start()
    {
        GetData.combatM_Data += GiveData;
        FightManager.manager.OnTurnStart += SetActer;

        OnActStart += ActStart;

        InputManager.manager.OnPressTab += OnActStart;
    }

    private void OnDisable()
    {
        GetData.combatM_Data -= GiveData;
    }

    void SetActer()
    {
        managerData.ResetTurnData();

        List<KeyValuePair<int, Character>> acterSpeedList = new();
        if (OnActerFind != null)
        {
            foreach (Func<KeyValuePair<int, Character>> func in OnActerFind.GetInvocationList())
            {
                acterSpeedList.Add(func.Invoke());
            }
        }
        acterSpeedList.Sort((a, b) => b.Key.CompareTo(a.Key));

        foreach (var pair in acterSpeedList)
        {
            managerData.actingCharacterList.Add(pair.Value);
        }

        OnActingCharSelected?.Invoke();
    }

    void ActStart()
    {
        IEnumerator Cor()
        {
            managerData.ResetActData();
            SetNowChar();

            yield return new WaitForSeconds(1f);

            SetActP();
        }

        StartCoroutine(Cor());
    }

    void SetNowChar()
    {
        Character nowSelectedChar = managerData.actingCharacterList[managerData.nowSelectedChar];

        ActDataSet();
        OnSelectedChar?.Invoke(nowSelectedChar);
    }

    void ActDataSet()
    {
        foreach(SetSkillContext context in GetData.skillM_Data.Invoke().setSkillList)
        {
            managerData.actList.Add(new KeyValuePair<Character, ActSkillContext>(context.performer, context.nowSelectedActSkill));
        }
    }

    void SetActP()
    {
        IEnumerator Cor()
        {
            ActP.SetActive(true);

            OnSetAct?.Invoke();

            yield return new WaitForSeconds(1f);

            SetInput();
            SetReAct();
        }

        StartCoroutine(Cor());
    }

    void SetAlsoActer()
    {
        managerData.alsoSettingActers.Add(managerData.actList[managerData.nowActNum].Key);
    }

    void SetInput()
    {
        InputManager.manager.OnPressSpace += NextAct;
    }

    void RemoveInput()
    {
        InputManager.manager.OnPressSpace -= NextAct;
    }

    void SetReAct()
    {
        bool canReact = false;
        int removeCount = 0;

        List<KeyValuePair<Character,SkillData_ReAct>> list = new();
        if (OnReActFind != null)
        {
            foreach (Func<KeyValuePair<Character, SkillData_ReAct>> func in OnReActFind.GetInvocationList())
            {
                list.Add(func.Invoke());
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            int selectedReAct = UnityEngine.Random.Range(0, list.Count);
            int chance = UnityEngine.Random.Range(1, 101);

            if(list[selectedReAct].Value.actChance >= chance)
            {
                canReact = true;
                removeCount = selectedReAct;

                managerData.nowSelectedReAct = list[selectedReAct].Value;
                return;
            }
        }

        if(!canReact)
        {
            RemoveInput();
        }
    }

    void NextAct()
    {
        SetReAct();
        OnSetNextAct?.Invoke();

        managerData.nowActNum++;
        if(managerData.nowActNum >= managerData.actList.Count)
        {
            RemoveInput();
        }
    }
}
