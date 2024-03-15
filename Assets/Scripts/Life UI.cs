using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField]
    private Player instancePlayer;
    private TextMeshProUGUI textLife;
    // Start is called before the first frame update
    void Start()
    {
        textLife = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instancePlayer != null)
        {
            textLife.SetText(instancePlayer.getLife().ToString());
        }
    }
}
