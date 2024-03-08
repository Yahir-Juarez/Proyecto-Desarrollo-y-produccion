using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackEnemy : MonoBehaviour, IState
{
    public Animator EnemyAnimator;
    public Animation eventAnim;
    [SerializeField]
    Enemy1 instanceEnemy;
    float timePass = 0;
    public void CheckEnterConditions()
    {
        if (timePass >= .63)
        {
            EnemyAnimator.SetBool("isAtack", false);
            Debug.Log("golpe");
        }
        if (timePass >= 1.5)
        {
            if (instanceEnemy != null)
            {
                instanceEnemy.stateMachine.CurrentState.OnExit();
                instanceEnemy.stateMachine.CurrentState = instanceEnemy.runState;
                instanceEnemy.stateMachine.CurrentState.OnEnter();
            }
        }
    }


    public void Execute()
    {
        timePass += Time.deltaTime;
    }

    public void OnEnter()
    {
        EnemyAnimator.SetBool("isAtack", true);
        timePass = 0;
    }

    public void OnExit()
    {
        
    }
}
