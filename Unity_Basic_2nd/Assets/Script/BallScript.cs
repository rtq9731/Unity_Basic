using UnityEngine;

public class BallScript : MonoBehaviour
{

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 dir , float power)
    {
        rigid.AddForce(dir.normalized * power);
    }
}
