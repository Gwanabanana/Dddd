using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlock : MonoBehaviour
{
    AudioManager myAudioManager;
    private void Start()
    {
        myAudioManager = FindObjectOfType<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Grav(collision);
    }


    void Grav(Collision2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (gameObject.layer == LayerMask.NameToLayer("Platform") && rb)
        {

            rb.gravityScale *= -1;
            if (rb.gameObject.GetComponent<Player>())
            {
                if (rb.gravityScale < 0)
                {
                    myAudioManager.PlayBlockGravityUpSound();
                }
                else if (rb.gravityScale > 0)
                {
                    myAudioManager.PlayBlockGravityDownSound();
                }
            }
        }
    }
}
