using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 20.0f;
    [SerializeField]
    GameObject Instance;
    public Animator EnemyAnimator;
    [SerializeField]
    Player instancePlayer;
    [SerializeField]
    Enemy1 instanceEnemy;

    public void Execute()
    {
        if (instancePlayer != null)
        {
            // Calcula la dirección hacia el objetivo
            Vector3 direccion = instancePlayer.transform.position - transform.position;

            // Normaliza la dirección para que la magnitud sea 1
            direccion.Normalize();
            EnemyAnimator.SetFloat("Run", 1);
            // Mueve el objeto hacia el objetivo
            transform.position += direccion * velocity * Time.deltaTime;
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
        EnemyAnimator.SetFloat("Run", 0);
    }

    private void OnTriggerEnter(Collider cPlayer)
    {
        if (cPlayer.gameObject.CompareTag("Player"))
        {
            if (instanceEnemy.preparedAtack == true)
            {
                instanceEnemy.stateMachine.CurrentState.OnExit();
                instanceEnemy.stateMachine.CurrentState = instanceEnemy.atackState;
                instanceEnemy.stateMachine.CurrentState.OnEnter();
            }
            else 
            {
                instanceEnemy.preparedAtack = true;
            }
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
}
