using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Wind_3 : Skill_Wind_1
{
    public GameObject _skillsWindShotPrefab;

    public List<GameObject> WindShotChild;

    private bool is_first = true;

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable() {
        base.OnEnable();

        if(is_first) {
            for(int i=0;i<SkillData._attackCount;i++){
                GameObject temp = Instantiate(_skillsWindShotPrefab, this.gameObject.transform);
                temp.SetActive(false);
                WindShotChild.Add(temp);
            }
            is_first = false;
            return;
        }

        for(int i=0;i<SkillData._attackCount;i++){
            WindShotChild[i].transform.position = _player.position;
            float st = _player.eulerAngles.z + 120f * i;
            WindShotChild[i].transform.eulerAngles = new Vector3(_player.eulerAngles.x, _player.eulerAngles.y, st);
            WindShotChild[i].SetActive(true);
        }
    }
   
}
