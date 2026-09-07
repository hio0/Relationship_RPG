using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GiveDataClass<TData> : MonoBehaviour
    where TData : new()
{
    public TData data = new();

    protected TData GiveData()
    {
        return data;
    }
}
