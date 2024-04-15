using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvicible = 2f;
    public InputAction MoveAction;
    Rigidbody2D rigidbody2D;
    Vector2 move;
    int currentHealth;
    bool isInvincible;
    float damageCooldown;

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);

        if (isInvincible) {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0) {
                isInvincible = false;
                damageCooldown = 0;
            }
        }
    }

    void FixedUpdate() {
        Vector2 position = (Vector2) rigidbody2D.position + move * speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        if (amount == 0 || isInvincible) return;
        isInvincible = true;
        damageCooldown = timeInvicible;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.Instance.SetHealthValue(currentHealth/(float)maxHealth);
        Debug.Log(string.Format("Current Health: {0} / {1}", currentHealth, maxHealth));
    }

    public Boolean IsFullHealth() {
        return currentHealth == maxHealth;
    }

    public int Health {get {return currentHealth;}}
}
