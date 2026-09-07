using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NowActer_UI : MonoBehaviour
{
    [SerializeField] TMP_Text actT;
    [SerializeField] CanvasGroup can;

    // Start is called before the first frame update
    void Start()
    {
        CombatManager.manager.OnSelectedChar += SetText;
    }

    private void OnDisable()
    {
        CombatManager.manager.OnSelectedChar -= SetText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetText(Character character)
    {
        IEnumerator Cor()
        {
            can.alpha = 1f;
            SkillManagerData skillData = GetData.skillM_Data.Invoke();

            actT.text = $"{character.characterName}의 {skillData.FindCharactersContext(character).nowSelectedActSkill.useSkill.skillID}";

            yield return new WaitForSeconds(1.5f);
            
            Movement.DOFade(can, 0f, 1f);
        }

        StartCoroutine(Cor());
    }
}
