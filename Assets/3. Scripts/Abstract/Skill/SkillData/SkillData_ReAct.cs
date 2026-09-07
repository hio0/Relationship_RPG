using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkillData_ReAct")]
public class SkillData_ReAct : SkillData
{
    public enum SkillTerms
    {
        attack,
        damaged
    }
    public SkillTerms skillTerms;

    public int actChance;
}
