using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : RangeUI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FightViewMove()
    {
        base.FightViewMove();
        Movement.DOFade(can, 0.4f, MainData.fightView_moveSpeed);
    }

    protected override void OurViewMove()
    {
        base.OurViewMove();
        Movement.DOFade(can, 1f, MainData.fightView_moveSpeed);
    }
}
