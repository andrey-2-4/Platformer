using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeModel
{
    public PlayerLifeModel(Rigidbody2D playerBody, Animator animator)
    {
        this.playerBody = playerBody;
        this.animator = animator;
    }

    public int Health
    { 
        get
        {
            health = SaveSystem.LoadHealth();
            if (health < 0 || health > 3)
            {
                health = 3;
            }
            return health;
        }
        set
        {
            health = value;
            SaveSystem.SaveHealth(health);
        }
    }

    public void TurnOffCollision()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
    }

    public void TurnOnCollision()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    public void Die()
    {
        SaveSystem.SaveHealth(3);
        playerBody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }

    public void TurnOnHurtAnimation()
    {
        animator.SetLayerWeight(1, 1);
    }

    public void TurnOffHurtAnimation()
    {
        animator.SetLayerWeight(1, 0);
    }

    private int health;
    private Rigidbody2D playerBody;
    private Animator animator;
}
