using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public string skillID;
    public enum SkillType
    {
        Act,
        ReAct
    }
    public SkillType skillType;

    public int minDamage;
    public int maxDamage;

    public int accuracy;
    public float calledShotChance;

    public Color32 skillBgColor;
    [TextArea] public string skillExplanation;

    [SerializeReference, SubclassSelector] public List<SkillEffect> effects;

    // 템플렛
    public int SetDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public bool CheckHit(Character target)
    {
        float hitChance = this.accuracy - target.dodgeChance;

        return Random.value * 100f < hitChance;
    }
}
