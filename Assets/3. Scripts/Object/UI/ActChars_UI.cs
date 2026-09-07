using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActChars_UI : ActObjectUI
{
    [SerializeField] ActChar_UI pre_actChar;
    [SerializeField] float targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void ActSet()
    {
        CombatManagerData data = GetData.combatM_Data.Invoke();

        for (int i = 0; i < data.actList[data.nowActNum].Value.targets.Count; i++)
        {
            MakeActChar(data.actList[data.nowActNum].Value.targets[i], false);
        }
    }

    protected override void NextAct()
    {
        CombatManagerData data = GetData.combatM_Data.Invoke();

    }

    void MakeActChar(Character character, bool ismove)
    {
        bool isHero = SetTeam(character);

        ActChar_UI act = Instantiate(pre_actChar, SetRange(isHero));
        act.Initialize(character, SetPos(isHero, targetPos));
        
        if(!ismove)
        {
            act.rect.anchoredPosition = new Vector2(targetPos, act.rect.anchoredPosition.y);
        }

        SetAlsoChar(character);
    }
}
