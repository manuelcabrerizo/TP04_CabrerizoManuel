using UnityEngine;

[CreateAssetMenu(fileName = "ParallaxData", menuName = "Parallax/Data", order = 1)]

public class ParallaxData : ScriptableObject
{
    [Header("Speeds")]
    public float SkySpeed = 0.25f;
    public float SeaSpeed = 0.5f;
    public float BeachSpeed = 2.0f;
    public float PalmsSpeed = 2.0f;
}
