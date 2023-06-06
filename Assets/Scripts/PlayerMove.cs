using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    public float speed;
    public float keyboardSpeed;
    Rigidbody2D rb;

    private Vector3 dir;
    float angle;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = Vector3.zero;
#if UNITY_EDITOR
        speed = keyboardSpeed;
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        KeyBoardMove();
#else
        GyroMove();
#endif
       GeneralMove();
    }

    void GeneralMove() {
        if(dir.sqrMagnitude > 1) dir.Normalize();

        dir *= Time.deltaTime;

        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);

        angle = Mathf.Atan2(dir.x * -1,dir.y) * Mathf.Rad2Deg;

        if(dir.x != 0 || dir.y != 0) transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    void GyroMove(){
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
    }

    void KeyBoardMove(){
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            ResultManager.instance.GameOver();
        }
    }
}
