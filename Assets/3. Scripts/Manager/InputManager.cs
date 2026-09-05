using System;
using UnityEngine;

public class InputManager : Manager<InputManager>
{
    public event Action OnPressingA;
    public event Action OnPressA;
    public event Action OnPressingD;
    public event Action OnPressD;
    public event Action OnPressE;
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
        if (Input.GetKeyUp(KeyCode.D))
        {
            OnPressD?.Invoke();
        }

        if (Input.GetKey(KeyCode.E))
        {
            OnPressE?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnPressTab?.Invoke();
        }
    }
}
