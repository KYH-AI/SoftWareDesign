using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIle2 : MonoBehaviour
{
    public GameObject Player;

    int Unitsize = 16;
    int Tile_x = 0;
    int Tile_y = 0;

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x > Tile_x + Unitsize)
        {
            Tile_x += Unitsize * 2;
            transform.position = new Vector2(Tile_x, Tile_y);
            print("x : " + Tile_x);
        }
        if (Player.transform.position.x < Tile_x - Unitsize)
        {
            Tile_x -= Unitsize * 2;
            transform.position = new Vector2(Tile_x, Tile_y);
            print("x : " + Tile_x);
        }
        if (Player.transform.position.y > Tile_y + Unitsize)
        {
            Tile_y += Unitsize * 2;
            transform.position = new Vector2(Tile_x, Tile_y);
            print("y : " + Tile_y);
        }
        if (Player.transform.position.y < Tile_y - Unitsize)
        {
            Tile_y -= Unitsize * 2;
            transform.position = new Vector2(Tile_x, Tile_y);
            print("y : " + Tile_y);
        }
    }
}
