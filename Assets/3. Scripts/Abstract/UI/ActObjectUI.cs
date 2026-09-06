using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class ActObjectUI : MonoBehaviour
{
    public Transform heroRange;
    public Transform enemyRange;

    // Start is called before the first frame update
    void Start()
    {
        CombatManager.manager.OnSetAct += ActSet;
        CombatManager.manager.OnSetNextAct += NextAct;
    }

    private void OnDisable()
    {
        CombatManager.manager.OnSetAct -= ActSet;
        CombatManager.manager.OnSetNextAct -= NextAct;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void ActSet();
    protected abstract void NextAct();

    protected virtual bool SetTeam(Character character)
    {
        bool isHero = false;

        if (character is Hero)
        {
            isHero = true;
        }

        return isHero;
    }

    protected virtual Transform SetRange(bool isHero)
    {
        Transform parent = null;

        if (isHero)
        {
            parent = heroRange;
        }
        else
        {
            parent = enemyRange;
        }

        return parent;
    }
}
