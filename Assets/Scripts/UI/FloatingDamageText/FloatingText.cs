using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private void OnDisable()
    {
        MemoryPoolManager.GetInstance().InputGameObject(this.gameObject); ;
    }
}