using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackEnemy : MonoBehaviour, IState
{
    public Animator EnemyAnimator;
    [SerializeField]
    Enemy1 instanceEnemy;
    float timePass = 0;
    bool damageRecive = false;
    public void CheckEnterConditions()
    {
        if (instanceEnemy.getLife() <= 0)
        {
            OnExit();
            instanceEnemy.stateMachine.CurrentState = instanceEnemy.deathState;
            instanceEnemy.stateMachine.CurrentState.OnEnter();
        }
        if (timePass >= .63 && damageRecive == false)
        {
            damageRecive = true;
            if (instanceEnemy.runState.GetObject() != null)
            {
                //Player player = instanceEnemy.runState.GetObject().AddComponent<Player>();
                //player.setDamage();
                Debug.Log("Recibio danio");
            }
        }
        if (timePass >= 1.5)
        {
            if (instanceEnemy != null && instanceEnemy.runState.getPlayerObject() == null)
            {
                {
                    damageRecive = false;
                    OnExit();
                    instanceEnemy.stateMachine.CurrentState = instanceEnemy.runState;
                    instanceEnemy.stateMachine.CurrentState.OnEnter();
                }
            }
            else
            {
                timePass = 0;
                damageRecive = false;
            }
        }
    }

    public void Execute()
    {
        timePass += Time.deltaTime;
    }

    public void OnEnter()
    {
        if (instanceEnemy == null)
        {
            instanceEnemy = GetComponent<Enemy1>();
        }
        EnemyAnimator.SetBool("isAtack", true);
        timePass = 0;
    }

    public void OnExit()
    {
        EnemyAnimator.SetBool("isAtack", false);
    }

    public void AtackPlayer()
    {
        if (instanceEnemy.runState.getPlayerObject() != null)
        {
            Player instance = instanceEnemy.runState.getPlayerObject().GetComponent<Player>();
            instance.setDamage(1);
        }
    }
    public void onExitState()
    {
        if (instanceEnemy.runState.getRunState() == false)
        {
            instanceEnemy.stateMachine.CurrentState.OnExit();
            instanceEnemy.stateMachine.CurrentState = instanceEnemy.walkState;
            instanceEnemy.stateMachine.CurrentState.OnEnter();
        }
        else if (instanceEnemy.runState.getAtackState() == false)
        {
            instanceEnemy.stateMachine.CurrentState.OnExit();
            instanceEnemy.stateMachine.CurrentState = instanceEnemy.runState;
            instanceEnemy.stateMachine.CurrentState.OnEnter();
        }
    }
}
