
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Wind_3_Child : Skill
{
    public override void Update()
    {
        base.Update();

        transform.position += transform.up * (SkillData._speed * Time.deltaTime);
    }
}
