using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanIBuy : MonoBehaviour

{
    [SerializeField] RandomSkill RandomSkillSlot1; /*Image1, skillLevel1*/
    [SerializeField] RandomSkill2 RandomSkillSlot2; /*Image2, skillLevel2*/
    [SerializeField] RandomSkill3 RandomSkillSlot3;/*Image3, skillLevel3*/



    readonly string DEFAULT_SKILL_PATH = "Player_Skill/";
    private bool isActiveSkill;
    GameObject skillObject;
    public Text money;
    public Button btn;
    public Text byeBtn;
    public GameObject Yes;
    public GameObject No;
    public int item;
    public float Speed;
    public Button SelectBtn1;
    public Button SelectBtn2;
    public Button SelectBtn3;
    //int skillLevel;
    //Image selectImage;
    //public Image soldout;
  


    // GameObject  Money = new GameObject();
    //  Money.AddComponent<Text>();
    
    public void Start()
    {//Money = GameObject.Find("NowMoney");



        Yes.SetActive(false);
        No.SetActive(false);
        btn.onClick.AddListener(Answer);
        


        SelectBtn1.onClick.AddListener(Buy1);
        SelectBtn2.onClick.AddListener(Buy2);
        SelectBtn3.onClick.AddListener(Buy3);

        print(StageManager.GetInstance().Player);
    }



    void HideAnswerY() { Yes.SetActive(false); }
    void HideAnswerN() { No.SetActive(false); }

    public void Answer()
    {
       
       
        
        //skillObject.AddComponent<RayForUpgrade> ();
        if (StageManager.GetInstance().Player.PlayerGold >= item)
               { 
                StageManager.GetInstance().Player.PlayerGold -= item;
                Yes.SetActive(true);
                Invoke("HideAnswerY", Speed);
            //selectImage = soldout;


                //if (skillLevel == 0)
                
               // skillLevel++;
                    if (isActiveSkill)
                    {
                        ActiveSkill activeSkill = Instantiate(skillObject, StageManager.GetInstance().Player.transform).GetComponent<ActiveSkill>();
                        activeSkill.Init(StageManager.GetInstance().Player);
                        StageManager.GetInstance().Player.playerActiveSkills.Add(0, activeSkill);


                    }
                    else
                    {

                        print(skillObject.name);
                        print(StageManager.GetInstance().Player);
                        PassiveSkill passiveSkill = Instantiate(skillObject, StageManager.GetInstance().Player.transform).GetComponent<PassiveSkill>();
                        passiveSkill.Init(StageManager.GetInstance().Player);
                        passiveSkill.OnActive();


                    }
                

               /* else
                {
                    skillLevel++;
                    
                }*/

            money.text = StageManager.GetInstance().Player.PlayerGold.ToString();
        }


        else
        {
            No.SetActive(true);
            Invoke("HideAnswerN", Speed);
        }

    }

    void Buy1()
    {
        item = RandomSkillSlot1.product;
        //selectImage = Image1.skill;
        //skillLevel = skillLevel1.skillLevel;

        switch (RandomSkillSlot1.RandomInt)
        {
            case 1: skillObject = MakeSkillObject("ThunderSlash Skill", true);break;
            case 2: skillObject = MakeSkillObject("FlameStrike Skill", true); break;
            case 3: skillObject = MakeSkillObject("WindDash Skill", true); break;
        }

        
    }

    void Buy2()
    {
        item = RandomSkillSlot2.product;
       // selectImage = Image2.skill;
        //skillLevel = skillLevel2.skillLevel;

        switch (RandomSkillSlot2.RandomInt)
        {
            case 1: skillObject = MakeSkillObject("ThrowingKnife Skill", true);break;
            case 2: skillObject = MakeSkillObject("Barrier Skill", true); break;
            case 3: skillObject = MakeSkillObject("HourGlass Skill", false); break;
              
           
        }
    }

    void Buy3()
    {
        item = RandomSkillSlot3.product;
       // selectImage = Image3.skill;
        //skillLevel = skillLevel3.skillLevel;

        switch (RandomSkillSlot3.RandomInt)
        {
            case 1: skillObject = MakeSkillObject("Drone Skill", false); break;
            case 2: skillObject = MakeSkillObject("Coward Skill", false); break;
            case 3: skillObject = MakeSkillObject("SpellBlade Skill", false); break;
            case 4: skillObject = MakeSkillObject("FirstAid Skill", false); break;
        }
    }

    private GameObject MakeSkillObject(string skillObjectName, bool isActiveSkill)
    {
        skillObject = Managers.Resource.GetPerfabGameObject(DEFAULT_SKILL_PATH + skillObjectName);
        this.isActiveSkill = isActiveSkill;

        return skillObject;
    }



}



