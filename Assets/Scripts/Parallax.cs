using UnityEngine;

public enum ParallaxLayer
{ 
    Sky,   // 0.25
    Sea,   // 0.5
    Beach, // 2
    Palms, // 2
    Ground // 3
}

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ParallaxData parallaxData;
    [SerializeField] private ParallaxLayer parallaxLayer;

    SpriteRenderer spriteRendeder;
    float magnitude = 0;
    
    void Awake()
    {
        spriteRendeder = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0;

        switch(parallaxLayer)
        {
            case ParallaxLayer.Sky: speed = parallaxData.SkySpeed; break;
            case ParallaxLayer.Sea: speed = parallaxData.SeaSpeed; break;
            case ParallaxLayer.Beach: speed = parallaxData.BeachSpeed; break;
            case ParallaxLayer.Palms: speed = parallaxData.PalmsSpeed; break;
            case ParallaxLayer.Ground: speed = playerData.InitialSpeed; break;
        }

        magnitude += (speed/20) * Time.deltaTime;
        spriteRendeder.material.SetTextureOffset("_MainTex", Vector2.right * magnitude);
    }
}
