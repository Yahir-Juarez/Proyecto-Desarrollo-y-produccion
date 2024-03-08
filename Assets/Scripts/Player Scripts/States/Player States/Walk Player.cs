using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPlayer : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 10.0f;
    [SerializeField]
    GameObject Instance;
    public Animator PlayerAnimator;

    public void Execute()
    {
        float MoveY = Input.GetAxis("Jump");
        //float MoveZ = Input.GetAxis("Vertical");

        float MoveX = Input.GetAxis("Horizontal");

        Vector3 MoveX_Z = new Vector3(0, MoveY, MoveX);

        PlayerAnimator.SetFloat("Speed", MoveX);
        Instance.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        float posX = Input.GetAxis("Horizontal");
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnExit();
            Player instance = GetComponent<Player>();
            instance.stateMachine.CurrentState = instance.runState;
        }
    }
}
