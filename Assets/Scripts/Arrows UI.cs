using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowsUI : MonoBehaviour
{
    [SerializeField]
    private Player instancePlayer;
    private TextMeshProUGUI textArrow;
    // Start is called before the first frame update
    void Start()
    {
        textArrow = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instancePlayer != null)
        {
            textArrow.SetText(instancePlayer.getArrow().ToString());
        }
    }
}
