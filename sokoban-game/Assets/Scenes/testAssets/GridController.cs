using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    // reset variables 
    public Tile block; // block 1
    public Tile skyBlue; // block 2
    public Tile tealBlue; // block 3
    public Tile blockGoal;
    public Tile skyGoal;
    public Tile tealGoal;
    //public Tile wall;
    //public Tile background;
    public static GridController instance;
    private Grid grid;
    public Tilemap tilemap;
    private Tilemap goalTilemap;
    private Tilemap wallTilemap;
    public List<int[]> tileMapHistory; // need to remove block from where it was and put it where it needs to be
    public HashSet<Tile> blockTypes;
    public List<Vector3Int> goalPos = new List<Vector3Int>();
    public Dictionary<Tile, Tile> goalToBlock;
    public Vector3Int blockStart = new Vector3Int();
    public Vector3Int skyBlueStart = new Vector3Int();
    public Vector3Int tealBlueStart = new Vector3Int();
    //public Dictionary<Tile, Vector3Int> tileStartPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance != null) // if there is an instance in the scene already, destroy ourself
        {
            Destroy(this);
            return;
        }

        instance = this; // set ourselves to singleton instance
        grid = GetComponent<Grid>();
        tilemap = transform.Find("blocks").GetComponent<Tilemap>();
        goalTilemap = transform.Find("goals").GetComponent<Tilemap>();
        wallTilemap = transform.Find("walls").GetComponent<Tilemap>();

        tileMapHistory = new List<int[]>();

        blockTypes = new HashSet<Tile>
        {
            block,
            skyBlue,
            tealBlue // adding
        };

        goalToBlock = new Dictionary<Tile, Tile>
        {
            {blockGoal, block}, // receive corresponding block tile type
            {skyGoal, skyBlue},
            {tealGoal, tealBlue}
        };

        // tileStartPos = new Dictionary<Tile, Vector3Int>
        // {
        //     {block, blockStart},
        //     {skyBlue,skyBlueStart},
        //     {tealBlue, tealBlueStart}
        // };
    }
    public Vector3 GetWorldPos(int x, int y)
    {
        return grid.CellToWorld(new Vector3Int(x, y, 0)); // getting the pos
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsOccupied(int x, int y)
    {
        var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
        var tile2 = wallTilemap.GetTile(new Vector3Int(x, y, 0));
        return tile != null || tile2 != null;
    }

    public void PushBlock(Vector3Int start, Vector3Int destination) // deleting and respawning— not actually moving
    {
        Tile targetTile = GetTileAt(start) as Tile;
        // set start position to the empty (background sprite)
        tilemap.SetTile(start, null);
        // set destination sprite to be the block
        tilemap.SetTile(destination, targetTile);
    }

    public TileBase GetTileAt(Vector3Int position)
    {
        return tilemap.GetTile(position);
    }

    public TileBase GetGoalAt(Vector3Int position)
    {
        return goalTilemap.GetTile(position);
    }

    public void Reset()
    {
        
    }
}
