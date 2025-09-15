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
    }

    private void Move(int xMove, int yMove)
    {
        int targetX = x + xMove;
        int targetY = y + yMove;

        Vector3Int targetPos = new Vector3Int(targetX, targetY, 0); // gets new position
        TileBase targetTile = GridController.instance.GetTileAt(targetPos); // gets the tile at that new position

        if (targetTile == GridController.instance.block) // if the target tile has a block
        {
            int byBlockX = targetX + xMove;
            int byBlockY = targetY + yMove;
            Vector3Int byBlockPos = new Vector3Int(byBlockX, byBlockY, 0);

            if (!GridController.instance.IsOccupied(byBlockX, byBlockY))
            {
                // block can be pushed
                GridController.instance.PushBlock(targetPos, byBlockPos);

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

