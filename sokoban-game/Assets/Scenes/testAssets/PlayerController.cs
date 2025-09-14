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
        
    }

    private void Move(int xMove, int yMove)
    {
        x += xMove;
        y += yMove;
        transform.position = GridController.instance.GetWorldPos(x, y);
    }
}
