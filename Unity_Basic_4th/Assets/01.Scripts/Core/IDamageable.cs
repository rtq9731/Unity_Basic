using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// damage��ŭ�� �������� �����ݴϴ�.
    /// </summary>
    /// <param name="damage">������</param>
    /// <param name="hitPoint">�ǰݺ���</param>
    /// <param name="normal">�ǰ� ���� ���� �븻����</param>
    void OnDamage(int damage, Vector2 hitPoint, Vector2 normal);
}
