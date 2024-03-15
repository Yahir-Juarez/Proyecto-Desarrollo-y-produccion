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
    [SerializeField]
    private int m_damage = 1;
    [SerializeField]
    private int m_life = 3;
    [SerializeField]
    private int m_arrows = 3;

    public StateMachine<Player> stateMachine = new StateMachine<Player>();

    public IdlePlayer idleState;
    public WalkPlayer walkState;
    public RunPlayer runState;
    public JumpPlayer jumpState;
    public ShotState shotState;
    public DeathState deathState;

    private Rigidbody rb;
    //////////////////////Variables declaradas//////////////////
    private int limitYDeath = -7;
    private float limitY;
    private float limitX = 6.33f;
    private Quaternion rotacionA = Quaternion.Euler(0, 270, 0);
    private Quaternion rotacionD = Quaternion.Euler(0, 90, 0);
    private float actualSpeed = 10.0f;

    bool direccionPlayer = true;
    private List<GameObject> listArrow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        idleState = GetComponent<IdlePlayer>();
        walkState = GetComponent<WalkPlayer>();
        runState = GetComponent<RunPlayer>();
        jumpState = GetComponent<JumpPlayer>();
        shotState = GetComponent<ShotState>();
        deathState = GetComponent<DeathState>();
        listArrow = new List<GameObject>();
        stateMachine.Owner = this;
        stateMachine.CurrentState = idleState;
        limitY = instanceCamera.gameObject.transform.position.y;
        limitX = instanceCamera.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        moveCamera();
        orientation();
        comprobateArrows();
        fallWorld(transform.position);
        //if (rb.velocity.y < -.1)
        //{
        //    Debug.Log("Esta cayendo");
        //    Debug.Log(rb.velocity.y);
        //}
    }

    private void moveCamera()
    {
        if (transform.position.x > limitX)
        {
            Vector3 posPlayer;
            posPlayer = Camera.main.transform.position;
            posPlayer.x = transform.position.x;
            instanceCamera.transform.position = posPlayer;
        }
        if (transform.position.y > limitY)
        {
            Vector3 posPlayer;
            posPlayer = Camera.main.transform.position;
            posPlayer.y = transform.position.y;
            instanceCamera.transform.position = posPlayer;
        }
    }

    public void setDamage()
    {

    }

    private void orientation()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Aplica la rotación correspondiente
            transform.rotation = rotacionD;
            direccionPlayer = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = rotacionA;
            direccionPlayer = false;
        }
    }

    public bool getDireccion()
    {
        return direccionPlayer;
    }

    public void setNewArrow(GameObject objectArrow)
    {
        listArrow.Add(objectArrow);
    }
    public List<GameObject> getListArrow()
    {
        return listArrow;
    }
    //private void orientation()
    //{
    //    if (Input.GetAxis("Horizontal") != 0)
    //    {
    //        Vector3 rotation = new Vector3(0, 180, 0);
    //        visualRotation.rotation = Quaternion.Euler(rotation);
    //    }
    //}
    public int getDamage()
    {
        return m_damage;
    }

    private void comprobateArrows()
    {
        if (listArrow.Count > 0)
        {
            for (int i = listArrow.Count - 1; i >= 0; i--) 
            {
                if (listArrow[i] == null)
                {
                    listArrow.RemoveAt(i);
                }
            }
        }
    }
    public void setDamage(int damage)
    {
        m_life -= damage;
    }
    public int getLife()
    {
        return m_life;
    }

    public List<GameObject> getArrows()
    {
        return listArrow;
    }
    private void fallWorld(Vector3 compare)
    {

        if (compare.y < limitYDeath)
        {
            setDamage(1);
        }
    }

    public void setLife(int newLife)
    {
        m_life = newLife;
    }

    public void setActualSpeed(float speed)
    {
        actualSpeed = speed;
    }
    public float getActualSpeed()
    {
        return actualSpeed;
    }

    public void setArrows(int newArrow)
    {
        m_arrows += newArrow;
    }

    public void setArrowsCheats(int newArrows)
    {
        m_arrows = newArrows;
    }
    public int getArrow()
    {
        return m_arrows;
    }
}