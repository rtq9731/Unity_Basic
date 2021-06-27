using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    ParticleSystem particle;
    ParticleSystemRenderer pr;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        pr = GetComponent<ParticleSystemRenderer>();
    }

    public void SetParticleColor(Color color)
    {
        pr.material.color = color;
    }

    public void Play(Vector3 pos)
    {
        transform.position = pos;
        particle.Play();
        Invoke("Disable", 2f);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetRotation(Vector2 normal)
    {
        transform.rotation = Quaternion.LookRotation(normal);
    }

}
