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

    private void Start()
    {
        Debug.Log("박스 등장!");
        CannonHandler.boxCount++;
    }

    void Update()
    {
        if (transform.position.x < -6f)
        AddExplosion(Vector3.zero, 10);
    }

    public void AddExplosion(Vector3 pos, float power)
    {
        Vector3 dir = transform.position - pos;
        power *= 1 / dir.magnitude;
        //rigid.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        GameObject broken = Instantiate(brokenPrefab, transform.position, transform.rotation);

        BrokenCrate bs = broken.GetComponent<BrokenCrate>();
        bs.AddExplosion(dir.normalized, power);
        
        CannonHandler.boxCount--;
        Destroy(broken, 2);
        Destroy(this.gameObject);
    }
}
