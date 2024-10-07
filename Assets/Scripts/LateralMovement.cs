using UnityEngine;

public class LateralMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    Rigidbody2D body2D;

    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        body2D.velocity = new Vector2(-playerData.InitialSpeed, 0);
    }

    private void OnDisable()
    {
        body2D.velocity = new Vector2(0, 0);
    }

}
