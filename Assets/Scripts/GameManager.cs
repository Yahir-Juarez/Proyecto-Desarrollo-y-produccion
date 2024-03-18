using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player instancePalyer;
    [SerializeField]
    private EnemyFactory instanceEenemyFactory;
    [SerializeField]
    private ArrowFactory instanceArrowFactory;
    [SerializeField]
    private Camera instanceCamera;
    Vector3 posCamera;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Hay muchas intancias creadas");
        }
        posCamera = instanceCamera.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player GetPlayer()
    {
        return instancePalyer;
    }

    public EnemyFactory GetEnemyFactory()
    {
        return instanceEenemyFactory;
    }

    public void resetLevel()
    {
        instanceEenemyFactory.clearEnemys();
        instanceEenemyFactory.createEnemy();
        instanceArrowFactory.clearArrows();
        instanceArrowFactory.createArrows();
        instanceCamera.transform.position = posCamera;
        instancePalyer.transform.position = instancePalyer.getPosInicial();
        instancePalyer.resetValue();
    }
}
