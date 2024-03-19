using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private Enemy1 enemyPaladin;
    [SerializeField]
    private Enemy1 enemyParasite;
    [SerializeField]
    private EnemyADC enemyADC;
    private List<GameObject> posEnemys;
    ///////  Pendiente Enemigo 3  ////////
    private List<GameObject> enemysList;

    float rotateADC = 270;


    // Start is called before the first frame update
    void Start()
    {
        posEnemys = new List<GameObject>();
        enemysList = new List<GameObject>();
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

    public void createEnemy()
    {
        for (int i = 0; i < posEnemys.Count; i++)
        {
            if (posEnemys[i].gameObject.tag == "EnemyPaladin")
            {
                Enemy1 newEnemy = Instantiate(enemyPaladin, posEnemys[i].transform.position, Quaternion.identity);
                enemysList.Add(newEnemy.gameObject);
            }
            else if (posEnemys[i].gameObject.tag == "EnemyParasite")
            {
                Enemy1 newEnemy = Instantiate(enemyParasite, posEnemys[i].transform.position, Quaternion.identity);
                enemysList.Add(newEnemy.gameObject);
            }
            else if (posEnemys[i].gameObject.tag == "EnemyTirador")
            {
                EnemyADC newEnemy = Instantiate(enemyADC, posEnemys[i].transform.position, Quaternion.identity);
                newEnemy.transform.Rotate(0f, 270f, 0f);
                enemysList.Add(newEnemy.gameObject);
            }
        }
    }

    public List<GameObject> getListEnemy()
    {
        return enemysList;
    }

    public void AddEnemy(float posEnemyX, float posEnemyY, int Type)
    {
        Vector3 newPosEnemy = new Vector3(posEnemyX, posEnemyY, 0);
        if (Type == 1)
        {
            Enemy1 newEnemy = Instantiate(enemyPaladin, newPosEnemy, Quaternion.identity);
            enemysList.Add(newEnemy.gameObject);
        }
        else if (Type == 2) 
        {
            Enemy1 newEnemy = Instantiate(enemyParasite, newPosEnemy, Quaternion.identity);
            enemysList.Add(newEnemy.gameObject);
        }
        else if(Type == 3) 
        {
            EnemyADC newEnemy = Instantiate(enemyADC, newPosEnemy, Quaternion.identity);
            newEnemy.gameObject.transform.Rotate(0, rotateADC, 0);
            enemysList.Add(newEnemy.gameObject);
        }
    }

    public void clearEnemys()
    {
        for (int i = enemysList.Count - 1; i >= 0; i--) 
        {
            if (enemysList[i] != null)
            {
                Destroy(enemysList[i]);
            }
            enemysList.RemoveAt(i);
        }
    }

    //public void deleteEnemy(GameObject enemyObject)
    //{
    //    for (int i = 0; i < enemysList.Count; i++)
    //    {
    //        if (enemysList[i] == enemyObject) 
    //        { 
                
    //        }
    //    }
    //}
}
