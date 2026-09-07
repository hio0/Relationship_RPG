using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActChar_UI : MoveUI
{
    [SerializeField] Character myChar;
    [SerializeField] Image characterImage;

    public void Initialize(Character character, float targetPos)
    {
        myChar = character;
        openPos = new Vector2(targetPos, rect.anchoredPosition.y);

        Move();
    }

    // Start is called before the first frame update
    void Start()
    {
        CombatManager.manager.OnSetNextAct += Move;
    }

    private void OnDisable()
    {
        CombatManager.manager.OnSetNextAct -= Move;
    }

    void SetImage()
    {
        CombatManagerData rangeData = GetData.combatM_Data.Invoke();

        //characterImage.sprite = rangeData.nowSkillActs[].data.artData.FindSpriteToSkill(rangeData.nowActSkill.useSkill);
    }

    protected override void Move()
    {
        Movement.DoAnchorMove(rect, openPos, MainData.characterSkill_moveSpeed);
        SetImage();
    }

    protected override void ResetPos()
    {
        rect.anchoredPosition = closePos;
    }
}
