using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public bool vertical = false;
    public float switchDirectionTime = 3.0f;
    private float switchDirectionTimer = 0f;
    private float direction = 1f;
    Rigidbody2D rigidbody2D;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        switchDirectionTimer = switchDirectionTime;
        animator = GetComponent<Animator>();
    }

    void Update() {
        switchDirectionTimer -= Time.deltaTime;

        if (switchDirectionTimer < 0) {
            direction *= -1;
            switchDirectionTimer = switchDirectionTime;
        }
    }

    void FixedUpdate() {
        Vector2 position = rigidbody2D.position;
        if (vertical) {
            position.y = position.y + (speed * direction) * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        } else {
            position.x = position.x + (speed * direction) * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null) {
            player.ChangeHealth(-1);
        }
    }
}
