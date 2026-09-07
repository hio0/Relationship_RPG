using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkillData_Act")]
public class SkillData_Act : SkillData
{
    public enum SkillTarget
    {
        performer,
        hero,
        enemy
    }
    public SkillTarget skillTarget;
}
