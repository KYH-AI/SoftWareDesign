using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    /// <summary>
    /// �ִϸ��̼ǿ��� ȣ��
    /// </summary>
    private void NextSecen()
    {
        SceneManager.LoadScene("Stage1");
    }
}