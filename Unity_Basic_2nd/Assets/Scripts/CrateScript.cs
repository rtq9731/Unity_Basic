using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    [SerializeField] GameObject brokenPrefab;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void AddExplosion(Vector3 pos, float power)
    {
        Vector3 dir = transform.position - pos;
        power *= 1 / dir.magnitude;
        //rigid.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        GameObject broken = Instantiate(brokenPrefab, transform.position, transform.rotation);

        BrokenCrate bs = broken.GetComponent<BrokenCrate>();
        bs.AddExplosion(dir.normalized, power);

        Destroy(this.gameObject);
        Destroy(broken, 2);
    }
}
