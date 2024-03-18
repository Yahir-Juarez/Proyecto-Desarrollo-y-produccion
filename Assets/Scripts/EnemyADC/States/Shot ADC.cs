using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotADC : MonoBehaviour, IState
{
    [SerializeField]
    public Animator EnemyAnimator;
    private EnemyADC instanceEnemy;
    [SerializeField]
    private Bullets instanceBullet;
    Bullets instancia;

    private bool isCreate = false;

    float scaleX = 0.0f;
    float scaleY = 0.0f;

    private void Awake()
    {
        instanceEnemy = GetComponent<EnemyADC>();
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

    public void Execute()
    {
        if (isCreate == true)
        {
            if (scaleX < 70.0f)
            {
                scaleX += 1.0f;
                scaleY += 1.0f;
                instancia.transform.localScale = new Vector3(scaleX, scaleY, instancia.transform.localScale.z);
            }
            Vector3 pos = instancia.transform.position;

            pos.x -= 0.1f;

            instancia.transform.position = pos;
        }
    }

    public void OnEnter()
    {
        EnemyAnimator.SetBool("isFire", true);
    }

    public void OnExit()
    {
        EnemyAnimator.SetBool("isFire", false);
    }

    public void extiShot()
    {
        if (instanceEnemy == null) { return; }
        if (instanceEnemy.idleState.getPlayerInArea() == false)
        {
            OnExit();
            instanceEnemy.stateMachine.CurrentState = instanceEnemy.idleState;
            instanceEnemy.idleState.OnEnter();
        }
    
    }

    public void createBall()
    {
        if (instanceEnemy == null) { return; }
        GameObject posBall = transform.Find("PointShot").gameObject;
        instancia = Instantiate(instanceBullet, posBall.transform.position, Quaternion.identity);
        instancia.transform.Rotate(0f, 270f, 0f);
        isCreate = true;
    }

    public void shot()
    {
        if (instanceEnemy == null) { return; }
        scaleX = 0.0f;
        scaleY = 0.0f;
        instancia.setShotB(true);
        instancia = null;
        isCreate = false;
    }
}
