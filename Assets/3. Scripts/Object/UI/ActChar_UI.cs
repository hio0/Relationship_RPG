using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActChar_UI : MoveUI
{
    [SerializeField] Character myChar;
    [SerializeField] Image characterImage;

    bool isMoved;
    
    public void Initialize(Character character, bool isMoved)
    {
        myChar = character;
        this.isMoved = isMoved;
    }

    // Start is called before the first frame update
    void Start()
    {
        CombatManager.manager.OnSetNextAct += CanActSet;
    }

    private void OnDisable()
    {
        CombatManager.manager.OnSetNextAct -= CanActSet;
    }

    void CanActSet()
    {

    }

    void SetImage()
    {
        CombatManagerData rangeData = GetData.combatM_Data.Invoke();

        //characterImage.sprite = rangeData.nowSkillActs[].data.artData.FindSpriteToSkill(rangeData.nowActSkill.useSkill);
    }

    protected override void Move()
    {
        Movement.DoAnchorMove(rect, openPos, MainData.characterSkill_moveSpeed);
    }

    protected override void ResetPos()
    {
        rect.anchoredPosition = closePos;     
    }
}
