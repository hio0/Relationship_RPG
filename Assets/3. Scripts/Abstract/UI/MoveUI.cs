using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveUI : MonoBehaviour
{
    [Header("움직임 UI")]
    public RectTransform rect;

    public Vector2 closePos;
    public Vector2 openPos;

    public float moveSpeed;

    protected virtual void Awake()
    {
        ResetPos();
    }

    protected virtual void Move()
    {
        
    }
    protected virtual void ResetPos()
    {
        
    }
}
