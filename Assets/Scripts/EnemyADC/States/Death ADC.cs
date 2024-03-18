using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathADC : MonoBehaviour, IState
{

    [SerializeField]
    public Animator EnemyAnimator;
    private EnemyADC instanceEnemy;

    private void Awake()
    {
        instanceEnemy= GetComponent<EnemyADC>();
        EnemyAnimator = GetComponent<Animator>();
    }
    public void CheckEnterConditions()
    {

    }

    public void Execute()
    {

    }

    public void OnEnter()
    {
        EnemyAnimator.SetBool("isDeath", true);
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

}
