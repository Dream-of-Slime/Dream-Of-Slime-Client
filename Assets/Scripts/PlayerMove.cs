using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    private Vector3 dir;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;

        if(dir.sqrMagnitude > 1) dir.Normalize();

        dir *= Time.deltaTime;

        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);

        angle = Mathf.Atan2(dir.x * -1,dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
