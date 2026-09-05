using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    [Header("원정대원 정보")]
    public HeroData heroData;

    public int mana { get; set; }
    public int maxMana { get; set; }

    public List<Emotion> emotionList = new();
    public List<Relation> relationList = new();
    public List<Identity> identityList = new();

    public override void Initialize(CharacterData data)
    {
        heroData = (HeroData)data;

        DefaultSet();
    }

    protected override void DefaultSet()
    {
        this.data = heroData;
        base.DefaultSet();

        maxMana = heroData.maxMana;
        mana = maxMana;
    }
}
