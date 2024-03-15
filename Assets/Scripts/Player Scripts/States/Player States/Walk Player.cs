using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPlayer : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 10.0f;
    [SerializeField]
    GameObject Instance;
    Player instancePlayer;
    public Animator PlayerAnimator;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        instancePlayer = GetComponent<Player>();
    }
    public void Execute()
    {
        //float MoveY = Input.GetAxis("Jump");
        //float MoveZ = Input.GetAxis("Vertical");

        float MoveX = Input.GetAxis("Horizontal");

        Vector3 MoveX_Z = new Vector3(0, 0, 1);

        PlayerAnimator.SetFloat("Speed", MoveX);
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
        PlayerAnimator.SetFloat("Speed", 0);
    }
    public void CheckEnterConditions()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.idleState;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(rb.velocity.normalized.y);
            //OnExit();
            //Player instance = GetComponent<Player>();
            //instance.stateMachine.CurrentState = instance.jumpState;
            //instance.stateMachine.CurrentState.OnEnter();
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.jumpState;
            instance.stateMachine.CurrentState.OnEnter();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.runState;
            instance.stateMachine.CurrentState.OnEnter();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && instancePlayer.getArrow() > 0)
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
