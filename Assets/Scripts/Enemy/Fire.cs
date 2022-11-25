using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator Animator;
    float burnDelay = 0.5f;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    IEnumerator Burning()
    {
        yield return new WaitForSeconds(burnDelay);
        Animator.SetBool("isBurn",true);
    }
    private void DisableObject()
    {
        Animator.SetBool("isBurn", false);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        MemoryPoolManager.GetInstance().InputGameObject(gameObject);
    }
}

