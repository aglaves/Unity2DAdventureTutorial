using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Object that triggered OnTriggerEnter2d: " + other);
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null && !controller.IsFullHealth()) {
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}
