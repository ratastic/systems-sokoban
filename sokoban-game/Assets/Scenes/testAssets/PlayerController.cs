using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private int x = 0;
    private int y = 0;
    public List<Vector3Int> playerHistory = new List<Vector3Int>();
    public List<bool> blockHistory = new List<bool>(); // checks whether or not block is being moved hence bool

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

        // undo
        if (Input.GetKeyDown("z"))
        {
            if (playerHistory.Count > 0)
            {
                Debug.Log("im being pressed");
                UndoPlayerMove();

                if (blockHistory[blockHistory.Count - 1])
                {
                    UndoBlockMove();
                }
                blockHistory.RemoveAt(blockHistory.Count - 1);
            }
        }

        if (Input.GetKeyDown("r"))
        {
            RestartLevel();
        }

        //Debug.Log("x:" + x + "y:" + y);
        TileBase tileToGet = GridController.instance.GetTileAt(new Vector3Int(-4, -1, 0));
        //Debug.Log("tile:" + (tileToGet == null));
    }

    private void UndoPlayerMove()
    {
        Debug.Log("player move undone");
        Vector3Int playerPos = playerHistory[playerHistory.Count - 1];
        playerHistory.RemoveAt(playerHistory.Count - 1);
        transform.position = GridController.instance.GetWorldPos(playerPos.x, playerPos.y);

        x = playerPos.x;
        y = playerPos.y;
    }

    private void UndoBlockMove()
    {
        Debug.Log("block move undone");
        int[] blockMoveInfo = GridController.instance.tileMapHistory[GridController.instance.tileMapHistory.Count - 1];
        GridController.instance.tileMapHistory.RemoveAt(GridController.instance.tileMapHistory.Count - 1);
        // grabbing block x y and target x y
        GridController.instance.PushBlock(new Vector3Int(blockMoveInfo[0], blockMoveInfo[1], 0), new Vector3Int(blockMoveInfo[2], blockMoveInfo[3], 0));
    }

    private bool CheckWin()
    {
        foreach (Vector3Int goal in GridController.instance.goalPos)
        {
            Tile goalTile = GridController.instance.GetGoalAt(goal) as Tile;
            Tile blockTile = GridController.instance.GetTileAt(goal) as Tile;

            if (blockTile == null || blockTile != GridController.instance.goalToBlock.GetValueOrDefault(goalTile, null))
            {
                return false;
            }
        }
        return true;
    }

    private void RestartLevel()
    {
        // reset player
        x = 0;
        y = 0;
        Move(0, 0);
        playerHistory.Clear();

        // reset blocks
        blockHistory.Clear();
        GridController.instance.tileMapHistory.Clear();
    }

    private void Move(int xMove, int yMove)
    {
        // target spot is the x position plus move amount
        int targetX = x + xMove;
        int targetY = y + yMove;

        // access the target spot
        Vector3Int targetPos = new Vector3Int(targetX, targetY, 0); // gets new position
        Tile targetTile = GridController.instance.GetTileAt(targetPos) as Tile; // gets the tile at that new position

        if (GridController.instance.blockTypes.Contains(targetTile)) // handles the case when the target tile has a block
        {
            // defines that block position
            int blockX = targetX + xMove;
            int blockY = targetY + yMove;
            // gets the new block position
            Vector3Int blockPos = new Vector3Int(blockX, blockY, 0);

            // if nothing is in front of the block
            if (!GridController.instance.IsOccupied(blockX, blockY))
            {
                blockHistory.Add(true); // player moves and a block
                playerHistory.Add(new Vector3Int(x, y, 0)); // adds player pos

                // stores before and after position for later
                GridController.instance.tileMapHistory.Add(new int[4] { blockX, blockY, targetX, targetY });

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
            blockHistory.Add(false); // player moves but not a block
            playerHistory.Add(new Vector3Int(x, y, 0));

            // move player to next tile
            x = targetX;
            y = targetY;
        }

        transform.position = GridController.instance.GetWorldPos(x, y);

        if (CheckWin())
        {
            Debug.Log("YOU WON");
        }
    }
}

