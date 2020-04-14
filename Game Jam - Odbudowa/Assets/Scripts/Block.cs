using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool stickAtStart = false;
    [SerializeField] float suckingSpeed = 2f;

    Collider2D myCollider;
    Rigidbody2D myRigidBody;
    Player player = null;
    Enemy enemy = null;

    bool isSticking = false;
    bool isStickingToEnemy = false;
    public bool canBeSucked = false;
    [HideInInspector] public bool isSucked = false;
    bool ammoAdded = false;

    [HideInInspector] public Color myColor;

    AudioManager myAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        myColor = GetComponent<SpriteRenderer>().color;

        myAudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStickingToEnemy)
        {
            Stick();
        }
        else if (!isSucked)
        {
            StickToEnemy(enemy);
        }

        if (player)
        {
            StartSucking(false);
            Suck();
        }
    }

    void Stick()
    {
        if (!isSticking)
        {
            float extraHeightCheck = 0.1f;
            RaycastHit2D raycastHit = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size * (1 + extraHeightCheck), 0f, Vector2.down, 0, 256);

            if (raycastHit || stickAtStart)
            {
                if (gameObject.layer != 9)
                {
                    this.gameObject.layer = 8;
                }
                myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

                isSticking = true;

                if (!stickAtStart)
                {
                    myAudioManager.PlayBlockStickSound();
                }

                stickAtStart = false;
            }
        }
    }

    void StartSucking(bool emergency = false)
    {
        if ((canBeSucked && Input.GetButton("Fire2")) || emergency)
        {
            if (!isSucked)
            {
                if (isStickingToEnemy && enemy)
                {
                    enemy.isHit = false;
                }

                isSucked = true;
                gameObject.layer = LayerMask.NameToLayer("Default");

                myCollider.isTrigger = true;
                myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    void Suck()
    {
        if (isSucked)
        {
            Vector3 moveVector = (player.transform.position - transform.position);
            transform.position += moveVector.normalized * suckingSpeed * Time.deltaTime;
            if(moveVector.magnitude < 0.5f)
            {
                DestroyBlock();
            }
        }
    }

    void StickToEnemy(Enemy enemy)
    {
        if (enemy && !isSucked)
        {
            transform.rotation = enemy.transform.rotation;
            transform.position = enemy.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        if (gameObject.layer == 9)
        {
            player.Kill();
        }
        else
        {
            if (!ammoAdded)
            {
                player.AddBlockToAmmo(GetComponent<SpriteRenderer>().color);
                myAudioManager.PlayBlockPickupSound();
                ammoAdded = true;

                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11) //Returns blocks to player
        {
            StartSucking(true);
        }
        else
        {
            if (!isStickingToEnemy && !isSticking)
            {
                enemy = collision.gameObject.GetComponent<Enemy>(); //Hits an enemy

                if (enemy)
                {
                    if (!enemy.isHit)
                    {
                        isStickingToEnemy = true;


                        if (gameObject.layer != 9)
                        {
                            this.gameObject.layer = 8;
                        }
                        myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

                        isSticking = true;

                        myAudioManager.PlayBlockStickSound();

                        enemy.GetHit(this);
                    }
                }
            }
        }
        
    }
}
