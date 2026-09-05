using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo_UI : ParentUI
{
    List<OurCharacterInfo_UI> infoList = new();

    [SerializeField] OurCharacterInfo_UI pre_characterInfo;

    // Start is called before the first frame update
    void OnEnable()
    {
        SetInfo();
        FightManager.manager.OnTurnStart += InfoMoveAside;
    }

    private void OnDisable()
    {
        FightManager.manager.OnTurnStart -= InfoMoveAside;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetInfo()
    {
        RangeManagerData data = GetData.rangeM_Data.Invoke();

        DestroyAllChild();

        for (int i = 0; i < data.ourCharacterList.Count; i++)
        {
            OurCharacterInfo_UI info = Instantiate(pre_characterInfo, transform);
            info.Initialize(data.ourCharacterList[i]);

            infoList.Add(info);
        }
    }

    void GetInfo()
    {
        infoList.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            OurCharacterInfo_UI info = transform.GetChild(i).GetComponent<OurCharacterInfo_UI>();
            infoList.Add(info);
        }
    }

    void InfoMoveAside()
    {
        GetInfo();

        IEnumerator Cor()
        {
            float duration = MainData.fightView_moveSpeed / infoList.Count;
            float wait = duration - 0.2f;

            for (int i = 0; i < infoList.Count; i++)
            {
                OurCharacterInfo_UI info = infoList[i];
                info.MoveAside(duration);

                yield return new WaitForSeconds(wait);
            }
        }

        StartCoroutine(Cor());
    }
}
