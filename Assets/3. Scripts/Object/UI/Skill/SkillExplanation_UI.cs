using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillExplanation_UI : MonoBehaviour
{
    [SerializeField] CanvasGroup can;
    [SerializeField] CanvasGroup textCan;

    [SerializeField] TMP_Text skillNameT;
    [SerializeField] TMP_Text explanationT;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        can.alpha = 0;
        SkillManager.manager.OnSkillFind += SetExplanation;
    }

    private void OnDisable()
    {
        SkillManager.manager.OnSkillFind -= SetExplanation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetExplanation()
    {
        Movement.DOFade(can, 1f, 0.5f);
        textCan.alpha = 1f;
        Movement.DOFade(textCan, 1f, 0.5f);

        SkillData skill = GetData.nowSelectedSkill.Invoke().useSkill;

        skillNameT.text = skill.skillID;
        explanationT.text = skill.skillExplanation;

        StopAllCoroutines();
        if(explanationT.textInfo.lineCount >= 5)
        {
            StartCoroutine(AutoScroll());
        }
    }

    IEnumerator AutoScroll()
    {
        yield return new WaitForSeconds(2f);
        
        while(true)
        {
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;

            if (scrollRect.verticalNormalizedPosition <= 0f)
            {
                yield return new WaitForSeconds(2f);

                float fadeSpeed = 0.5f;
                Movement.DOFade(textCan, 0f, fadeSpeed);

                yield return new WaitForSeconds(fadeSpeed);

                scrollRect.verticalNormalizedPosition = 1f;
                Movement.DOFade(textCan, 1f, fadeSpeed);

                yield return new WaitForSeconds(2f);
            }
            yield return null;
        }
    }
}
