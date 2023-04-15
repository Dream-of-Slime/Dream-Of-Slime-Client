using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Fire_1 : Skill
{
    SkillManager SM;

    void Awake()
    {
        SM = SkillManager.instance;
    }

    void Update()
    {
        transform.position += transform.up * (SkillData._speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
