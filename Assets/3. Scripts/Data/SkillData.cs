using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillID;
    public enum SkillType
    {
        Act,
        ReAct
    }
    public SkillType skillType;
    
    public enum SkillTarget
    {
        performer,
        hero,
        enemy
    }
    public SkillTarget skillTarget;

    public int minDamage;
    public int maxDamage;

    public int accuracy;
    public float calledShotChance;

    [TextArea] public string skillExplanation;

    [SerializeReference, SubclassSelector] public List<SkillEffect> effects;

    // 템플렛
    protected bool CheckHit(Character target, SkillData skill)
    {
        float hitChance = skill.accuracy - target.dodgeChance;

        return Random.value * 100f < hitChance;
    }
}
