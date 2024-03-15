using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JumpPlayer : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 7.5f;
    [SerializeField]
    GameObject Instance;
    [SerializeField]
    Player instancePlayer;
    [SerializeField]
    private float jumpForce = 20;
    public Animator PlayerAnimator;
    private bool isJump = false;
    private bool onFloor = true;

    private Rigidbody rb;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }
    public void CheckEnterConditions()
    {
        //if (onFloor == true)
        //{
        //    OnExit();
        //    if (Input.GetAxis("Horizontal") == 0)
        //    {
        //        Player instance = GetComponent<Player>();
        //        instance.stateMachine.CurrentState = instance.idleState;
        //        instance.stateMachine.CurrentState.OnEnter();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.LeftShift))
        //    {
        //        Player instance = GetComponent<Player>();
        //        instance.stateMachine.CurrentState = instance.runState;
        //        instance.stateMachine.CurrentState.OnEnter();
        //    }
        //    else
        //    {
        //        Player instance = GetComponent<Player>();
        //        instance.stateMachine.CurrentState = instance.walkState;
        //        instance.stateMachine.CurrentState.OnEnter();
        //    }
        //}
        //if (rb.velocity.normalized.y == 0)
        //{
        //    OnExit();
        //    instancePlayer.stateMachine.CurrentState = instancePlayer.idleState;
        //    instancePlayer.stateMachine.CurrentState.OnEnter();
        //}
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Salto");
        }

        float MoveX = Input.GetAxis("Horizontal");

        if (MoveX != 0)
        {
            Vector3 MoveX_Z = new Vector3(0, 0, 1);
            Instance.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
        }


        //PlayerAnimator.SetFloat("Jump", rb.velocity.normalized.y);
        if (isJump == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJump = false;
            Debug.Log("Salto 1");
        }
    }

    public void OnEnter()
    {
        PlayerAnimator.SetBool("isJump", true);
        isJump = true;
        velocity = instancePlayer.getActualSpeed();
        //PlayerAnimator.SetBool("isJump", true);
        //isJump = false;
    }

    public void OnExit()
    {
        //PlayerAnimator.SetBool("isJump", false);
        //PlayerAnimator.SetBool("onFloor", true);
        //isJump= false;
        //onFloor = true;
        isJump = false;
    }

    private void OnTriggerStay(Collider other)
    {
        //PlayerAnimator.SetBool("onFloor", true);
        //isJump = false;
        //onFloor = true;
        if (other.tag == "Plattform")
        {
            onFloor = true;
            PlayerAnimator.SetBool("onFloor", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plattform")
        {
            PlayerAnimator.SetBool("isJump", false);
            OnExit();
            instancePlayer.stateMachine.CurrentState = instancePlayer.idleState;
            instancePlayer.stateMachine.CurrentState.OnEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plattform")
        {
            if (onFloor == true)
            {
                PlayerAnimator.SetBool("onFloor", false);
            }
        }
        //if (other.tag != "Plattform") { return; }

        //if (instancePlayer != null && onFloor != false) 
        //{
        //    onFloor = false;
        //    instancePlayer.stateMachine.CurrentState.OnExit();
        //    instancePlayer.stateMachine.CurrentState = instancePlayer.jumpState;
        //    PlayerAnimator.SetBool("onFloor", false);
        //    PlayerAnimator.SetBool("isJump", false);
        //    isJump = true;
        //}
    }
}
