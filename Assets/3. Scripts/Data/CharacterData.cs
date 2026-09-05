using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    [Header("캐릭터 정보")]
    public string iD;
    public string defaultName;

    public int maxHp;
    public int defaultDodge;
    public int minSpeed;
    public int maxSpeed;

    public CharacterArtData artData;

    public List<SkillData> skillList = new();
}