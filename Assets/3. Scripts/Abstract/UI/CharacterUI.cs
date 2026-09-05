using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterUI : MonoBehaviour
{
    protected Character myChar;

    public virtual void Initialize(Character character)
    {
        myChar = character;
    }
}
