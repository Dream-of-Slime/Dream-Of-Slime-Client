using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemMove : MonoBehaviour
{
    [SerializeField] float _minSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _minAngle;
    [SerializeField] float _maxAngle;
    float _speed;
    Vector2 _dir;
    float _angle;


    void Awake()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _angle = Random.Range(Mathf.Tan(_minAngle), Mathf.Tan(_maxAngle));
    }

    void OnEnable()
    {
        _dir = new Vector2(_angle, 1);
        _dir = _dir.normalized;
        _angle = Mathf.Atan2(-_dir.x, _dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * _angle);
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x + _dir.x * (_speed * Time.deltaTime), transform.position.y + _dir.y * (_speed * Time.deltaTime));
    }
}
