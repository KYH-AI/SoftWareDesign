using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Golem2Attack : BasicMonsterController
{
    public GameObject beam;
    public Sprite laser;
    public Sprite square;
    float attackTime = 3.0f, warningTime = 3.0f;
    SpriteRenderer beamRenderer;
    Vector3 dir;


    private new void Start()
    {
        base.Start();
        StartCoroutine("Beam");
        beamRenderer = beam.GetComponent<SpriteRenderer>();
        beamRenderer.sprite = square;
    }

    protected override void Attack()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }


    IEnumerator Beam()
    {

        yield return new WaitForSeconds(base.skillTime);
        dir = transform.position - playerTarget.transform.position;
        Debug.Log("방향"+dir);
        beam.transform.rotation=Quaternion.FromToRotation(Vector3.up, dir);
        Debug.Log("회전"+beam.transform.rotation);
        beam.transform.Rotate(new Vector3(0,0,90));
        beamRenderer.sprite = square;
        beamRenderer.color = new Color(255, 0, 0, 90);
        beam.SetActive(true);

        yield return new WaitForSeconds(warningTime);
        beamRenderer.color = new Color(225, 225, 225, 225);
        beamRenderer.sprite = laser;
        //공격넣기

        yield return new WaitForSeconds(attackTime);
        beam.SetActive(false);
        StartCoroutine("Beam");
    }
}