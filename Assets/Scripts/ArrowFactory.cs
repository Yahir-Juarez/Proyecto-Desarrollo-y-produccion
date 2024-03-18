using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    [SerializeField]
    private ItemArrow arrowPrefab;
    private List<GameObject> posArrow;
    private List<GameObject> arrowInGame;
    // Start is called before the first frame update
    void Start()
    {
        posArrow = new List<GameObject>();
        arrowInGame = new List<GameObject>();
        foreach (Transform objects in transform)
        {
            posArrow.Add(objects.gameObject);
        }
        createArrows();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createArrows()
    {
        for (int i = 0; i < posArrow.Count; i++)
        {
            ItemArrow newArrow = Instantiate(arrowPrefab, posArrow[i].transform.position, Quaternion.identity);
            newArrow.transform.Rotate(90, 0, 0);
            arrowInGame.Add(newArrow.gameObject);
        }
    }
    public List<GameObject> getListArrows()
    {
        return arrowInGame;
    }

    public void clearArrows()
    {
        for (int i = arrowInGame.Count - 1; i >= 0; i--)
        {
            if (arrowInGame[i] != null)
            {
                Destroy(arrowInGame[i]);
            }
            arrowInGame.RemoveAt(i);
        }
    }
}
