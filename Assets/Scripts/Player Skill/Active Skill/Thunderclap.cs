using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 귀멸의 칼날 벽력일섬 N연속 기술
/// </summary>
public class Thunderclap : ActiveSkill
{

    private TrailRenderer thunderclapEffect;
    [SerializeField] VolumeProfile thunderClapProfile;

    private void Start()
    {
        thunderclapEffect = GetComponentInChildren<TrailRenderer>();

        thunderclapEffect.enabled = false;
    }

    public override void OnActive()
    {
        ThuderclapSkillAttack();
    }

    private void ThuderclapSkillAttack()
    {
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(playerObject.transform.position, 10f, enemyLayer);

        if (enemyCollider.Length > 0)
        {
            PlayerCamera.Instance.ChagnePostProcessProfile(thunderClapProfile); 
            AcitveThunderClap(); // 번개 효과 이펙트 (Trail Renderer) 활성화
            Time.timeScale = 0.1f;
            StartCoroutine(ThuderclapSkillProcess(enemyCollider));
        }

    }

    IEnumerator ThuderclapSkillProcess(Collider2D[] c)
    {
        for (int i = 0; i < c.Length; i++)
        {
            GameManager.Instance.PlayerCameraMoveSpeed = 0f;
            playerObject.transform.position = c[i].transform.position;

            Destroy(c[i].gameObject, 0.5f);

            yield return new WaitForSecondsRealtime(0.2f);
            GameManager.Instance.PlayerCameraMoveSpeed = 10f;

            yield return new WaitForSecondsRealtime(1f);
        }
        PlayerCamera.Instance.ChagnePostProcessProfile(null);
        Time.timeScale = 1.0f;
 
        Invoke(nameof(AcitveThunderClap), 1f); // 번개 효과 이펙트 (Trail Renderer) 비활성화

    }

    private void AcitveThunderClap()
    {
        thunderclapEffect.enabled = !thunderclapEffect.enabled;
    }
}
