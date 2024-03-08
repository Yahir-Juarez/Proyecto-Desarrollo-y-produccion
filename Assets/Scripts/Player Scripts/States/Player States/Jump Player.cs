using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 7.5f;
    [SerializeField]
    GameObject Instance;
    public Animator PlayerAnimator;

    bool isFall = true;
    public void CheckEnterConditions()
    {
        //if (Input.GetAxis("Horizontal") == 0)
        //{
        //    Player instance = GetComponent<Player>();
        //    instance.stateMachine.CurrentState = instance.idleState;
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    Player instance = GetComponent<Player>();
        //    instance.stateMachine.CurrentState = instance.runState;
        //}
    }

    public void Execute()
    {
        float MoveX = Input.GetAxis("Horizontal");

        Vector3 MoveX_Z = new Vector3(0, 0, MoveX);

        PlayerAnimator.SetFloat("Run", MoveX);

        Instance.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
    }

    public void OnEnter()
    {
        isFall = false;
        PlayerAnimator.SetBool("isJump", true);
    }

    public void OnExit()
    {
        PlayerAnimator.SetBool("isJump", false);
    }

    void OnCollisionEnter(Collision cEnemy)
    {
        // Verificar si estamos colisionando con otro objeto
        if (cEnemy.gameObject.CompareTag("Plattform"))
        {
            OnExit();
            if (Input.GetAxis("Horizontal") == 0)
            {
                Player instance = GetComponent<Player>();
                instance.stateMachine.CurrentState = instance.idleState;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Player instance = GetComponent<Player>();
                instance.stateMachine.CurrentState = instance.runState;
            }
            else 
            {
                Player instance = GetComponent<Player>();
                instance.stateMachine.CurrentState = instance.walkState;
            }
        }
    }
}
