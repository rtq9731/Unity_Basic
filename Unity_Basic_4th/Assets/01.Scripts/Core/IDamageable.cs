using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// damage만큼의 데미지를 입혀줍니다.
    /// </summary>
    /// <param name="damage">데미지</param>
    /// <param name="hitPoint">피격부위</param>
    /// <param name="normal">피격 받은 곳의 노말벡터</param>
    void OnDamage(int damage, Vector2 hitPoint, Vector2 normal);
}
