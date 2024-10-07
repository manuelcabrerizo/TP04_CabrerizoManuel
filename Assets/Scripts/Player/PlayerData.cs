using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data", order = 1)]

public class PlayerData : ScriptableObject
{
    [Header("Transform")]
    public Vector2 InitialPosition;
   
    [Header("Speed")]
    public float InitialSpeed = 3;
    
    [Header("Jump")]
    public float JumpForce = 8;

    [Header("Rotation")]
    public float RotationForce = 4;

    [Header("Sounds")]
    public AudioClip JumpClip;
    public AudioClip LandClip;
    public AudioClip FallClip;
    public AudioClip TrickClip;
}
