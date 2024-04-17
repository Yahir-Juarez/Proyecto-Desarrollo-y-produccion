using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyADC : MonoBehaviour
{
    [SerializeField]
    private int life = 1;
    [SerializeField]
    private GameObject instancePlayer;
    public StateMachine<EnemyADC> stateMachine = new StateMachine<EnemyADC>();
    public IdleADC idleState;
    public ShotADC shotState;
    public DeathADC deathState;

    private Quaternion rotacionA = Quaternion.Euler(0, 270, 0);
    private Quaternion rotacionD = Quaternion.Euler(0, 90, 0);
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
        rotateEnemy();
    }

    private void rotateEnemy()
    {
         Vector3 posPlayer = GameManager.Instance.GetPlayer().transform.position;
         Vector3 posEnemy = transform.position;
         if (posPlayer.x < posEnemy.x)
         {
             transform.rotation = rotacionA;
         }
         else
         {
            transform.rotation = rotacionD;
        }
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
