using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject Player;

    int Unitsize = 10;
    int Tile_x = 0;
    int Tile_y = 0;

    


    void Start()
    {
        
    }

   
    void Update()
    {
        switch (StageManager.GetInstance().stage)
        {
            case Define.Stage.TWO:
                Unitsize = 16;
                break;
            case Define.Stage.THREE:
                Unitsize = 30;
                break;
            case Define.Stage.FOUR:
                Unitsize = 20;
                break;
        }

        MoveTile();
    }
    void MoveTile()
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
