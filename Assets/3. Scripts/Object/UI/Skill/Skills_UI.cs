using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills_UI : ParentUI
{
    [SerializeField] SkillIcon_UI pre_skillIcon;

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
            bool firstSet = true;

            for (int i = 0; i < data.setSkillList[data.nowSelectedHero].targetContexts.Count; i++)
            {
                SkillIcon_UI icon = Instantiate(pre_skillIcon, transform);
                icon.Initialize(data.setSkillList[data.nowSelectedHero].targetContexts[i], 0.5f);

                if (data.setSkillList[data.nowSelectedHero].nowSelectedContext == data.setSkillList[data.nowSelectedHero].targetContexts[i])
                {
                    firstSet = false;
                }

                yield return null;
            }

            
            for (int i = 0; i < transform.childCount; i++)
            {
                SkillIcon_UI icon = transform.GetChild(i).GetComponent<SkillIcon_UI>();

                if(firstSet)
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
            
        }

        StopAllCoroutines();
        StartCoroutine(Cor());
    }
}
