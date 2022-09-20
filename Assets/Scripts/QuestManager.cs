using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questid;
    Dictionary<int, QuestData> questList;
    // Start is called before the first frame update
    void Start()
    {
        questList = new Dictionary <int, QuestData>();
        GenarateData();
    }

    // Update is called once per frame
    void GenarateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문",new int[] {1}));
    }
}
