using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 10.0f;
    [SerializeField]
    GameObject Instance;
    public Animator EnemyAnimator;
    [SerializeField]
    int perimeter = 20;
    [SerializeField]
    Player instancePlayer;
    [SerializeField]
    Enemy1 instanceEnemy;

    private float distance = 0;
    private bool moveRight = true;
    public void Execute()
    {
        int moveX = 1;
        if (moveRight == false)
        {
            moveX = moveX * -1;
        }

        Vector3 MoveX = new Vector3(0, 0, 1);

        Instance.transform.Translate(MoveX * velocity * Time.deltaTime);
        if (moveRight == true)
        {
            distance += MoveX.z * velocity * Time.deltaTime;
        }
        else
        {
            distance -= MoveX.z * velocity * Time.deltaTime;
        }
        EnemyAnimator.SetFloat("Walk", moveX);
        if (distance >= perimeter)
        {
            moveRight = false;
        }
        if (distance <= 0 && moveRight == false)
        {
            moveRight = true;
        }

    }
    public void CheckEnterConditions()
    {
        if (instanceEnemy == null)
        {
            instanceEnemy = GetComponent<Enemy1>();
        }
        if (instanceEnemy.getLife() <= 0)
        {
            OnExit();
            instanceEnemy.stateMachine.CurrentState = instanceEnemy.deathState;
            instanceEnemy.stateMachine.CurrentState.OnEnter();
        }
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        EnemyAnimator.SetFloat("Walk", 0);
    }

    public bool getMove()
    {
        return moveRight;
    }
}
