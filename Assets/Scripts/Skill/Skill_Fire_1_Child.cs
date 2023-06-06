
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Fire_1_Child : Skill
{
    private bool is_first = true;

    void OnEnable()
    {
        if (is_first)
        {
            is_first = false;
            return;
        }

        this.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(makeFire());
    }

    IEnumerator makeFire()
    {
        this.GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(SkillData._usingTime);

        this.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }
}
