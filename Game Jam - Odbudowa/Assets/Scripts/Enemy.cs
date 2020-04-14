using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;
    Player player;
    Collider2D myCollider;
    Rigidbody2D myRigidBody;
    Deadly myDeadly;
    Follower myFollower;

    //Block
    [HideInInspector] public bool isHit;
    Block myBlock;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        myCollider = GetComponent<Collider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myDeadly = GetComponent<Deadly>();
        myFollower = GetComponent<Follower>();
    }

    private void Update()
    {
        HandleBlock();
    }

    void HandleBlock()
    {
        if (myBlock && !myBlock.isSucked)
        {
            isActive = false;

            //playerSeen = false;
            myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

            if (myDeadly)
            {
                myDeadly.isDeadly = false;
            }
        }
        else
        {
            isActive = true;
            isHit = false;
            if (myDeadly)
            {
                myDeadly.isDeadly = true;
            }
        }
    }

    public void GetHit(Block block)
    {
        CameraShaker.Instance.ShakeOnce(1f, 2f, 0.1f, 0.2f);

        isHit = true;
        myBlock = block;
        myFollower.startPos = transform.position;
    }



}
