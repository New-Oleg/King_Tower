using UnityEngine;

public struct CollisionComponent
{
    [Tooltip("не выбирать вместе с IsWeapon")]
    public bool IsUnit;

    [Tooltip("не выбирать вместе с IsUnit")]
    public bool IsWeapon;

    public bool IsGrounded;
    public bool IsCanUp;
}
