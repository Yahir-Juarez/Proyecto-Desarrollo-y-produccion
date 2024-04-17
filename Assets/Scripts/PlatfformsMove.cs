using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfformsMove : MonoBehaviour
{
    [SerializeField]
    bool movePlattform = true;
    [SerializeField]
    float distance = 10;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    bool moveHorizontal = true;
    [SerializeField]
    bool moveVertical = false;
    float movedHorizontal = 0;
    bool moveRight = true;

    float movedVertical = 0;
    bool moveUp = true;

    bool inPlattform = false;
    bool boxInPlattform = false;
    Player instancePlayer;
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
        if (movePlattform)
        {
            if (moveHorizontal == true)
            {
                float velocity = speed;

                if (!moveRight)
                {
                    velocity *= -1;
                }
                Vector3 MoveX_Z = new Vector3(1, 0, 0);
                transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                movedHorizontal += MoveX_Z.x * velocity * Time.deltaTime;
                if (movedHorizontal >= distance && moveRight)
                {
                    moveRight = false;
                }
                else if (movedHorizontal <= 0 && !moveRight)
                {
                    moveRight = true;
                }
            }
            if (moveVertical == true)
            {
                float velocity = speed;

                if (!moveUp)
                {
                    velocity *= -1;
                }
                Vector3 MoveX_Z = new Vector3(0, 1, 0);
                transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                if (inPlattform)
                {
                    instancePlayer.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                }
                movedVertical += MoveX_Z.y * velocity * Time.deltaTime;
                if (movedVertical >= distance && moveUp)
                {
                    moveUp = false;
                }
                else if (movedVertical <= 0 && !moveUp)
                {
                    moveUp = true;
                }
            }
        }
        else if (boxInPlattform)
        {
            if (moveVertical == true)
            {
                if (movedVertical < distance)
                {
                    float velocity = speed;

                    velocity *= -1;

                    Vector3 MoveX_Z = new Vector3(0, 1, 0);
                    transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                    if (inPlattform)
                    {
                        if (instancePlayer != null)
                        {
                            instancePlayer.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                        }
                    }
                    velocity *= -1;
                    movedVertical += MoveX_Z.y * velocity * Time.deltaTime;
                }
            }

        }
        else if (movedVertical >= 0)
        {
            float velocity = speed;

            Vector3 MoveX_Z = new Vector3(0, 1, 0);
            transform.Translate(MoveX_Z * velocity * Time.deltaTime);
            if (inPlattform)
            {
                if (instancePlayer != null)
                {
                    instancePlayer.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                }
            }
            velocity *= -1;
            movedVertical += MoveX_Z.y * velocity * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Feads")
        {
            Player instance = other.GetComponentInParent<Player>();
            if (moveHorizontal)
            {
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
            if (moveVertical)
            {
                Vector3 MoveX_Z = new Vector3(0, 1, 0);
                if (instance == null) { return; }
                float velocity = speed;
                if (!moveUp)
                {
                    velocity *= -1;
                }

                //instance.gameObject.transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                if (instance.getArrows().Count > 0)
                {
                    if (instance.getArrows()[instance.getArrows().Count - 1] == null) { return; }
                    instance.getArrows()[instance.getArrows().Count - 1].transform.Translate(MoveX_Z * velocity * Time.deltaTime);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plattform")
        {
            boxInPlattform = true;
            return;
        }
        inPlattform = true;
        instancePlayer = other.GetComponentInParent<Player>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plattform")
        {
            boxInPlattform = false;
            return;
        }
        inPlattform = false;
    }
}
