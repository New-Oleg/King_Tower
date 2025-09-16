using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAsset", menuName = "Data/Player")]
public class Player : ScriptableObject
{
    public GameObject PlayePref;
    public float PlayerSpeed;
    public float PlayerJumpForce;
}
