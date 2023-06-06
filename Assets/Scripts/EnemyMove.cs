using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    PlayerMove PM;

    [SerializeField] float _speed;
    Transform _player;
    Vector2 _dir;
    float _angle;


    void Awake()
    {
        PM = PlayerMove.instance;
        _player = PM.transform;
    }

    void OnEnable()
    {
        _dir = (_player.transform.position - transform.position);
        _dir = _dir.normalized;
        _angle = Mathf.Atan2(-_dir.x, _dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * _angle);
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x + _dir.x * (_speed * Time.deltaTime), transform.position.y + _dir.y * (_speed * Time.deltaTime));
    }
}
