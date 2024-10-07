using UnityEngine;

public class PlayerSpin : MonoBehaviour
{
    private Rigidbody2D body2D;
    private PlayerController playerController;
    private new ParticleSystem particleSystem;
    private float spinAmount;
    private float lastFrameAngle;
    private bool trickDone;

    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        particleSystem = GetComponent<ParticleSystem>();
        spinAmount = 0;
        lastFrameAngle = body2D.rotation;
        trickDone = false;
    }

    private void Update()
    {
        if (!playerController.Grounded && !trickDone)
        {
            float rotatonDelta = body2D.rotation - lastFrameAngle;
            spinAmount += rotatonDelta;

            if (spinAmount > 300 || spinAmount < -300)
            {
                AudioManager.Instance.PlayClip(playerController.PlayerData.TrickClip, AudioSourceType.SFX);
                particleSystem.Play();
                trickDone = true;
            }
        }
        else
        {
            trickDone = false;
            spinAmount = 0;
        }

        lastFrameAngle = body2D.rotation;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body2D.AddTorque(playerController.PlayerData.RotationForce, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.D))
        { 
            body2D.AddTorque(-playerController.PlayerData.RotationForce, ForceMode2D.Force);
        }
    }
}
