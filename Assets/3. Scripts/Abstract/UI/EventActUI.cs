using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventActUI : MonoBehaviour
{
    protected virtual void OnDisable()
    {
        RemoveEvent();
    }

    protected abstract void EventSet();
    protected abstract void RemoveEvent();
}
