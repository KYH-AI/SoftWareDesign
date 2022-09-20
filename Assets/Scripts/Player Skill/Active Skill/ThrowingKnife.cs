using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 수리검 N개 투척
/// </summary>
public class ThrowingKnife : ActiveSkill
{
    [SerializeField] GameObject knifeObject;
    private int projectileCount = 5;
    
    private Coroutine knifeProcessCheck = null;
    private WaitForSeconds skillDelay = new WaitForSeconds(0.25f);

    public override void OnActive()
    {
        if(knifeProcessCheck == null)
        {
            ThrowingKnifeSkillAttack();
        }
    }

    private void ThrowingKnifeSkillAttack()
    {

      knifeProcessCheck =  StartCoroutine(ThrowingKnifeSkillProcess());
    }

    IEnumerator ThrowingKnifeSkillProcess()
    {
        for(int i = 0; i < projectileCount; i++)
        {
            CreateKnife();
            yield return skillDelay;
        }

        knifeProcessCheck = null;
    }


    private void CreateKnife()
    {

        GameObject knife = Instantiate(knifeObject, playerController.AttackDir.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
        
        knife.transform.rotation =  Quaternion.Euler(0, 0, Mathf.Atan2(-playerController.LastDir.y, -playerController.LastDir.x) * Mathf.Rad2Deg);
     //   GunMuzzel.right* Random.Range(2, 4) + Vector3.up * Random.Range(2, 4);

        knife.GetComponent<Rigidbody2D>().velocity = playerController.LastDir.normalized * 15f;
    }


    
}
