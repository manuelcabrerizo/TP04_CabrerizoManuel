using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2.0f;

    SpriteRenderer spriteRendeder;
    float magnitude = 0;
    
    void Awake()
    {
        spriteRendeder = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        magnitude += (speed/20) * Time.deltaTime;
        spriteRendeder.material.SetTextureOffset("_MainTex", Vector2.right * magnitude);
    }
}
