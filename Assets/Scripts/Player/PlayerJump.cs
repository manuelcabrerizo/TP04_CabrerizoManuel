using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D body2D;
    private Animator animator;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        body2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.Fall)
        {
            return;
        }

        if (playerController.Grounded)
        {
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("Ducking", true);
            }
            else 
            {
                animator.SetBool("Ducking", false);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                body2D.velocity = new Vector2();
                body2D.AddForce(new Vector2(0, playerController.PlayerData.JumpForce), ForceMode2D.Impulse);
                AudioManager.Instance.PlayClip(playerController.PlayerData.JumpClip, AudioSourceType.SFX);
            }
        }
    }
}
