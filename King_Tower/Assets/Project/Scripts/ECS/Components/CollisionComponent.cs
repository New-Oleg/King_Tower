using UnityEngine;

public struct CollisionComponent
{
    [Tooltip("�� �������� ������ � IsWeapon")]
    public bool IsUnit;

    [Tooltip("�� �������� ������ � IsUnit")]
    public bool IsWeapon;

    public bool IsGrounded;
    public bool IsCanUp;
}
