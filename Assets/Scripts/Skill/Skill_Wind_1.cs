using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Wind_1 : Skill
{
    private bool is_first = true;

    Transform _player;

    public override void Update()
    {
        base.Update();

        transform.position = _player.transform.position;
    }

    void OnEnable() {
        if(is_first) {
            is_first = false;
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        StartCoroutine(makeWindAttack());
    }

    IEnumerator makeWindAttack()
    {
        yield return new WaitForSeconds( SkillData._attackDuration );
        
        this.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.CompareTag("Enemy")) {
            collision.gameObject.SetActive(false);
        }
    }    
}
