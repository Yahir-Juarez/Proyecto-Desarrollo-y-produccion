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

    //////////////////////Variables declaradas//////////////////
    private int limitYDeath = -7;
    private float limitY;
    private float limitX = 6.33f;
    private Quaternion rotacionA = Quaternion.Euler(0, 270, 0);
    private Quaternion rotacionD = Quaternion.Euler(0, 90, 0);
    private float actualSpeed = 10.0f;

    private Vector3 spawnActual;
    private Vector3 spawnInicial;

    bool direccionPlayer = true;
    private List<GameObject> listArrow;

    private bool damageRecive = false;

    ////////////// Valores Iniciales ////////////////
    private int arrowInitial;
    private int lifeInitial;

    // Start is called before the first frame update
    void Start()
    {
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
        spawnActual = transform.position;
        spawnInicial = spawnActual;
        arrowInitial = m_arrows;
        lifeInitial = m_life;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        moveCamera();
        orientation();
        comprobateArrows();
        fallWorld(transform.position);
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            spawnActual = transform.position;
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
        if (damageRecive == false)
        {
            damageRecive = true;
            stateMachine.CurrentState.OnExit();
            m_life -= damage;
            stateMachine.CurrentState = deathState;
            stateMachine.CurrentState.OnEnter();
        }
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

    public void setPosPlayer(float posX, float posY)
    {
        gameObject.transform.position = new Vector3(posX, posY, 0);
    }

    public Vector3 getSpawn()
    {
        return spawnActual;
    }

    public void setSpawn(Vector3 posPlayer)
    {
        spawnActual = posPlayer;
    }

    public void setDamageRecive(bool active)
    {
        damageRecive = active;
    }

    public Vector3 getPosInicial()
    {
        return spawnInicial;
    }

    public void resetValue()
    {
        m_life = lifeInitial;
        m_arrows = arrowInitial;
        runState.resetSpeed();
        walkState.resetSpeed();
    }
}