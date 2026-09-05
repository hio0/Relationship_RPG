using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : FightViewMovingUI
{
    [Header("전투 뷰")]
    [SerializeField] float fightPos;
    [SerializeField] float fightHight;

    [Header("아군 턴 뷰")]
    [SerializeField] float ourTurnPos;
    [SerializeField] float outTurnHeight;

    protected override void FightViewMove()
    {
        Move_FightView(fightPos, fightHight);
    }

    protected override void OurViewMove()
    {
        Move_FightView(ourTurnPos, outTurnHeight);
    }

    void Move_FightView(float pos, float height)
    {
        Movement.DoRotation(rect, new Vector3(0, 0, pos), MainData.fightView_moveSpeed);
        Movement.DoSizeMove(rect, new Vector2(rect.sizeDelta.x, height), MainData.fightView_moveSpeed);
    }
}
