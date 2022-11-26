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
    float attackTime = 2.0f, warningTime = 4.0f;
    SpriteRenderer beamRenderer;
    public Collider2D beamCollider;
    Vector3 dir;


    private new void Start()
    {
        base.Start();
        beamRenderer = beam.GetComponent<SpriteRenderer>();
        beamRenderer.sprite = square;
        state = State.Attack;
        StartCoroutine("Beam");
    }

    protected override void Attack()
    {
        state = State.Run;
    }


    IEnumerator Beam()
    {

        yield return new WaitForSeconds(base.skillTime);
        dir = transform.position - playerTarget.transform.position;
        beam.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        beam.transform.Rotate(0, 0, 90);
        beamRenderer.sprite = square;
        beamRenderer.color = new Color(255, 0, 0, 90);
        beam.SetActive(true);

        yield return new WaitForSeconds(warningTime);


        beamRenderer.color = new Color(225, 225, 225, 225);
        beamRenderer.sprite = laser;
        beamCollider.enabled = true;//АјАн
        yield return new WaitForSeconds(attackTime);
        beamCollider.enabled = false;
        beam.SetActive(false);
        StartCoroutine("Beam");
    }
}