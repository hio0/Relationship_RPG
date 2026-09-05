using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeManagerData
{
    public List<Character> ourCharacterList { get; set; } = new();
    public List<Character> actingCharacterList { get; set; } = new();

    public int nowSelectedNum { get; set; }
}

public class RangeManager : Manager_DataGiving<RangeManager, RangeManagerData>
{
    public RangeUI ourRange;
    public RangeUI enemyRange;

    public event Func<KeyValuePair<int, Character>> OnActerFind;
    public event Action OnActingCharSelected;
    public event Action<Character> OnSelectedChar;

    void OnEnable()
    {
        FightManager.manager.OnTurnStart += SetActingTurn;

        GetData.rangeM_Data += GiveData;
        managerData.ourCharacterList = ourRange.GetCharacter();
        InputManager.manager.OnPressTab += SetNowChar;
    }

    private void OnDisable()
    {
        FightManager.manager.OnTurnStart -= SetActingTurn;

        GetData.rangeM_Data -= GiveData;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetActingTurn()
    {
        managerData.nowSelectedNum = 0;
        managerData.actingCharacterList.Clear();

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

    void SetNowChar()
    {
        Character nowSelectedChar = managerData.actingCharacterList[managerData.nowSelectedNum];

        OnSelectedChar?.Invoke(ourRange.GetCharacter()[0]);
    }
}
