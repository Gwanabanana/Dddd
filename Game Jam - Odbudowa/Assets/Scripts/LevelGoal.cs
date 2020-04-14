using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    bool alreadyWon = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player pl = collision.GetComponent<Player>();

        if (pl && !alreadyWon)
        {
            alreadyWon = true;
            StartCoroutine(pl.Win(this.transform.position));
        }
    }
}
