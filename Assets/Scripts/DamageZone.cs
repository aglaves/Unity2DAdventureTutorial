using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other) {
        //Debug.Log("Object that triggered OnTriggerEnter2d: " + other);
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null && controller.Health > 0) {
            controller.ChangeHealth(-1);
        }
    }
}
