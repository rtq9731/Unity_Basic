using UnityEngine;
using Cinemachine;

public class BallScript : MonoBehaviour
{
    [SerializeField] LayerMask whatIsTarget;

    [SerializeField] float expRadius = 4f;
    [SerializeField] float expPower = 200f;
    [SerializeField] GameObject boomEffect;

    CannonHandler cannon;
    CinemachineVirtualCamera cannonCam;
    Rigidbody2D rigid;

    private int crateLayer = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        crateLayer = LayerMask.NameToLayer("Crate");
    }

    void Update()
    {
        if( this.transform.position.y <= -10 )
        {
            Boom();
        }
    }

    public void Shoot(Vector2 dir , float power, CinemachineVirtualCamera cam, CannonHandler cannon)
    {
        Debug.Log("°ø ¹ß»ç!");
        rigid.AddForce(dir.normalized * power);
        cannonCam = cam;
        this.cannon = cannon;
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
                    if(cs != null)
                    {
                        cs.AddExplosion(transform.position, expPower);
                    }
                }
            }
            Boom();
        }
    }

    private void Boom()
    {
        Instantiate(boomEffect, this.transform.position, Quaternion.identity);
        cannonCam.gameObject.GetComponent<CameraManager>().SetDisable(1.5f);
        cannon.isFire = false;
        Destroy(this.gameObject);
    }
}
