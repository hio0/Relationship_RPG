using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RangeUI : FightViewMovingUI
{
    [Header("레인지")]
    public HorizontalLayoutGroup layoutGroup;
    public CanvasGroup can;

    [Header("전투 뷰")]
    public float fightSpacing;
    public float fightSize;
    public float fightPos;

    [Header("아군 턴 뷰")]
    public float ourTurnSpacing;
    public float ourTurnSize;
    public float ourTurnPos;

    protected virtual void MoveSpacing(float targetSpacing, float time)
    {
        StartCoroutine(Movement.LerpValue(layoutGroup.spacing, targetSpacing, time));
    }

    protected virtual void MoveSize(Vector2 targetSize, float time)
    {
        Movement.DoScale(rect, targetSize, time);
    }

    protected virtual void MoveMovement(Vector2 targetPos, float time)
    {
        Movement.DoAnchorMove(rect, targetPos, time);
    }

    protected virtual void MoveView(float spac, float size, float pos)
    {
        MoveSpacing(spac, MainData.fightView_moveSpeed);
        MoveSize(new Vector2(size, size), MainData.fightView_moveSpeed);
        MoveMovement(new Vector2(pos, rect.anchoredPosition.y), MainData.fightView_moveSpeed);
    }

    protected override void FightViewMove()
    {
        MoveView(fightSpacing, fightSize, fightPos);
    }

    protected override void OurViewMove()
    {
        MoveView(ourTurnSpacing, ourTurnSize, ourTurnPos);
    }

    public List<Character> GetCharacter()
    {
        List<Character> list = new List<Character>();

        if (transform.childCount == 0)
        {
            return null;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Character character = transform.GetChild(i).GetComponent<Character>();

            list.Add(character);
        }

        return list;
    }
}
