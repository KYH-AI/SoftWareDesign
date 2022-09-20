using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc) //구조체 생성 for 매개변수 생성자 
    {
        questName = name;
        npcId = npc;
    }
    }



