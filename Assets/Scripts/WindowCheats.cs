using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class TestWindow : EditorWindow
{
    string ObjectToFind = "Nothing...";
    string EnemyInGame = "Enemy...";
    public float sliderValue = 0.0f;
    public float minValue = 0.0f;
    public float maxValue = 100.0f;

    int LifeCheats = 1;
    int ArrowsCheats = 1;
    float WalkCheats = 1;
    float RunCheats = 1;
    int spacing = 2;

    float posEnemyX = 0.0f;
    float posEnemyY = 0.0f;
    int typeEnemy = 1;

    float posPlayerX = 0.0f;
    float posPlayerY = 0.0f;

    float spawnPlayerX = 0.0f;
    float spawnPlayerY = 0.0f;
    [MenuItem("Tool/Cheats")]
    public static void ShowWindow()
    {
        GetWindow<TestWindow>("Cheats");
    }

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
        GUILayout.Label("State Player", EditorStyles.boldLabel);
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
        if (GUILayout.Button("Reset Value"))
        {
            GameManager.Instance.GetPlayer().resetValue();
        }

        GUILayout.Label("Delete Enemy", EditorStyles.boldLabel);
        EnemyInGame = EditorGUILayout.TextArea(EnemyInGame);
        if (GUILayout.Button("Delete Enemy"))
        {
            GameObject foundObject = GameObject.Find(EnemyInGame);
            if (foundObject != null && foundObject.tag == "Enemy")
            {
                deleteEnemy(foundObject);
                EnemyInGame = "Enemy...";
            }
            else
            {
                Debug.LogWarning(string.Format("Could not eliminate enemy [{0}]", EnemyInGame));
            }
        }
        if (GUILayout.Button("Clear All Enemies"))
        {
            if (GameManager.Instance.GetEnemyFactory().getListEnemy().Count != 0)
            {
                deleteEnemy();
            }
            else
            {
                Debug.LogWarning(string.Format("List enemy empty"));
            }
        }
        GUILayout.Label("Create Enemy In Game", EditorStyles.boldLabel);
        posEnemyX = EditorGUILayout.FloatField("Enemy pos in X:", posEnemyX);
        posEnemyY = EditorGUILayout.FloatField("Enemy pos in Y:", posEnemyY);
        GUILayout.Label("Enemy1[1], Enemy2[2], ADC[3]", EditorStyles.boldLabel);
        typeEnemy = EditorGUILayout.IntSlider(typeEnemy, 1, 3);

        if (GUILayout.Button("Create Enemy"))
        {
            GameManager.Instance.GetEnemyFactory().AddEnemy(posEnemyX, posEnemyY, typeEnemy);
        }

        GUILayout.Label("Teleport Player", EditorStyles.boldLabel);
        posPlayerX = EditorGUILayout.FloatField("Player pos in X:", posPlayerX);
        posPlayerY = EditorGUILayout.FloatField("Player pos in Y:", posPlayerY);

        if (GUILayout.Button("Teleport"))
        {
            GameManager.Instance.GetPlayer().setPosPlayer(posPlayerX, posPlayerY);
        }

        GUILayout.Label("Spawn Player", EditorStyles.boldLabel);
        spawnPlayerX = EditorGUILayout.FloatField("Player pos in X:", spawnPlayerX);
        spawnPlayerY = EditorGUILayout.FloatField("Player pos in Y:", spawnPlayerY);

        if (GUILayout.Button("Create new Spawn"))
        {
            GameManager.Instance.GetPlayer().setSpawn(new Vector3(spawnPlayerX, spawnPlayerY, 0));
        }
        if (GUILayout.Button("Reset Level"))
        {
            GameManager.Instance.resetLevel();
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
    void aplicateValue()
    {
        GameManager.Instance.GetPlayer().setLife(LifeCheats);
        GameManager.Instance.GetPlayer().setArrowsCheats(ArrowsCheats);
        GameManager.Instance.GetPlayer().runState.setSpeed(RunCheats);
        GameManager.Instance.GetPlayer().walkState.setSpeed(WalkCheats);
    }

    void deleteEnemy(GameObject enemyObject)
    {
        for (int i = 0; i < GameManager.Instance.GetEnemyFactory().getListEnemy().Count; i++)
        {
            if (GameManager.Instance.GetEnemyFactory().getListEnemy()[i] == enemyObject)
            {
                Destroy(GameManager.Instance.GetEnemyFactory().getListEnemy()[i]);
                GameManager.Instance.GetEnemyFactory().getListEnemy().RemoveAt(i);
            }
        }
    }

    void deleteEnemy()
    {
        for (int i = GameManager.Instance.GetEnemyFactory().getListEnemy().Count - 1; i >= 0; i--)
        {
            Destroy(GameManager.Instance.GetEnemyFactory().getListEnemy()[i]);
            GameManager.Instance.GetEnemyFactory().getListEnemy().RemoveAt(i);
        }
    }
}
#endif