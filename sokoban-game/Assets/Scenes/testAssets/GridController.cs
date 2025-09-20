using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    public Tile block;
    public Tile wall;
    public Tile background;

    public static GridController instance;
    private Grid grid;
    private Tilemap tilemap;
    private Tilemap wallTilemap;

    public List<int[]> tileMapHistory; // need to remove block from where it was and put it where it needs to be

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
        wallTilemap = transform.Find("walls").GetComponent<Tilemap>();

       tileMapHistory = new List<int[]>();
    }

    public Vector3 GetWorldPos(int x, int y)
    {
        return grid.CellToWorld(new Vector3Int(x, y, 0)); // getting the pos
    }

    // Update is called once per frame
    void Update()
    {
        // pushing block; block against wall / other block
    }

    public bool IsOccupied(int x, int y)
    {
        var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
        var tile2 = wallTilemap.GetTile(new Vector3Int(x, y, 0));
        return tile == block || tile2 == wall;
    }

    public void PushBlock(Vector3Int start, Vector3Int destination) // deleting and respawning— not actually moving
    {
        // set start position to the empty (background sprite)
        tilemap.SetTile(start, background);
        // set destination sprite to be the block
        tilemap.SetTile(destination, block);
    }

    public TileBase GetTileAt(Vector3Int position)
    {
        return tilemap.GetTile(position);
    }
}

//public class Junk
//{
//    public void DoStuff()
//    {
//        GridController.instance.DoSomething();
//    }

//}
