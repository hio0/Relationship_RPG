using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurRange : RangeUI
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
        can.alpha = 1f;
        can.blocksRaycasts = true;
    }

    protected override void OurViewMove()
    {
        base.OurViewMove();
        Movement.DOFade(can, 0f, MainData.fightView_moveSpeed);
        can.blocksRaycasts = false;
    }
}
