using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    Player myPlayer;
    SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponentInParent<Player>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        myPlayer.canShoot = false;
        mySprite.color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        myPlayer.canShoot = true;
        mySprite.color = Color.white;
    }
}
