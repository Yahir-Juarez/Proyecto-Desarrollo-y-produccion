using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class TestWindow : EditorWindow
{
    string ObjectToFind = "Nothing...";

    public float sliderValue = 0.0f;
    public float minValue = 0.0f;
    public float maxValue = 100.0f;

    [MenuItem("Tool/Cheats")]
    public static void ShowWindow()
    {
        GetWindow<TestWindow>("Cheats");
    }
    int LifeCheats = 1;
    int ArrowsCheats = 1;
    float WalkCheats = 1;
    float RunCheats = 1;
    int spacing = 2;

    [MenuItem("Examples/Editor GUILayout IntSlider usage")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(TestWindow));
        window.Show();
    }

    /// <summary>
    /// Clone Enemys Example////////////////////////////////////////
    /// </summary>
    void CloneSelected()
    {
        if (!Selection.activeGameObject)
        {
            Debug.LogError("Select a GameObject first");
            return;
        }

        for (int i = 0; i < LifeCheats; i++)
            for (int j = 0; j < WalkCheats; j++)
                for (int k = 0; k < RunCheats; k++)
                    Instantiate(Selection.activeGameObject, new Vector3(i, j, k) * spacing, Selection.activeGameObject.transform.rotation);
    }
    //////////////////////////////////////////////////////////////
    private void OnGUI()
    {
        GUILayout.Label("Searh Window", EditorStyles.boldLabel);
        ObjectToFind = EditorGUILayout.TextArea(ObjectToFind);
        if (GUILayout.Button("Search in Scene"))
        {
            GameObject foundObject = GameObject.Find(ObjectToFind);
            if (foundObject != null)
            {
                Selection.activeGameObject = foundObject;
                EditorGUIUtility.PingObject(foundObject);
            }
            else
            {
                Debug.LogWarning(string.Format("Could not find in Scene [{0}]", ObjectToFind));
            }
        }
        GUILayout.Label("Life", EditorStyles.boldLabel);
        LifeCheats = EditorGUILayout.IntSlider(LifeCheats, 0, 99);
        GUILayout.Label("Arrows", EditorStyles.boldLabel);
        ArrowsCheats = EditorGUILayout.IntSlider(ArrowsCheats, 0, 99);
        GUILayout.Label("Walk Speed", EditorStyles.boldLabel);
        WalkCheats = EditorGUILayout.Slider(WalkCheats, 0, maxValue);
        GUILayout.Label("Run Speed", EditorStyles.boldLabel);
        RunCheats = EditorGUILayout.Slider(RunCheats, 0, maxValue);

        if (GUILayout.Button("Aplicate New Value"))
        {
            aplicateValue();
        }

        void aplicateValue()
        {
            GameManager.Instance.GetPlayer().setLife(LifeCheats);
            GameManager.Instance.GetPlayer().setArrowsCheats(ArrowsCheats);
            GameManager.Instance.GetPlayer().runState.setSpeed(RunCheats);
            GameManager.Instance.GetPlayer().walkState.setSpeed(WalkCheats);
        }
        //GUILayout.Label("Searh Windowss", EditorStyles.boldLabel);
        //myIntValue = EditorGUILayout.TextArea(myIntValue);
        //if (GUILayout.Button("Search in Scenesdsd"))
        //{
        //    GameObject foundObject = GameObject.Find(ObjectToFind);
        //    if (foundObject != null)
        //    {
        //        Selection.activeGameObject = foundObject;
        //        EditorGUIUtility.PingObject(foundObject);
        //    }
        //    else
        //    {
        //        Debug.LogWarning(string.Format("Could not find in Scene [{0}]", ObjectToFind));
        //    }
        //}

        //GUILayout.BeginArea(new Rect(10, 50, 200, 100));
        //GUILayout.Label("Slider Value: " + sliderValue.ToString("F2")); // Muestra el valor actual del slider
        //sliderValue = GUILayout.HorizontalSlider(sliderValue, minValue, maxValue); // Crea el slider horizontal
        //GUILayout.EndArea();
        //// Comienza un área de diseño automático
        //GUILayout.BeginArea(new Rect(10, 10, 200, 150));

        //// Crea un botón con GUILayout
        //if (GUILayout.Button("Presiona Me"))
        //{
        //    Debug.Log("¡Botón presionado!");
        //}

        //// Crea una etiqueta con GUILayout
        //GUILayout.Label("Esto es una etiqueta");

        //// Crea un campo de texto con GUILayout
        //// Puedes usar GUILayout.TextField para la entrada de texto
        //GUILayout.Label("Nombre:");
        //string nombre = GUILayout.TextField("Ingrese su nombre");

        //// Finaliza el área de diseño automático
        //GUILayout.EndArea();

        //// Acciones basadas en la entrada del usuario
        //if (nombre != "Ingrese su nombre")
        //{
        //    Debug.Log("Hola, " + nombre + "!");
        //}
    }

}
#endif