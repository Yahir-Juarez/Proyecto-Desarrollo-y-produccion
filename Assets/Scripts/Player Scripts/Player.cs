using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
public class Player : MonoBehaviour
{
    //private Animator animation;

    [SerializeField]
    Camera instanceCamera;

    public StateMachine<Player> stateMachine = new StateMachine<Player>();

    public IdlePlayer idleState;
    public WalkPlayer walkState;
    public RunPlayer runState;
    public JumpPlayer jumpState;

    private float posCamera = 4.38f;


    // Start is called before the first frame update
    void Start()
    {
        idleState = GetComponent<IdlePlayer>();
        walkState = GetComponent<WalkPlayer>();
        runState = GetComponent<RunPlayer>();
        jumpState = GetComponent<JumpPlayer>();
        stateMachine.Owner = this;
        stateMachine.CurrentState = idleState;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        moveCamera();
    }

    private void moveCamera()
    {
        if (transform.position.x > 0)
        {
            Vector3 posPlayer;
            posPlayer = Camera.main.transform.position;
            posPlayer.x = transform.position.x;
            instanceCamera.transform.position = posPlayer;
        }
        if (transform.position.y > posCamera)
        {
            Vector3 posPlayer;
            posPlayer = Camera.main.transform.position;
            posPlayer.y = transform.position.y;
            instanceCamera.transform.position = posPlayer;
        }
    }

    //private void orientation()
    //{
    //    if (Input.GetAxis("Horizontal") != 0)
    //    {
    //        Vector3 rotation = new Vector3(0, 180, 0);
    //        visualRotation.rotation = Quaternion.Euler(rotation);
    //    }
    //}
}