using UnityEngine;

public class LateralMovement : MonoBehaviour
{
    Rigidbody2D body2D;

    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        body2D.velocity = new Vector2(-3, 0);
    }

}
