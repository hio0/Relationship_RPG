using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContext
{
    public Character performer;
    public List<Character> targets = new();
    public SkillData skillData;
}

[Serializable]
public abstract class Skill
{
    protected SkillContext data = new();

    public virtual void Initialize(SkillContext context)
    {
        data = context;
    }

    public virtual void Use()
    {
        foreach(Character target in data.targets)
        {
            bool hit = data.skillData.CheckHit(target);

            if(hit)
            {
                target.Damaged(data.skillData.SetDamage());
            }
        }
    }
}
