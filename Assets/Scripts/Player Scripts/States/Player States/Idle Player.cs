using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class IdlePlayer : MonoBehaviour, IState
{
    [SerializeField]
    GameObject Instance;
    private Player instancePlayer;
    public Animator PlayerAnimator;
    public void CheckEnterConditions()
    {
        if (instancePlayer == null)
        {
            instancePlayer = GetComponent<Player>();
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            //PlayerAnimator.SetFloat("isWalk", );
            //layerAnimator.SetBool("isIdle", false);
            
            instancePlayer.stateMachine.CurrentState.OnExit();
            instancePlayer.stateMachine.CurrentState = instancePlayer.walkState;
            instancePlayer.stateMachine.CurrentState.OnEnter();
        }
        else
        {
            PlayerAnimator.SetFloat("Run", 0.0f);
            PlayerAnimator.SetFloat("Speed", 0.0f);
        }
        if (Input.GetAxis("Jump") != 0)
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.jumpState;
            instance.stateMachine.CurrentState.OnEnter();
            //instancePlayer.stateMachine.CurrentState.OnExit();
            //instancePlayer.stateMachine.CurrentState = instancePlayer.jumpState;
            //instancePlayer.stateMachine.CurrentState.OnEnter();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            instancePlayer.stateMachine.CurrentState.OnExit();
            instancePlayer.stateMachine.CurrentState = instancePlayer.shotState;
            instancePlayer.stateMachine.CurrentState.OnEnter();
        }

        if (instancePlayer.getLife() <= 0)
        {
            OnExit();
            instancePlayer.stateMachine.CurrentState = instancePlayer.deathState;
        }
    }

    public void Execute()
    {

    }

    public void OnEnter()
    {
        //throw new System.NotImplementedException();
    }

    public void OnExit()
    {

    }
}
