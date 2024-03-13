using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : MonoBehaviour, IState
{
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Player instancePlayer;
    private Rigidbody rbInstance;
    public void CheckEnterConditions()
    {

    }

    public void Execute()
    {

    }

    public void OnEnter()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("isDeath", true);
    }

    public void OnExit()
    {

    }
    public void spawn()
    {
        if (instancePlayer.getLife() > 0)
        {

        }
    }
}
