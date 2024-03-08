using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class IdlePlayer : MonoBehaviour, IState
{
    [SerializeField]
    GameObject Instance;
    public Animator PlayerAnimator;
    public void CheckEnterConditions()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            //PlayerAnimator.SetFloat("isWalk", );
            //layerAnimator.SetBool("isIdle", false);
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.walkState;
        }
        if (Input.GetAxis("Jump") != 0)
        {
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.jumpState;
            instance.stateMachine.CurrentState.OnEnter();
        }
    }

    public void Execute()
    {

    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {

    }
}
