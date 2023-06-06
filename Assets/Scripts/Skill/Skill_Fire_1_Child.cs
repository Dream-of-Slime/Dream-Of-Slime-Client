
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Fire_1_Child : Skill
{
    private bool is_first = true;
    SpriteRenderer _sprite;

    void OnEnable()
    {
        if (is_first)
        {
            is_first = false;
            _sprite = GetComponent<SpriteRenderer>();
            return;
        }

        this.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(makeFire());
    }

    IEnumerator makeFire()
    {
        this.GetComponent<Collider2D>().enabled = true;

        float duration = 0;

        while (duration < skillData._usingTime)
        {
            duration += Time.deltaTime;
            //_sprite.color = new Color(1, 1, 1, 1);
            _sprite.color = new Color(1, 1, 1, Mathf.Clamp01(1 - (duration / skillData._usingTime)));
            yield return new WaitForEndOfFrame();
        }

        //yield return new WaitForSeconds(SkillData._usingTime);

        this.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }
}
