using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private LayerMask jumpadbleGround;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 14f;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip[] stepSounds;
    [SerializeField] private AudioSource stepsAudioSource;

    private PlayerMovementModel playerMovementModel;
    private UIMovementModel uiMovementModel;

    // Start is called before the first frame update
    private void Start()
    {
        playerMovementModel = new PlayerMovementModel(GetComponent<Rigidbody2D>(), 
            GetComponent<BoxCollider2D>(), jumpadbleGround, moveSpeed, jumpSpeed);
        uiMovementModel = new UIMovementModel(jumpSound, stepSounds, stepsAudioSource,
            GetComponent<SpriteRenderer>(), GetComponent<Animator>());
    }

    // Update is called once per frame
    private void Update()
    {
        playerMovementModel.MoveHorizontally(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(playerMovementModel.MoveVertically())
            {
                stepsAudioSource.PlayOneShot(jumpSound);
            }
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (playerMovementModel.IsMovingLeft())
        {
            uiMovementModel.MoveLeft();
        }
        if (playerMovementModel.IsMovingRight())
        {
            uiMovementModel.MoveRight();
        }
        uiMovementModel.SetAnimationState(playerMovementModel.GetAnimationState());
    }

    // called by unity animator
    private void Step()
    {
        uiMovementModel.Step();
    }
}
