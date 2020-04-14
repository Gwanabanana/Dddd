using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] blocks;
    [SerializeField] LayerMask platformLayerMask;
    [SerializeField] CursorManager cursorManager;
    [SerializeField] GameObject suckArea;
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject shootPointSelector;

    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem winParticles;

    //Movement
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float jumpVelocity = 10f;
    [SerializeField] float fallMulti = 2.5f;
    [SerializeField] float lowJumpMulti = 2f;

    //Shooting
    [SerializeField] float blockVelocity = 2f;
    [SerializeField] float shootingOffset = 1f;

    [SerializeField] int activeBlocks = 0;

    [SerializeField] float winSpeed = 2f;
    [HideInInspector] public bool canPlay = true;

    [HideInInspector] public bool canShoot = true;

    Color[] colors;
    int[] numberOfBullets;

    [HideInInspector] public Rigidbody2D myRigidBody;
    Collider2D myCollider;
    SpriteRenderer suckAreaSprite;
    SpriteRenderer shootSelector;

    //Sound
    [SerializeField] AudioManager myAudioManager;

    LevelManager myLevelManager;

    bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();

        suckAreaSprite = suckArea.GetComponent<SpriteRenderer>();
        shootSelector = shootPointSelector.GetComponent<SpriteRenderer>();

        if (!myAudioManager)
        {
            myAudioManager = FindObjectOfType<AudioManager>();
        }
        myLevelManager = FindObjectOfType<LevelManager>();

        SetupColors();
        SetupBullets();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay)
        {
            Jump();
            Move();
            ShootingPoint();
            Shoot();
            ManageBlocks();

            if (Input.GetKeyDown(KeyCode.R))
            {
                Kill();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
    }

    void SetupColors()
    {
        colors = new Color[blocks.Length];
        for(int i=0; i < blocks.Length; i++)
        {
            colors[i] = blocks[i].GetComponent<SpriteRenderer>().color;
        }
    }

    void SetupBullets()
    {
        numberOfBullets = new int[blocks.Length];
        for(int i = 0; i < blocks.Length; i++)
        {
            numberOfBullets[i] = 0;
        }
    }

    void ManageBlocks()
    {
        bool noBullets = false;
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        ChangeBlocks(scroll);

        int startingBlock = activeBlocks;
        while (numberOfBullets[activeBlocks] <= 0)
        {
            ChangeBlocks(scroll, true);
            if(activeBlocks == startingBlock)
            {
                noBullets = true;
                break;
            }
        }

        if (noBullets)
        {
            shootSelector.color = Color.black;
        }
        else
        {
            shootSelector.color = colors[activeBlocks];
        }
    }

    private void ChangeBlocks(float scroll, bool goThroughColors = false)
    {
        if (scroll > 0)
        {
            activeBlocks++;
        }
        else if (scroll < 0 || goThroughColors)
        {
            activeBlocks--;
        }

        if (activeBlocks < 0)
        {
            activeBlocks = blocks.Length - 1;
        }

        activeBlocks = activeBlocks % blocks.Length;

    }

    public void AddBlockToAmmo(Color color)
    {
        int id = GetIdFromColor(color);
        if(id >= 0)
        {
            numberOfBullets[id]++;
        }
    }

    private int GetIdFromColor(Color color)
    {
        for(int i=0; i < colors.Length; i++)
        {
            if(color == colors[i])
            {
                return i;
            }
        }
        Debug.LogError("Didn't found color in colors");
        return -1;
    }

    void Shoot()
    {
        if (canShoot && Input.GetButtonDown("Fire1") && numberOfBullets[activeBlocks] > 0)
        {
            numberOfBullets[activeBlocks]--;
            Vector2 cursorPos = cursorManager.GetCursorPosition() - new Vector2(transform.position.x, transform.position.y);
            cursorPos.Normalize();

            GameObject projectile = Instantiate(blocks[activeBlocks], shootPoint.transform.position,new Quaternion(0,0,0,0));

            if (!canShoot)
            {
                projectile.GetComponent<Block>().stickAtStart = true;
            }

            projectile.GetComponent<Rigidbody2D>().velocity = cursorPos * blockVelocity;
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        float xVelocity;

        if (x == 0)
        {
            xVelocity = 0; ;
        }
        else
        {
            xVelocity = Mathf.Sign(x) * xSpeed;
        }

        myRigidBody.velocity = new Vector2(xVelocity, myRigidBody.velocity.y);
    }

    void Jump()
    {
        float extraHeightCheck = 0.25f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size * 0.95f, 0f,
                                                    Vector2.down * Mathf.Sign(myRigidBody.gravityScale), extraHeightCheck, platformLayerMask);
        if (raycastHit.collider)
        {
            if (!canJump)
            {
                canJump = true;
                CameraShaker.Instance.ShakeOnce(1f, 2f, 0.1f, 0.5f);
                myAudioManager.PlayPlayerLandingSound(); 
            }

            if (Input.GetButtonDown("Jump"))
            {
                myRigidBody.velocity = Vector2.up * jumpVelocity * Mathf.Sign(myRigidBody.gravityScale);
                myAudioManager.PlayPlayerJumpSound();
            }

            if (myRigidBody.velocity.y < 0)
            {
                myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMulti - 1) * Time.deltaTime;
            }
            else if (myRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
            }
        }
        else
        {
            canJump = false;
        }
    }

    void ShootingPoint()
    {
        Vector2 cursorPos = cursorManager.GetCursorPosition() - new Vector2(transform.position.x, transform.position.y);
        cursorPos.Normalize();

        shootPoint.transform.position = new Vector2(transform.position.x, transform.position.y) + cursorPos * shootingOffset;
    }

    public void Kill()
    {
        ParticleSystem particles = Instantiate(deathParticles, transform.position, new Quaternion(0, 0, 0, 0));
        particles.gravityModifier *= Mathf.Sign(myRigidBody.gravityScale);
        CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 3f);

        myAudioManager.PlayPlayerDeathSound();

        Destroy(this.gameObject);

        myLevelManager.RestartLevel();
    }

    public IEnumerator Win(Vector3 target)
    {
        canPlay = false;
        myRigidBody.gravityScale = 0f;
        myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        Vector3 moveVector = target - transform.position;
        while (moveVector.magnitude > 0.1f)
        {
            transform.position += moveVector * winSpeed * Time.deltaTime;
            moveVector = target - transform.position;

            yield return null;
        }

        myAudioManager.PlayLevelWinSound();

        myLevelManager.LoadNextLevel(winParticles, transform.position, Mathf.Sign(myRigidBody.gravityScale));

        Destroy(this.gameObject);
    }
}
