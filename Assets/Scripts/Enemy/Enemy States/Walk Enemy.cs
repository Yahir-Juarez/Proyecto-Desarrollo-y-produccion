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
    [SerializeField]
    BoxCollider instanceBox;

    private float distance = 0;
    private bool moveRight = true;
    public void Execute()
    {
        int moveX = 1;
        if (moveRight == false)
        {
            moveX = moveX * -1;
        }

        Vector3 MoveX = new Vector3(0, 0, moveX);

        Instance.transform.Translate(MoveX * velocity * Time.deltaTime);
        distance += MoveX.z * velocity * Time.deltaTime;

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
        
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        EnemyAnimator.SetFloat("Walk", 0);
    }

    private void OnTriggerEnter(Collider cPlayer)
    {
        if (cPlayer.gameObject.CompareTag("Player"))
        {
            Enemy1 instance = GetComponent<Enemy1>();
            instance.stateMachine.CurrentState.OnExit();
            instance.stateMachine.CurrentState = instance.runState;
            instance.stateMachine.CurrentState.OnEnter();
        }
        if (cPlayer)
        {

        }
    }
    private void OnTriggerExit(Collider cPlayer)
    {
        if (cPlayer.gameObject.CompareTag("Player"))
        {
            instanceEnemy.stateMachine.CurrentState.OnExit();
            instanceEnemy.stateMachine.CurrentState = instanceEnemy.walkState;
            instanceEnemy.stateMachine.CurrentState.OnEnter();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("si");
        }
    }

}
