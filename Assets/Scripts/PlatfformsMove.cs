using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfformsMove : MonoBehaviour
{
    [SerializeField]
    float distance = 10;
    [SerializeField]
    float speed = 1;
    float moved = 0;
    bool moveRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        float velocity = speed;
        if (!moveRight) 
        {
            velocity *= -1;
        }
        Vector3 MoveX_Z = new Vector3(1, 0, 0);
        transform.Translate(MoveX_Z * velocity * Time.deltaTime);
        moved += MoveX_Z.x * velocity * Time.deltaTime;
        if (moved >= distance && moveRight)
        {
            moveRight = false;
        }
        else if (moved <= 0 && !moveRight)
        {
            moveRight = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Feads")
        {
            Player instance = other.GetComponentInParent<Player>();
            Vector3 MoveX_Z = new Vector3(0, 0, 1);
            if (instance == null) { return; }
            if (instance.getDireccion() == false)
            {
                MoveX_Z.z = -1;
            }
            float velocity = speed;
            if (!moveRight)
            {
                velocity *= -1;
            }
             
            instance.gameObject.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
            if (instance.getArrows().Count > 0) 
            {
                if (instance.getArrows()[instance.getArrows().Count - 1] == null) { return; } 
                instance.getArrows()[instance.getArrows().Count - 1].transform.Translate(MoveX_Z * velocity * Time.deltaTime);
            }
        }
    }
}
