using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterChildUI : EventActUI
{
    protected Character myChar;

    protected virtual void OnEnable()
    {
        myChar = GetComponentInParent<Character>();
    }
}
