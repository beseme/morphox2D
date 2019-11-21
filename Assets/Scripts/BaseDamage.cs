using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamage : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCondition health = collision.gameObject.GetComponent<PlayerCondition>();
                    health.TakeDamage(20f);
        }

    }
}
