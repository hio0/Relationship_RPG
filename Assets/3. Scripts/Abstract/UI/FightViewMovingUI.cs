using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightViewMovingUI : MonoBehaviour
{
    public RectTransform rect;

    protected virtual void OnEnable()
    {
        FightManager.manager.OnFightView += FightViewMove;
        FightManager.manager.OnOurView += OurViewMove;
        FightManager.manager.OnEnemyView += EnemyViewMove;
    }

    protected virtual void OnDisable()
    {
        FightManager.manager.OnFightView -= FightViewMove;
        FightManager.manager.OnFightStart -= OurViewMove;
        FightManager.manager.OnOurTurnStart -= EnemyViewMove;
    }

    protected virtual void FightViewMove()
    {

    }
    protected virtual void OurViewMove()
    {

    }
    protected virtual void EnemyViewMove()
    {

    }
}
