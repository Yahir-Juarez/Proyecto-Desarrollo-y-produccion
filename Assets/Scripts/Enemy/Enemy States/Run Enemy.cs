using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunEnemy : MonoBehaviour, IState
{
    [SerializeField]
    float velocity = 20.0f;
    [SerializeField]
    GameObject Instance;
    public Animator EnemyAnimator;
    private GameObject instancePlayer;
    private Player O_instancePlayer;
    [SerializeField]
    Enemy1 instanceEnemy;
    private float direccionNormalize;

    public void Execute()
    {
        if (O_instancePlayer != null)
        {
            // Calcula la dirección hacia el objetivo
            Vector3 direccion = O_instancePlayer.transform.position - transform.position;
            direccion.Normalize();
            EnemyAnimator.SetFloat("Run", 1);
            direccionNormalize = direccion.x;
            direccion.y = 0;
            direccion.z = 0;
            transform.position += direccion * velocity * Time.deltaTime;
        }
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
            if (instanceEnemy.getLife() > 0)
            {
                O_instancePlayer = cPlayer.GetComponent<Player>();
                if (instanceEnemy.preparedAtack == true && instancePlayer == null)
                {
                    instanceEnemy.stateMachine.CurrentState.OnExit();
                    instancePlayer = cPlayer.gameObject;
                    instanceEnemy.stateMachine.CurrentState = instanceEnemy.atackState;
                    instanceEnemy.stateMachine.CurrentState.OnEnter();
                }
                else
                {
                    instanceEnemy.preparedAtack = true;
                    instanceEnemy.stateMachine.CurrentState.OnExit();
                    instanceEnemy.stateMachine.CurrentState = instanceEnemy.runState;
                    instanceEnemy.stateMachine.CurrentState.OnEnter();
                }
            }
        }
    }
    private void OnTriggerExit(Collider cPlayer)
    {
        if (cPlayer.gameObject.CompareTag("Player"))
        {
            if (instancePlayer == null && instanceEnemy.preparedAtack == true)
            {
                instanceEnemy.stateMachine.CurrentState.OnExit();
                instanceEnemy.preparedAtack = false;
                instanceEnemy.stateMachine.CurrentState = instanceEnemy.walkState;
                instanceEnemy.stateMachine.CurrentState.OnEnter();
            }
            else
            {
                instancePlayer = null;
            }
        }
    }

    public GameObject GetObject()
    {
        return instancePlayer;
    }

    public float getDireccion()
    {
        return direccionNormalize;
    }
    public GameObject getPlayerObject()
    {
        return instancePlayer;
    }
}
