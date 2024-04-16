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
    public InputAction launchAction;

    Rigidbody2D rigidbody2D;
    Vector2 move;
    int currentHealth;
    bool isInvincible;
    float damageCooldown;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        launchAction.Enable();
        launchAction.performed += Launch;
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        //Debug.Log(string.Format("Magnitude: {0}", moveDirection.magnitude));
        animator.SetFloat("Speed", move.magnitude);

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
        animator.SetTrigger("Hit");
        isInvincible = true;
        damageCooldown = timeInvicible;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.Instance.SetHealthValue(currentHealth/(float)maxHealth);
        Debug.Log(string.Format("Current Health: {0} / {1}", currentHealth, maxHealth));
    }

    public Boolean IsFullHealth() {
        return currentHealth == maxHealth;
    }

    void Launch(InputAction.CallbackContext context) {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * .5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }

    public int Health {get {return currentHealth;}}
}
