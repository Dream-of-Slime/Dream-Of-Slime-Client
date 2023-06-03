using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData skillData;
    public SkillData SkillData { set { skillData = value; } get { return skillData;} }
    SkillManager SM;

    public virtual void Awake() {
        SM = SkillManager.instance;
    }

    public virtual void Update()
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            ScoreManager.score++;
            Debug.Log(ScoreManager.score);
        }
    }
}
