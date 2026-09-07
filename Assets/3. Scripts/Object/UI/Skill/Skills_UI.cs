using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Skills_UI : ParentUI
{
    [SerializeField] SkillIcon_Act pre_actIcon;
    [SerializeField] SkillIcon_ReAct pre_reactIcon;

    // Start is called before the first frame update
    private void OnEnable()
    {
        SkillManager.manager.OnActerSelected += InfoMoveAside;
    }

    private void OnDisable()
    {
        SkillManager.manager.OnActerSelected -= InfoMoveAside;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void InfoMoveAside()
    {
        IEnumerator Cor()
        {
            DestroyAllChild();

            SkillManagerData data = GetData.skillM_Data.Invoke();

            for (int i = 0; i < data.setSkillList[data.nowSelectedHero].drawSkillList.Count; i++)
            {
                SkillIconUI icon = null;
                SkillData nowSkill = data.setSkillList[data.nowSelectedHero].drawSkillList[i];

                if (nowSkill.skillType == SkillData.SkillType.Act)
                {
                    icon = pre_actIcon;
                }
                else
                {
                    icon = pre_reactIcon;
                }

                SkillIconUI skillIcon = Instantiate(icon, transform);
                skillIcon.Initialize(nowSkill, 0.5f);

                yield return null;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                SkillIconUI icon = transform.GetChild(i).GetComponent<SkillIconUI>();

                if(!data.setSkillList[data.nowSelectedHero].almostSet)
                {
                    icon.FirstSetMove();

                    yield return new WaitForSeconds(0.3f);
                }
                else
                {
                    icon.SetMove();

                    yield return null;
                }
            }

            if (!data.setSkillList[data.nowSelectedHero].almostSet)
            {
                data.setSkillList[data.nowSelectedHero].almostSet = true;
            }
        }

        StopAllCoroutines();
        StartCoroutine(Cor());
    }
}
