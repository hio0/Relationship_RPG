using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeManagerData
{
    public List<Character> ourCharacterList { get; set; } = new();
    public List<Character> actingCharacterList { get; set; } = new();
}

public class RangeManager : Manager_DataGiving<RangeManager, RangeManagerData>
{
    public RangeUI ourRange;
    public RangeUI enemyRange;

    public void SetRangeData()
    {
        managerData.ourCharacterList.Clear();
        managerData.actingCharacterList.Clear();

        managerData.ourCharacterList = ourRange.GetCharacter();

        managerData.actingCharacterList.AddRange(ourRange.GetCharacter());
        managerData.actingCharacterList.AddRange(enemyRange.GetCharacter());
    }
}
