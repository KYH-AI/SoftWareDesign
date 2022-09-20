using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class FirePillar : ActiveSkill
{
    [SerializeField] GameObject firePillarObject;
    [SerializeField] VolumeProfile firePillarProfile;

    public override void OnActive()
    {
        FirePillarSkillAttack();
    }

    private void FirePillarSkillAttack()
    {
        StartCoroutine(FirePillarSkillProcess());
    }

    Vector2 SpawnLocation (float xPos = 0f, float yPos = 0f)
    {
        return new Vector2(transform.position.x + xPos, transform.position.y + yPos);
    }

    IEnumerator FirePillarSkillProcess()
    {

        float[,] locationCount = new float[4, 2]
        {
            {0f, 2f},
            {4f, 0f},
            {0f, -4f},
            {-4f, 0f},
         };

        PlayerCamera.Instance.ChagnePostProcessProfile(firePillarProfile);

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(SpawnLocation(locationCount[i, 0], locationCount[i, 1]));
            Instantiate(firePillarObject, SpawnLocation(locationCount[i,0], locationCount[i,1]), Quaternion.identity);
            yield return new WaitForSeconds(0.5f); // 발사 간격
        }

        PlayerCamera.Instance.ChagnePostProcessProfile(null);
    }


    private void SpawnFirePillarObject(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector2.right;
        Vector2 spawnPosition = playerObject.transform.position + direction * 5;
        Debug.Log(direction);
        Debug.Log(spawnPosition);
        Instantiate(firePillarObject, spawnPosition, Quaternion.identity);
    }
}
