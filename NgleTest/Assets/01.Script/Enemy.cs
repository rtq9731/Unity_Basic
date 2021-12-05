using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Attackable
{
    [SerializeField] ParticleSystem hitParticle = null;
    [SerializeField] GameObject hitPrefab = null;
    [SerializeField] float hitDistance = 3f;

    bool bCanAttack;

    PlayerMove player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    private IEnumerator enemyDie(HitEffector hitEffector)
    {
        ParticleSystem particle = Instantiate(hitParticle, this.transform);
        particle.transform.position = this.transform.position;
        particle.Play();
        hitEffector.HitEffect(transform.position);
        Instantiate(hitPrefab, transform);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public override void GetAttack(HitEffector hitEffector)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(enemyDie(hitEffector));
    }
}
