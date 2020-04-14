using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBlock : MonoBehaviour
{
    [SerializeField] float jumpVelocity = 20f;
    AudioManager myAudioManager;

    private void Start()
    {
        myAudioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Platform") && collision.rigidbody)
        {
            collision.rigidbody.velocity = Mathf.Sign(collision.rigidbody.gravityScale) * Vector2.up * jumpVelocity;

            if (collision.rigidbody.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                myAudioManager.PlayPlayerJumpSound();
            }
        }
    }
}
