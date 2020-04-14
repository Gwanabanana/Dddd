using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    SpriteRenderer indicatorRenderer;
    float indicatorColorVisibility = 1f;
    [SerializeField] float followingSpeed = 2f;

    bool changeColor = false;

    bool playerSeen = false;
    Vector2 playerPos;
    Vector2 myPos;

    Player player;
    Collider2D myCollider;
    Rigidbody2D myRigidBody;
    Enemy enemy;

    AudioManager myAudioManager;

    public Vector2 startPos;
    Vector2 attackPos;
    bool attacked = true;

    enum TypeOfEnemy { follower, bumper};
    [SerializeField] TypeOfEnemy typeOfEnemy = TypeOfEnemy.follower;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();
        myCollider = GetComponent<Collider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAudioManager = FindObjectOfType<AudioManager>();

        if (indicator)
        {
            indicatorRenderer = indicator.GetComponent<SpriteRenderer>();
        }

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isActive && player)
        {
            myPos = new Vector2(transform.position.x, transform.position.y);

            SeePlayer();
            if(typeOfEnemy == TypeOfEnemy.follower)
            {
                FollowPlayer();
            }else if(typeOfEnemy == TypeOfEnemy.bumper)
            {
                if (player.canPlay)
                {
                    AttackPlayer();
                }
                else
                {
                    GoBack();
                }
            }
        }
        else
        {
            if (indicatorRenderer)
            {
                changeColor = false;
            }
        }

        ChangeIndicatorColor(changeColor);

        if (!player)
        {
            myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }


    void FollowPlayer()
    {
        if (playerSeen)
        {
            transform.up = -(playerPos - myPos);

            if ((playerPos - myPos).magnitude < 0.5 || !player.canPlay)
            {
                playerSeen = false;
                myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            Vector3 moveVector = (playerPos - myPos).normalized;
            myRigidBody.velocity = moveVector * followingSpeed;
        }
    }
    void AttackPlayer()
    {
        if (!attacked)
        {
            Vector3 moveVector = (attackPos - myPos).normalized;
            myRigidBody.velocity = moveVector * followingSpeed;

            if ((attackPos - myPos).magnitude < 0.5)
            {
                if (typeOfEnemy == TypeOfEnemy.bumper)
                {
                    myAudioManager.PlayEnemySonarSound();
                }
                attacked = true;
            }
        }
        else if (attacked)
        {
            GoBack();
        }
    }

    private void GoBack()
    {
        if ((myPos - startPos).magnitude < 0.5)
        {

            if (playerSeen && player.canPlay)
            {
                if (typeOfEnemy == TypeOfEnemy.bumper)
                {
                    myAudioManager.PlayEnemySonarSound();
                }
                attacked = false;
                attackPos = playerPos;
            }
            else
            {
                myRigidBody.velocity = new Vector2();
            }
        }
        else
        {
            Vector3 moveVector = (startPos - myPos).normalized;
            myRigidBody.velocity = moveVector * followingSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(typeOfEnemy == TypeOfEnemy.bumper && collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            attacked = true;
            if (typeOfEnemy == TypeOfEnemy.bumper)
            {
                myAudioManager.PlayEnemySonarSound();
            }
        }
    }

    void SeePlayer()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(myCollider.bounds.center, player.transform.position - myCollider.bounds.center,
                                                    100, ~LayerMask.NameToLayer("Enemy"));
        if (raycastHit)
        {
            //Debug.Log(raycastHit.transform.name);
        }
        if (raycastHit.collider.gameObject.layer != LayerMask.NameToLayer("Platform"))
        {
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            playerSeen = true;
            myRigidBody.constraints = RigidbodyConstraints2D.None;

            if (indicatorRenderer)
            {
                changeColor = true;
            }
            //Debug.DrawLine(myCollider.bounds.center, raycastHit.point, Color.green);
        }
        else
        {
            if(typeOfEnemy == TypeOfEnemy.bumper)
            {
                playerSeen = false;
            }

            if (indicatorRenderer)
            {
                changeColor = false;
            }
            //Debug.DrawLine(myCollider.bounds.center, raycastHit.point, Color.red);
        }
    }

    void ChangeIndicatorColor(bool on)
    {
        if (indicatorRenderer)
        {
            if (on && indicatorColorVisibility >= 0)
            {
                indicatorColorVisibility -= 0.01f;
            }
            else if (!on && indicatorColorVisibility <= 1)
            {
                indicatorColorVisibility += 0.01f;
            }

            Color c = indicatorRenderer.color;
            c.a = indicatorColorVisibility;
            indicatorRenderer.color = c;
        }
    }

}
