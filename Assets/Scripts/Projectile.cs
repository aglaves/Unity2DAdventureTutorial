using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (transform.position.magnitude > 100.0f)
            Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force) {
        rigidbody2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        EnemyController enemy = other.collider.GetComponent<EnemyController>();
        if (enemy != null)
            enemy.Fix();
        Destroy(gameObject);
    }
}
