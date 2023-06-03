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
    [HideInInspector] public string _type;

    void OnEnable()
    {
        if (_type != null)
        {
            Sprite sprite = SkillManager.instance._skillsIcons.Find(x => x.name == _type);
            GetComponent<SpriteRenderer>().sprite = sprite;
        }

        _speed = Random.Range(_minSpeed, _maxSpeed);

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

        if (_wall == 0)
        {
            if (transform.position.y < 0)
            {
                _angle = Random.Range(0, Mathf.Tan(Mathf.Deg2Rad * _maxAngle));
            }
            else
            {
                _angle = Random.Range(Mathf.Tan(Mathf.Deg2Rad * _minAngle), 0);
            }

            _dir = new Vector2(1, _angle);
        }
        else if (_wall == 1)
        {
            if (transform.position.x < 0)
            {
                _angle = Random.Range(0, Mathf.Tan(Mathf.Deg2Rad * _maxAngle));
            }
            else
            {
                _angle = Random.Range(Mathf.Tan(Mathf.Deg2Rad * _minAngle), 0);
            }

            _dir = new Vector2(_angle, -1);
        }
        else if (_wall == 2)
        {

            if (transform.position.y < 0)
            {
                _angle = Random.Range(0, Mathf.Tan(Mathf.Deg2Rad * _maxAngle));
            }
            else
            {
                _angle = Random.Range(Mathf.Tan(Mathf.Deg2Rad * _minAngle), 0);
            }

            _dir = new Vector2(-1, _angle);
        }

        _dir = _dir.normalized;
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
        }
        else if (collision.tag == "Player")
        {
            StartCoroutine(SkillManager.instance.Delay_SkillActive(_type, 1));
            SkillManager.instance.ItemActiveTest();
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
