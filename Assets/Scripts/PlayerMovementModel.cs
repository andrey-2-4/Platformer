using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModel
{
    private Rigidbody2D playerBody;
    private BoxCollider2D boxCollider;

    private LayerMask jumpadbleGround;

    private float dirX = 0;
    private float moveSpeed = 7f;
    private float jumpSpeed = 14f;

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling
    }

    public PlayerMovementModel(Rigidbody2D playerBody, BoxCollider2D boxCollider,
        LayerMask jumpadbleGround, float moveSpeed, float jumpSpeed)
    {
        this.playerBody = playerBody;
        this.boxCollider = boxCollider;
        this.jumpadbleGround = jumpadbleGround;
        this.moveSpeed = moveSpeed;
        this.jumpSpeed = jumpSpeed;
    }

    public void MoveHorizontally(float dirX)
    {
        playerBody.velocity = new Vector2(dirX * moveSpeed, playerBody.velocity.y);
        this.dirX = dirX;
    }

    public bool MoveVertically()
    {
        if (IsGrounded())
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpSpeed);
            return true;
        }
        return false;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpadbleGround);
    }

    public bool IsMovingRight()
    {
        if (dirX > 0f)
        {
            return true;
        }
        return false;
    }

    public bool IsMovingLeft()
    {
        if (dirX < 0f)
        {
            return true;
        }
        return false;
    }

    public int GetAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (playerBody.velocity.y > .01f)
        {
            state = MovementState.jumping;
        }
        else if (playerBody.velocity.y < -.01f)
        {
            state = MovementState.falling;
        }

        return (int)state;
    }
}
