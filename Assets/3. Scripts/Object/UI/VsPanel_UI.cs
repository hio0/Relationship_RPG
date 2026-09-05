using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsPanel_UI : MoveUI
{
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    protected override void Move()
    {
        ResetPos();
        Movement.DoOutEaseMove(rect, openPos, moveSpeed);
    }
}
