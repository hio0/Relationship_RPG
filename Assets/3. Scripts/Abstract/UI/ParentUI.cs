using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentUI : MonoBehaviour
{
    protected void DestroyAllChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
