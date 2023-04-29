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
    bool _canReflect;

    [HideInInspector] public int _wall;

    void OnEnable()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _angle = Random.Range(Mathf.Tan(Mathf.Deg2Rad * _minAngle), Mathf.Tan(Mathf.Deg2Rad * _maxAngle));
        if (_wall == 0)
        {
            _dir = new Vector2(1, _angle);
        }
        else if (_wall == 1)
        {
            _dir = new Vector2(_angle, -1);
        }
        else if (_wall == 2)
        {
            _dir = new Vector2(-1, _angle);
        }
        _dir = _dir.normalized;
        //_angle = Mathf.Atan2(-_dir.x, _dir.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(Vector3.forward * _angle);
        _canReflect = false;
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x + _dir.x * (_speed * Time.deltaTime), transform.position.y + _dir.y * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" && _canReflect)
        {
            _dir = Vector2.Reflect(_dir, collision.transform.right);
            _dir = _dir.normalized;
            //_angle = Mathf.Atan2(-_dir.x, _dir.y) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(Vector3.forward * _angle);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            _canReflect = true;
        }
    }
}
