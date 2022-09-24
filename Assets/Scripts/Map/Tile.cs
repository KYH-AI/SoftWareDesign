using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject Tilepref;

    int Unitsize = 10;
    int Tile_x = 0;
    int Tile_y = 0;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        if (transform.position.x > Tile_x + Unitsize)
        {
            Tile_x += Unitsize * 2;
            Tilepref.transform.position = new Vector2(Tile_x, Tile_y);
            print("x : " + Tile_x);
        }
        if (transform.position.x < Tile_x - Unitsize)
        {
            Tile_x -= Unitsize * 2;
            Tilepref.transform.position = new Vector2(Tile_x, Tile_y);
            print("x : " + Tile_x);
        }
        if (transform.position.y > Tile_y + Unitsize)
        {
            Tile_y += Unitsize * 2;
            Tilepref.transform.position = new Vector2(Tile_x, Tile_y);
            print("y : " + Tile_y);
        }
        if (transform.position.y < Tile_y - Unitsize)
        {
            Tile_y -= Unitsize * 2;
            Tilepref.transform.position = new Vector2(Tile_x, Tile_y);
            print("y : " + Tile_y);
        }
    }
}
