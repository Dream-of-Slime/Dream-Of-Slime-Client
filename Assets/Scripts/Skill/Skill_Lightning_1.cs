using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lightning_1 : Skill
{
    public List<GameObject> FoundEnemies;
    public List<GameObject> SelectEnemies;
    public List<GameObject> LightningChild;
    public GameObject _skillsLightningPrefab;
    private bool is_first = true;
    

    void OnEnable() {
        if(is_first) {
            is_first = false;
            for (int i = 0; i < SkillData._attackCount; i++)
            {
                GameObject temp = Instantiate(_skillsLightningPrefab, this.gameObject.transform);
                temp.SetActive(false);
                LightningChild.Add(temp);
            }
            return;
        }

        FoundEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        SelectEnemies = new List<GameObject>();

        if(FoundEnemies.Count <= SkillData._attackCount){
            foreach (GameObject enemy in FoundEnemies) {
                SelectEnemies.Add(enemy);
            }
        }else{
            for (int i = 0; i < SkillData._attackCount; i++)
            {
                FindMonster();
            }
        }

        makeLightning();
    }

    void FindMonster(){
        int random = Random.Range(0, FoundEnemies.Count);
        
        foreach (GameObject enemy in SelectEnemies)
        {
            if(FoundEnemies[random] == enemy)  {
                FindMonster();
                return;
            }
        }

        SelectEnemies.Add(FoundEnemies[random]);
    }

    void makeLightning()
    {
        for(int i = 0;i<SelectEnemies.Count;i++){
            LightningChild[i].transform.position = SelectEnemies[i].transform.position;
            LightningChild[i].SetActive(true);
        }

        StartCoroutine(activeFalseMyself());
    }

    IEnumerator activeFalseMyself()
    {
        yield return new WaitForSeconds( SkillData._usingTime + SkillData._attackDuration );
        
        this.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }
}
