using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData => playerData;
    
    // Player State
    private bool grounded;
    public bool Grounded => grounded;

    private bool fall;
    public bool Fall => fall;

    private Animator animator;
    private Rigidbody2D body2D;
    private new ParticleSystem particleSystem;


    public void Awake()
    {
        animator = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        ResetPlayer();
    }

    public void Update()
    {
        if (fall)
        {
            return;
        }
        Vector2 down = transform.TransformDirection(Vector2.down);
        Vector2 right = transform.TransformDirection(Vector2.right);
        Vector2 origin = (Vector2)transform.position + down * 0.2f;
        float scale = 0.6f;
        Vector2 xOffset = right * 0.35f;
        ProcessGrounded(origin, xOffset, scale);
        ProcessFall(origin, xOffset, scale);
        animator.SetFloat("Velocity", body2D.velocity.y);
    }


    public void ResetPlayer()
    {
        SetGrounded(false);
        SetFall(false);
        transform.position = playerData.InitialPosition;
        transform.rotation = new Quaternion();
        body2D.velocity = new Vector2();
        particleSystem.Clear();
    }

    private void ProcessGrounded(Vector2 origin, Vector2 xOffset, float scale)
    {
        Vector2 down = transform.TransformDirection(Vector2.down);
        RaycastHit2D hitGround0 = Physics2D.Raycast(origin - xOffset, down, scale, 1 << 9);
        RaycastHit2D hitGround1 = Physics2D.Raycast(origin + xOffset, down, scale, 1 << 9);
        if (hitGround0.collider != null || hitGround1.collider != null)
        {
            if (!grounded)
            {
                AudioManager.Instance.PlayClip(playerData.LandClip, AudioSourceType.SFX);
            }
            SetGrounded(true);
        }
        else
        {
            SetGrounded(false);
        }
    }

    private void ProcessFall(Vector2 origin, Vector2 xOffset, float scale)
    {
        Vector2 right = transform.TransformDirection(Vector2.right);
        Vector2 left = transform.TransformDirection(Vector2.left);
        Vector2 up = transform.TransformDirection(Vector2.up);
        Vector2 down = transform.TransformDirection(Vector2.down);

        // Check if we hit water
        RaycastHit2D hitGround0 = Physics2D.Raycast(origin - xOffset, down, scale, 1 << 10);
        RaycastHit2D hitGround1 = Physics2D.Raycast(origin + xOffset, down, scale, 1 << 10);
        if (hitGround0.collider != null || hitGround1.collider != null)
        {
            PlayerFall();
            return;
        }

        // Rays to check if the player fall
        float xScale = transform.localScale.x / 4;
        float yScale = transform.localScale.y / 4;
        RaycastHit2D hitFall0 = Physics2D.Raycast(origin, right, xScale, 1 << 9);
        RaycastHit2D hitFall1 = Physics2D.Raycast(origin, left, xScale, 1 << 9);
        RaycastHit2D hitFall2 = Physics2D.Raycast(origin, up, yScale, 1 << 9);
        if (hitFall0.collider != null || hitFall1.collider != null || hitFall2.collider != null)
        {
            PlayerFall();
            return;
        }

        // Debug Draw to see the rays
        Debug.DrawRay(origin - xOffset, down * scale, Color.green);
        Debug.DrawRay(origin + xOffset, down * scale, Color.green);
        Debug.DrawRay(origin, transform.TransformDirection(Vector2.right) * xScale, Color.red);
        Debug.DrawRay(origin, transform.TransformDirection(Vector2.left) * xScale, Color.red);
        Debug.DrawRay(origin, transform.TransformDirection(Vector2.up) * yScale, Color.red);
    }

    private void PlayerFall()
    {
        SetFall(true);
        particleSystem.Clear();
        transform.rotation = new Quaternion();
        AudioManager.Instance.PlayClip(playerData.FallClip, AudioSourceType.SFX);
        GameManager.Instance.ChangeState(GameManager.Instance.GameOverState);
    }

    private void SetGrounded(bool value)
    {
        grounded = value;
        animator.SetBool("Grounded", grounded);
    }

    private void SetFall(bool value)
    {
        fall = value;
        animator.SetBool("Fall", fall);
    }
}
