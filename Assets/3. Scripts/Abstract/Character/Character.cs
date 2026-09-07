using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [Header("기본 정보")]
    public CharacterData data;

    public string id { get; private set; }
    public string characterName { get; set; }

    public int hp { get; set; }
    public int maxHp { get; set; }
    public int dodgeChance { get; set; }

    public int minSpeed { get; set; }
    public int maxSpeed { get; set; }
    public int speed { get; set; }

    public int actCount { get; set; }

    public List<SkillData> skillList { get; set; } = new();

    [Header("컴포넌트")]
    public Image sprite;
    public CharacterEventTrigger trigger;

    public event Action OnDamaged;

    public abstract void Initialize(CharacterData data);

    // Start is called before the first frame update
    void OnEnable()
    {
        DefaultSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void DefaultSet()
    {
        id = Guid.NewGuid().ToString();
        characterName = data.defaultName;
        maxHp = data.maxHp;
        dodgeChance = data.defaultDodge;

        hp = maxHp;

        skillList.AddRange(data.skillList);
    }

    public void SetSpeed()
    {
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed + 1);
    }

    // 전투
    public void Damaged(int damage)
    {
        OnDamaged?.Invoke();
        hp -= damage;
    }
}
