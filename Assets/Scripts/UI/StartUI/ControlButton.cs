using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlButton : MonoBehaviour
{
   
       
        
    public void ChangeScene()
    {
        SceneManager.LoadScene("Stage1");
    }

      public void Quitbtn()
      {
        Application.Quit();
      }
   
}
