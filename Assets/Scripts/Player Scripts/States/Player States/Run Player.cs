using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class RunPlayer : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 20.0f;
    [SerializeField]
    GameObject Instance;
    public Animator PlayerAnimator;

    public void Execute()
    {
        float MoveX = Input.GetAxis("Horizontal");

        Vector3 MoveX_Z = new Vector3(0, 0, MoveX);

        PlayerAnimator.SetFloat("Run", MoveX);

        Instance.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        float posX = Input.GetAxis("Horizontal");
        PlayerAnimator.SetFloat("Run", posX);
        PlayerAnimator.SetFloat("Speed", posX);
    }
    public void CheckEnterConditions()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.idleState;
        }

        if (Input.GetAxis("Jump") != 0)
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.jumpState;
            instance.stateMachine.CurrentState.OnEnter();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.runState;
        }
    }
}