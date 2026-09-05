using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterEventTrigger : CharacterComponent
{
    public EventTrigger trigger;

    public event Action OnEnter;
    public event Action OnClick;
    public event Action OnExit;

    void OnEnable()
    {
        SetTrigger();
    }

    void OnDisable()
    {
        if(trigger != null)
        {
            trigger.triggers.Clear();
        }
    }

    public void AddEvent(EventTriggerType type, Action action)
    {
        if (trigger.triggers == null)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;

        entry.callback.AddListener(_ => action.Invoke());

        trigger.triggers.Add(entry);
    }

    public void RemoveEvent(EventTriggerType eventType)
    {
        trigger.triggers.RemoveAll(e => e.eventID == eventType);
    }

    // Fight
    void SetTrigger()
    {
        AddEvent(EventTriggerType.PointerEnter, OnEnter);
        AddEvent(EventTriggerType.PointerClick, OnClick);
        AddEvent(EventTriggerType.PointerExit, OnExit);
    }
}
