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
    
    [SerializeField]
    private int lifeEnemy = 1;

    private float posCamera = 4.38f;
    public bool preparedAtack = false;

    private Quaternion rotacionA = Quaternion.Euler(0, 270, 0);
    private Quaternion rotacionD = Quaternion.Euler(0, 90, 0);

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
        orientation();
    }
    private void orientation()
    {
        if (stateMachine.CurrentState == runState)
        {
            if (runState.getDireccion() != 0 && runState.getDireccion() > 0)
            {
                transform.rotation = rotacionD;
            }
            else if (runState.getDireccion() != 0 && runState.getDireccion() < 0)
            {
                transform.rotation = rotacionA;
            }
        }
        else if (stateMachine.CurrentState == walkState)
        {
            if (walkState.getMove() == true)
            {
                transform.rotation = rotacionD;
            }
            else if (walkState.getMove() == false)
            {
                transform.rotation = rotacionA;
            }
        }
    }
    public void setDamage(int damage)
    {
        lifeEnemy -= damage;
    }
    public void enemyDead()
    {
        Destroy(gameObject);
    }

    public int getLife()
    {
        return lifeEnemy;
    }
}
