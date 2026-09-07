using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 여러 데이터들이 오고가는 곳
/// </summary>
public static class GetData
{
    public static Func<RangeManagerData> rangeM_Data;
    public static Func<SkillManagerData> skillM_Data;
    public static Func<CombatManagerData> combatM_Data;

    public static Func<SkillIconData> nowSelectedSkill;
}
