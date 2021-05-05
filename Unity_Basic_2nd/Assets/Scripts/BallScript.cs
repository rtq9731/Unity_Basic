using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] LayerMask whatIsTarget;

    [SerializeField] float expRadius = 4f;
    [SerializeField] float expPower = 200f;

    Rigidbody2D rigid;

    private int crateLayer = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        crateLayer = LayerMask.NameToLayer("Crate");
    }

    public void Shoot(Vector2 dir , float power)
    {
        rigid.AddForce(dir.normalized * power);
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        int layer = collisionInfo.gameObject.layer;
        if ( ((1 << layer) & whatIsTarget) > 0)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, expRadius, 1 << crateLayer);
            if (cols.Length >= 1)
            {
                foreach(Collider2D item in cols)
                {
                    CrateScript cs = item.gameObject.GetComponent<CrateScript>();
                    Debug.Log("¤¾¤·");
                    if(cs != null)
                    {
                        cs.AddExplosion(transform.position, expPower);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
