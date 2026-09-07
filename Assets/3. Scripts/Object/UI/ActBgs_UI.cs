using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActBgs_UI : ActObjectUI
{
    [SerializeField] ActBg_UI pre_actBg;

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
            MakeActChar(data.actList[data.nowActNum].Value.targets[i]);
        }
    }

    protected override void NextAct()
    {
        CombatManagerData data = GetData.combatM_Data.Invoke();
    }

    void MakeActChar(Character character)
    {
        CombatManagerData data = GetData.combatM_Data.Invoke();

        bool isHero = SetTeam(character);

        ActBg_UI act = Instantiate(pre_actBg, SetRange(isHero));
        act.Initialize(character, data.actList[data.nowActNum].Value.useSkill.skillBgColor, isHero);
    }
}
