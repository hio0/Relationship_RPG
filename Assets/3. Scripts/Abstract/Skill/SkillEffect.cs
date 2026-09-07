using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SkillEffect
{
    public enum EffectTarget
    {
        target,         
        performer,      
        party,          
        enemyParty,     
    }
    public EffectTarget target;
    public int chance;

    public bool onHit;
    public bool onMiss;

    public abstract void Execute(SkillContext context);
}
