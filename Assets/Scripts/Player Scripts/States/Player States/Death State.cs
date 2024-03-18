using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : MonoBehaviour, IState
{
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Player instancePlayer;

    private void Awake()
    {
        instancePlayer = GetComponent<Player>();
    }
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
        playerAnimator.SetBool("isDeath", false);
    }
    public void spawn()
    {
        instancePlayer.setDamageRecive(false);
        if (instancePlayer.getLife() > 0)
        {
            OnExit();
            instancePlayer.stateMachine.CurrentState = instancePlayer.idleState;
            instancePlayer.stateMachine.CurrentState.OnEnter();
            transform.position = instancePlayer.getSpawn();
        }
    }
}
