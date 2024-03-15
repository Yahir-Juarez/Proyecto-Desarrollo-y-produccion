using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private Enemy1 enemyPaladin;
    [SerializeField]
    private Enemy1 enememyParasite;
    [SerializeField]
    private List<GameObject> posEnemys;
    ///////  Pendiente Enemigo 3  ////////
    private List<GameObject> enemysList;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform objects in transform)
        {
            posEnemys.Add(objects.gameObject);
        }
        createEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createEnemy()
    {
        for (int i = 0; i < posEnemys.Count - 1; i++)
        {
            Instantiate(enemyPaladin, posEnemys[i].transform);
        }
    }
}
