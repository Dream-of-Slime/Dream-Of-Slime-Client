using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Skill" || collision.tag == "Enemy" || collision.tag == "SkillItem")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
