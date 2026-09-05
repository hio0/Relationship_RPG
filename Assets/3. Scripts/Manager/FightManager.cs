using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : Manager<FightManager>
{
    public event Action OnFightStart;

    public event Action OnTurnStart;
    public event Action OnOurTurnStart;
    public event Action OnEnemyTurnStart;

    public event Action OnFightView;
    public event Action OnOurView;
    public event Action OnEnemyView;

    [Header("오브젝트")]
    [SerializeField] GameObject startP;
    [SerializeField] GameObject fightP;

    [SerializeField] float fightMove;

    // Start is called before the first frame update
    void Start()
    {
        MainData.fightView_moveSpeed = fightMove;

        FightStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void DesfaultSet()
    {
        startP.SetActive(false);
        fightP.SetActive(false);
    }

    void FightStart()
    {
        IEnumerator Cor()
        {
            startP.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            startP.SetActive(false);
            OnFightView?.Invoke();

            yield return new WaitForSeconds(1.5f);

            TurnStart();
        }

        OnFightStart?.Invoke();
        StartCoroutine(Cor());
    }

    void TurnStart()
    {
        fightP.SetActive(true);

        OnTurnStart?.Invoke();
        OnFightView?.Invoke();
        OurTurnStart();
    }

    void OurTurnStart()
    {
        IEnumerator Cor()
        {
            OnOurTurnStart?.Invoke();

            yield return new WaitForSeconds(0.3f);

            OnOurView?.Invoke();
        }

        StartCoroutine(Cor());
    }

    void EnemyTurnStart()
    {
        OnEnemyTurnStart?.Invoke();
        OnEnemyView?.Invoke();
    }
}
