using UnityEngine;

[CreateAssetMenu(fileName = "AlienData", menuName = "Alien/Data", order = 1)]

public class AlienData : ScriptableObject
{
    [Header("Fire")]
    public float TimeToFire = 2.5f;

    [Header("Sounds")]
    public AudioClip SpawnClip;
    public AudioClip ShootClip;
}
