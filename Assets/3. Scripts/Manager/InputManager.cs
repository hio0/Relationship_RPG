using System;
using UnityEngine;

public class InputManager : Manager<InputManager>
{
    public event Action OnPressingA;
    public event Action OnPressA;
    public event Action OnPressingD;
    public event Action OnPressD;
    public event Action OnPressE;

    public event Action OnPressSpace;
    public event Action OnPressTab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            OnPressingA?.Invoke();
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnPressingD?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnPressA?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnPressD?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnPressE?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnPressSpace?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnPressTab?.Invoke();
        }
    }
}
