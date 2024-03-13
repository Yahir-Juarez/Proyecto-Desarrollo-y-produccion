using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnemy : MonoBehaviour, IState
{
    public Animator EnemyAnimator;
    private Rigidbody rbInstance;
    private SphereCollider sphere;
    public void CheckEnterConditions()
    {
        
    }

    public void Execute()
    {
        
    }

    public void OnEnter()
    {
        if (rbInstance == null)
        {
            rbInstance = GetComponent<Rigidbody>();
        }
        if (sphere == null)
        {
            sphere = GetComponent<SphereCollider>();
        }
        sphere.isTrigger= true;
        rbInstance.useGravity = false;
        EnemyAnimator = GetComponent<Animator>();
        EnemyAnimator.SetBool("isDeath", true);
    }

    public void OnExit()
    {
        
    }
}
