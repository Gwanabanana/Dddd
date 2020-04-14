using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Block block = collision.GetComponent<Block>();

        if (block)
        {
            block.canBeSucked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Block block = collision.GetComponent<Block>();

        if (block)
        {
            block.canBeSucked = false;
        }
    }
}
