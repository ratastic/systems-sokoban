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
        int targetX = x + xMove;
        int targetY = y + yMove;

        Vector3Int targetPos = new Vector3Int(targetX, targetY, 0); // gets new position
        TileBase targetTile = GridController.instance.GetTileAt(targetPos); // gets the tile at that new position

        if (targetTile == GridController.instance.block) // if the target tile has a block
        {
            int blockX = targetX + xMove;
            int blockY = targetY + yMove;
            Vector3Int blockPos = new Vector3Int(blockX, blockY, 0);

            if (!GridController.instance.IsOccupied(blockX, blockY))
            {
                // block can be pushed
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
        else if (!GridController.instance.IsOccupied(targetX, targetY))
        {
            x = targetX;
            y = targetY;
        }
        else
        {
            return;
        }
        transform.position = GridController.instance.GetWorldPos(x, y);
    }
}

