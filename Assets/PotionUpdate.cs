using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUpdate : MonoBehaviour
{
    AudioSource audioSoure;
    public Button btn;
    public Text hp;
    public Text potion;
    public Text money; 

    // Start is called before the first frame update
    void Start()
    {
        audioSoure = GetComponent<AudioSource>();
        btn.onClick.AddListener(Potion);
       
    }

    // Update is called once per frame
    public void Potion()
    {
       
        if (Managers.StageManager.Player.PlayerGold >= 150)
            
        {
            audioSoure.Play();

            Managers.StageManager.Player.PlayerGold -= 150;
           
            Managers.StageManager.Player.MaxHp += 100;
            potion.text = "체력 100회복!";
          
        }
        else { potion.text = "구매 불가! "; }


        
        hp.text = Managers.StageManager.Player.MaxHp.ToString();
        money.text = Managers.StageManager.Player.PlayerGold.ToString();
    }
}
