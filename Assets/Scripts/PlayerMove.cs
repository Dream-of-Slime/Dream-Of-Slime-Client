using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;

        if(dir.sqrMagnitude > 1) dir.Normalize();

        dir *= Time.deltaTime;

        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }
}
