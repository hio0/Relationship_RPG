using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharArtData
{
    public enum CharacterSprite
    {
        icon,
        standing,
        back
    }

    public CharacterSprite type;
    public Sprite sprite;
}

[CreateAssetMenu(menuName = "Data/CharacterArtData")]
public class CharacterArtData : ScriptableObject
{
    public List<CharArtData> charArtData = new();
}
