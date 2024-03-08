using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    Camera instanceCamera;

    public StateMachine<Enemy1> stateMachine = new StateMachine<Enemy1>();

    public IdleEnemy idleState;
    public WalkEnemy walkState;
    public RunEnemy runState;
    public AtackEnemy atackState;
    public DeathEnemy deathState;

    private float posCamera = 4.38f;
    public bool preparedAtack = false;


    // Start is called before the first frame update
    void Start()
    {
        idleState = GetComponent<IdleEnemy>();
        walkState = GetComponent<WalkEnemy>();
        runState = GetComponent<RunEnemy>();
        deathState = GetComponent<DeathEnemy>();
        atackState = GetComponent<AtackEnemy>();
        stateMachine.Owner = this;
        stateMachine.CurrentState = walkState;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
