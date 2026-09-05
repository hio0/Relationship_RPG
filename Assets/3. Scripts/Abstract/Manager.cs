using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager_DataGiving<T, TData> : Manager<T>
    where T : Manager<T> where TData : new()
{
    public TData managerData = new();

    protected TData GiveData()
    {
        return managerData;
    }
}

public abstract class Manager<T> : MonoBehaviour
    where T : Manager<T> // 제너릭을 제한하는 문법
{
    public static T manager;

    protected virtual void Awake()
    {
        manager = (T)this;

        DesfaultSet();
    }

    // 템플렛
    protected virtual void DesfaultSet()
    {

    }
}
