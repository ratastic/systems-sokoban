using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private int x = 0;
    private int y = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = 0;
        y = 0;
        Move(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // player movement
        if (Input.GetKeyDown("w"))
        {
            Move(0, 1);
        }

        if (Input.GetKeyDown("a"))
        {
            Move(-1, 0);
        }

        if (Input.GetKeyDown("s"))
        {
            Move(0, -1);
        }

        if (Input.GetKeyDown("d"))
        {
            Move(1, 0);
        }

        Debug.Log("x:" + x + "y:" + y);
        TileBase tileToGet = GridController.instance.GetTileAt(new Vector3Int(-4, -1, 0));
        Debug.Log("tile:" + (tileToGet == null));
    }

    private void Move(int xMove, int yMove)
    {
        // target spot is the x position plus move amount
        int targetX = x + xMove;
        int targetY = y + yMove;

        // access the target spot
        Vector3Int targetPos = new Vector3Int(targetX, targetY, 0); // gets new position
        TileBase targetTile = GridController.instance.GetTileAt(targetPos); // gets the tile at that new position

        if (targetTile == GridController.instance.block) // handles the case when the target tile has a block
        {
            // defines that block position
            int blockX = targetX + xMove; 
            int blockY = targetY + yMove;
            // gets the new block position
            Vector3Int blockPos = new Vector3Int(blockX, blockY, 0);

            // if nothing is in front of the block
            if (!GridController.instance.IsOccupied(blockX, blockY))
            {
                // pushes the block from the player's target pos to the block's target position
                GridController.instance.PushBlock(targetPos, blockPos);

                // moves player into vacancy left by pushed block
                x = targetX;
                y = targetY;
            }
            else
            {
                // block cannot be pushed
                return;
            }
        }
        // if nothing is in front of the player
        else if (!GridController.instance.IsOccupied(targetX, targetY))
        {
            // move player to next tile
            x = targetX;
            y = targetY;
        }

        transform.position = GridController.instance.GetWorldPos(x, y);
    }
}

