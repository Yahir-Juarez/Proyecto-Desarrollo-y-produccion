using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyADC : MonoBehaviour
{
    [SerializeField]
    private int life = 1;
    public StateMachine<EnemyADC> stateMachine = new StateMachine<EnemyADC>();
    public IdleADC idleState;
    public ShotADC shotState;
    public DeathADC deathState;
    // Start is called before the first frame update
    void Start()
    {
        idleState = GetComponent<IdleADC>();
        shotState = GetComponent<ShotADC>();
        deathState = GetComponent<DeathADC>();
        stateMachine.CurrentState = idleState;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void setDamage(int damage)
    {
        life -= damage;
    }
    public int getLife()
    {
        return life;
    }

    public void DeathADC()
    {
        Destroy(gameObject);
    }
}
