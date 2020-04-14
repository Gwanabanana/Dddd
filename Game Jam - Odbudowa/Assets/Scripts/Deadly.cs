using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadly : MonoBehaviour
{
    [HideInInspector] public bool isDeadly = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeadly)
        {
            Player collisionPlayer = collision.GetComponent<Player>();
            if (collisionPlayer)
            {
                collisionPlayer.Kill();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDeadly)
        {
            Player collisionPlayer = collision.gameObject.GetComponent<Player>();
            if (collisionPlayer)
            {
                collisionPlayer.Kill();
            }
        }
    }
}
