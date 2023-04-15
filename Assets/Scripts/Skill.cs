using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData skillData;
    public SkillData SkillData { set { skillData = value; } get { return skillData;} }

    protected virtual void Awake() {

    }
}
