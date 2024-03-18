using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField]
    private float speedArrow = 1;
    [SerializeField]
    private float distance = 10;
    [SerializeField]
    private string collisionName;
    [SerializeField]
    private string otherEnemy;
    [SerializeField]
    private int m_damage = 1;
    private bool shot = false;
    private float Route = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveArrow();
    }

    private void moveArrow()
    {
        if (shot == true) 
        {
            Vector3 shoting = new Vector3(0, 0, 1);
            transform.Translate(shoting * speedArrow * Time.deltaTime);
            Route += shoting.z * speedArrow * Time.deltaTime;
        }
        if (Route >= distance)
        {
            Destroy(gameObject);
        }
    }
    
    public void setShotB(bool stateShot)
    {
        shot = stateShot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == collisionName || other.tag == otherEnemy)
        {
            if (shot == true)
            {
                if (other.tag == "Player")
                {
                    Player instancePlayer = other.GetComponent<Player>();
                    instancePlayer.setDamage(1);
                    Destroy(gameObject);
                }
                else if (other.tag == "EnemyCollider")
                {
                    Enemy1 enemyInstance = other.GetComponentInParent<Enemy1>();
                    enemyInstance.setDamage(m_damage);
                    Destroy(gameObject);
                }
                else if (other.tag == "ADC")
                {
                    EnemyADC enemyInstance = other.GetComponentInParent<EnemyADC>();
                    enemyInstance.setDamage(m_damage);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void setDamage(int damage)
    {
        m_damage = damage;
    }
}
