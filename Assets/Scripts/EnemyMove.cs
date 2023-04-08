using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    PlayerMove PM;

    [SerializeField] float Speed;
    Transform Player;
    Vector2 Dir;
    float Angle;


    void Awake()
    {
        PM = PlayerMove.instance;
        Player = PM.transform;
    }

    void OnEnable()
    {

        Dir = (Player.transform.position - transform.position);
        Dir = Dir.normalized;
        Angle = Mathf.Atan2(-Dir.x, Dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * Angle);
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x + Dir.x * (Speed * Time.deltaTime), transform.position.y + Dir.y * (Speed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
