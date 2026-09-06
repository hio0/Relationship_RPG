using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharArtData
{
    public enum CharacterSprite
    {
        icon,
        standing,
        back
    }

    public CharacterSprite type;
    public Sprite sprite;
}

[Serializable]
public class SkillArtData
{
    public SkillData skillData;
    public Sprite sprite;
}


[CreateAssetMenu(menuName = "Data/CharacterArtData")]
public class CharacterArtData : ScriptableObject
{
    public List<CharArtData> charArtData = new();

    public List<SkillArtData> skillArtData = new();

    public Sprite FindSpriteToSkill(SkillData data)
    {
        for (int i = 0; i < skillArtData.Count; i++)
        {
            if (skillArtData[i].skillData == data)
            {
                return skillArtData[i].sprite;
            }
        }

        return null;
    }
}
