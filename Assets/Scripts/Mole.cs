using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mole : MonoBehaviour
{

    [SerializeField]
    private GameObject model;
    [SerializeField]
    private int HP = 2;
    [SerializeField]
    private int reward = 2;
    [SerializeField]
    private int damage = 2;
    [SerializeField]
    private float timeLeave = 5f;

    [SerializeField]
    private ParticleSystem deathParticle;
    [SerializeField]
    private ParticleSystem leaveParticle;


    void Start()
    {
        BoxCollider boxCollider = this.gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        boxCollider.size = new Vector3(1f, 2f, 1f);
        Instantiate(model, transform.position, Quaternion.identity, transform);
        StartCoroutine(LeaveCoroutine());
    }

    IEnumerator LeaveCoroutine()
    {
        yield return new WaitForSeconds(timeLeave);
        leaveParticle.Play();
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.TakeDamage(damage);
        transform.parent.GetComponent<SpawnPoint>().MoleLose();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        DamageMole();
    }

    private void DamageMole()
    {
        HP--;
        if (HP <= 0)
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        GameManager.instance.AddScore(reward);
        StopCoroutine(LeaveCoroutine());
        deathParticle.Play();
        yield return new WaitForSeconds(0.5f);
        transform.parent.GetComponent<SpawnPoint>().MoleLose();
        Destroy(gameObject);

    }

}
