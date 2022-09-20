using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private float playerCameraMoveSpeed = 3.0f;
    public float PlayerCameraMoveSpeed { get { return playerCameraMoveSpeed; } set { playerCameraMoveSpeed = value; } }

    private void Awake()
    {
        instance = this;
    }


}
