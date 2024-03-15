using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotState : MonoBehaviour, IState
{
    [SerializeField]
    GameObject prefabArrow;
    private Player instancePLayer;
    public Animator instanceAnimator;

    private float speedRecharge = 0.40f;
    private bool shot = false;
    private bool isShot = false;
    public void CheckEnterConditions()
    {
        
    }

    public void Execute()
    {
        if (instancePLayer.getListArrow().Count > 0 && shot == false)
        {
            Vector3 recharge = new Vector3(0, 0, -1);
            instancePLayer.getListArrow()[instancePLayer.getListArrow().Count - 1].transform.Translate(recharge * speedRecharge * Time.deltaTime);
        }
    }

    public void OnEnter()
    {
        if (instancePLayer == null)
        {
            instancePLayer = GetComponent<Player>();
        }
        instanceAnimator.SetBool("isShot", true);
        shot = false;
        isShot = false;
    }

    public void OnExit()
    {
        isShot = true;
    }

    public void shoot()
    {
        Bullets arrowShoting = instancePLayer.getListArrow()[instancePLayer.getListArrow().Count-1].GetComponent<Bullets>();
        shot = true;
        arrowShoting.setShotB(shot);
    }

    public void instanceArrow()
    {
        if (isShot == false)
        {
            if (prefabArrow != null)
            {
                float addHeightInstance = 2.2f;
                float addWightInstance = 0.94f;
                float addFrontInstance = -.15f;
                if (instancePLayer.getDireccion() == false)
                {
                    addWightInstance *= -1;
                    addFrontInstance *= -1;
                }
                Vector3 pos = transform.position;
                pos.y += addHeightInstance;
                pos.x += addWightInstance;
                pos.z += addFrontInstance;
                GameObject flecha = Instantiate(prefabArrow, pos, transform.rotation);
                Bullets script = flecha.GetComponent<Bullets>();
                script.setDamage(instancePLayer.getDamage());
                instancePLayer.setNewArrow(flecha);
            }
        }
        isShot = true;
    }
    public void exitShot()
    {
        instancePLayer.setArrows(-1);
        instanceAnimator.SetBool("isShot", false);
        OnExit();
        instancePLayer.stateMachine.CurrentState = instancePLayer.idleState;
        instancePLayer.stateMachine.CurrentState.OnEnter();
    }
}
