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
    private Player instancePlayer;
    public Animator PlayerAnimator;

    private void Awake()
    {
        instancePlayer = GetComponent<Player>();
    }
    public void Execute()
    {
        float MoveX = Input.GetAxis("Horizontal");

        Vector3 MoveX_Z = new Vector3(0, 0, 1);

        PlayerAnimator.SetFloat("Run", MoveX);

        Instance.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
    }

    public void OnEnter()
    {
        //throw new System.NotImplementedException();
        instancePlayer.setActualSpeed(velocity);
    }

    public void OnExit()
    {
        float posX = Input.GetAxis("Horizontal");
        PlayerAnimator.SetFloat("Run", 0);
        PlayerAnimator.SetFloat("Speed", 0);
    }
    public void CheckEnterConditions()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.idleState;
            instance.stateMachine.CurrentState.OnEnter();
        }

        if (Input.GetAxis("Jump") != 0)
        {
            //OnExit();
            //Player instance = GetComponent<Player>();
            //instance.stateMachine.CurrentState = instance.jumpState;
            //instance.stateMachine.CurrentState.OnEnter();
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
            instance.stateMachine.CurrentState.OnEnter();
        }
        if (Input.GetKeyDown(KeyCode.Q) && instancePlayer.getArrow() > 0)
        {
            Player instance = GetComponent<Player>();
            OnExit();
            instance.stateMachine.CurrentState = instance.shotState;
            instance.stateMachine.CurrentState.OnEnter();
        }
    }

    public void setSpeed(float speed)
    {
        velocity = speed;
    }
}
