using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI classNameText;
    public Image charImage;
    public string imageName;

    public TextMeshProUGUI maxHpValueText;
    public TextMeshProUGUI atkPwrValueText;
    public TextMeshProUGUI criPrbValueText;
    public TextMeshProUGUI criDmgValueText;
    public TextMeshProUGUI hpRcvValueText;
    public TextMeshProUGUI cdnPctValueText;
    public TextMeshProUGUI gotDmgValueText;
    public TextMeshProUGUI moveSpeedValueText;

    public GameObject[] skills; 


    void Start()
    {
        int charId = CSVDataReader.Instance.ReturnId();
        ResetUI(charId);
       
    }

   

    public void GoBattle()
    {
        CSVDataReader.Instance.MoveScene(SceneName.BATTLE_SCENE);

    }

    void ResetUI(int id)
    {
        string className = CSVDataReader.Instance.classData[id].className;
        classNameText.text = className;

        maxHpValueText.text = CSVDataReader.Instance.classData[id].maxHp.ToString();
        atkPwrValueText.text = CSVDataReader.Instance.classData[id].atkPwr.ToString();
        criPrbValueText.text = CSVDataReader.Instance.classData[id].criPrb.ToString();
        criDmgValueText.text = CSVDataReader.Instance.classData[id].criDmg.ToString();
        hpRcvValueText.text = CSVDataReader.Instance.classData[id].hpRcv.ToString();
        cdnPctValueText.text = CSVDataReader.Instance.classData[id].cdnPct.ToString();
        gotDmgValueText.text = CSVDataReader.Instance.classData[id].gotDmg.ToString();
        moveSpeedValueText.text = CSVDataReader.Instance.classData[id].moveSpeed.ToString();






        foreach (GameObject item in skills)
        {
            item.SetActive(false);
        }

        int len = 0;
        if (CSVDataReader.Instance.classData[id].skill_01 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_01;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }

        if (CSVDataReader.Instance.classData[id].skill_02 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_02;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }

        if (CSVDataReader.Instance.classData[id].skill_03 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_03;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }

        if (CSVDataReader.Instance.classData[id].skill_04 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_04;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }

        if (CSVDataReader.Instance.classData[id].skill_05 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_05;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }

        if (CSVDataReader.Instance.classData[id].skill_06 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_06;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }

        if (CSVDataReader.Instance.classData[id].skill_07 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_07;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }

        if (CSVDataReader.Instance.classData[id].skill_08 != -1)//배열에 있는 모든 애들을 끄고 쓸 애들만 키는 방식
        {
            int skillId = CSVDataReader.Instance.classData[id].skill_08;
            string skillName = CSVDataReader.Instance.skillData[skillId].skillName;
            TextMeshProUGUI _text = skills[len].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _text.text = skillName;

            skills[len].SetActive(true);
            len++;
        }
    }


    

    public void LRBtn(bool isLeftBtn)//1이면 4로가라, 4이면 1로가라 set int id를 매계변수로, id를 반환하는 함수로
    {
        int charId = CSVDataReader.Instance.ReturnId(isLeftBtn);
        ResetUI(charId);
    }
}
