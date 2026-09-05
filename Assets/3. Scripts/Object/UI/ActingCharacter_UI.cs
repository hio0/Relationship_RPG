using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActingCharacter_UI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image bg;

    void OnEnable()
    {
        FightManager.manager.OnFightView += ResetPos;
        FightManager.manager.OnOurTurnStart += Move;
    }

    private void OnDisable()
    {
        FightManager.manager.OnFightView -= ResetPos;
        FightManager.manager.OnOurTurnStart -= Move;
    }

    void Move()
    {
        ResetPos();
        Movement.DoFillAmount_Cubic(bg, 1, MainData.fightView_moveSpeed);
    }

    void ResetPos()
    {
        bg.fillAmount = 0;
    }
}
