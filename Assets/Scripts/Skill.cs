using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData skillData;
    public SkillData SkillData { set { skillData = value; } get { return skillData;} }

    public string name;
    public int skill;
    public int count;

    protected virtual void Awake() {

    }
}
