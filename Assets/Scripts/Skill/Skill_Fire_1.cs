using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Fire_1 : Skill
{
    public override void Update()
    {
        base.Update();

        transform.position += transform.up * (SkillData._speed * Time.deltaTime);
    }
}
