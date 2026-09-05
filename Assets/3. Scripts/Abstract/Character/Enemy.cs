using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("적 정보")]
    public EnemyData enemyData;

    public override void Initialize(CharacterData data)
    {
        enemyData = (EnemyData)data;

        DefaultSet();
    }

    protected override void DefaultSet()
    {
        base.DefaultSet();
    }
}
