using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleADC : MonoBehaviour, IState
{
    bool playerInArea = false;
    bool playerInCollisionEnemy = false;
    [SerializeField]
    public Animator EnemyAnimator;
    private EnemyADC instanceEnemy;

    private void Awake()
    {
        instanceEnemy = GetComponent<EnemyADC>();
    }
    public void CheckEnterConditions()
    {
        if (instanceEnemy.getLife() <= 0) 
        {
            OnExit();
            instanceEnemy.stateMachine.CurrentState = instanceEnemy.deathState;
            instanceEnemy.stateMachine.CurrentState.OnEnter();
        }
    }

    public void Execute()
    {
        
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (instanceEnemy == null) { return; }
            if (playerInArea == false)
            {
                playerInArea = true;
                OnExit();
                instanceEnemy.stateMachine.CurrentState = instanceEnemy.shotState;
                instanceEnemy.stateMachine.CurrentState.OnEnter();
            }
            else if (playerInCollisionEnemy == false)
            {
                playerInCollisionEnemy = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (instanceEnemy == null) { return; }
            if (playerInCollisionEnemy == true)
            {
                playerInCollisionEnemy = false;
            }
            else if (playerInArea == true)
            {
                playerInArea = false;
            }
        }
    }

    public bool getPlayerInArea()
    {
        return playerInArea;
    }
}
