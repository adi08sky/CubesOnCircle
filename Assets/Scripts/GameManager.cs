using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public InitialData initialData;
    public GameObject spawnablePrefab;

    [HideInInspector]
    public int cubesCount;
    GameObject[] _cubes;
    bool _objectsAreMoving = false;
    readonly int _velocityRange = 5;

    Camera _cam;
    float _screenWidth;
    float _screenHeight;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        _cam = Camera.main;
        Vector3 coordinates = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _cam.transform.position.z*(-1)));
        _screenWidth = coordinates.x;
        _screenHeight = coordinates.y;

        SpawnCubeOnEllipse();
        AddUi();
    }

    void Update()
    {
        if (!_objectsAreMoving) MoveObjects();

        if (_objectsAreMoving)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Cube");
            CheckDestructionConditions(objects);
        }

        CheckExitGame();
    }

    /// <summary>
    /// Spawn(s) a cube/cubes in a location(s) on ellipse
    /// </summary>
    void SpawnCubeOnEllipse()
    {
        float semiMajorAxis = initialData.GetSemiMajorAxis();
        float semiMinorAxis = initialData.GetSemiMinorAxis();

        cubesCount = initialData.GetCubesCount();
        _cubes = new GameObject[cubesCount];

        float angle = 2 * Mathf.PI / cubesCount;

        for (int i = 0; i < cubesCount; i++)
        {
            float radius = GetEllipseRadius(semiMinorAxis, semiMajorAxis, angle, i);
            float y_pos = radius * Mathf.Cos(angle * i);
            float x_pos = radius * Mathf.Sin(angle * i);

            _cubes[i] = Instantiate(spawnablePrefab, new Vector3(x_pos, y_pos), Quaternion.Euler(0, 0, angle * i / Mathf.PI * -180)) as GameObject;
        }
    }

    /// <summary>
    /// Returns the radius of the ellipse at the given angle and its multiple
    /// </summary>
    /// <param name="a">Semi Major Axis Length</param>
    /// <param name="b">Semi Minor Axis Length</param>
    /// <param name="angle">Shift angle around the ellipse</param>
    /// <param name="i">Number of the shift around the ellipse by the given angle</param>
    /// <returns></returns>
    float GetEllipseRadius(float a, float b, float angle, int i)
    {
        float radius = Mathf.Pow((Mathf.Pow(a, 2) * Mathf.Pow(b, 2)
            / (Mathf.Pow(a, 2) * Mathf.Pow(Mathf.Sin(angle * i), 2) + Mathf.Pow(b, 2) * Mathf.Pow(Mathf.Cos(angle * i), 2))), 0.5f);
        return radius;
    }

    /// <summary>
    /// Move object(s) with random velocity vector
    /// </summary>
    void MoveObjects()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("MoveObjects");

            for (int i = 0; i < _cubes.Length; i++)
            {
                float random_x = Random.Range(_velocityRange * (-1), _velocityRange);
                float random_y = Random.Range(_velocityRange * (-1), _velocityRange);

                var rb = _cubes[i].GetComponent<Rigidbody>();
                rb.velocity = new Vector3(random_x, random_y, 0);
            }

            _objectsAreMoving = true;
        }
    }

    /// <summary>
    /// Destroys objects that collide with each other or fly off the screen
    /// </summary>
    /// <param name="objects">Objects to check</param>
    void CheckDestructionConditions(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].transform.position.x > _screenWidth ||
               objects[i].transform.position.x < -_screenWidth ||
               objects[i].transform.position.y > _screenHeight ||
               objects[i].transform.position.y < -_screenHeight)
            {
                Destroy(objects[i]);
                cubesCount--;
                return;
            }

            for (int j = 0; j < objects.Length; j++)
            {
                if (i == j) continue;

                bool collision = CheckCollision(objects[i].transform.position, objects[j].transform.position, 1);

                if (collision)
                {
                    Destroy(objects[i]);
                    Destroy(objects[j]);
                    cubesCount--;
                    cubesCount--;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Check the collisions of circle objects with the given diameter
    /// </summary>
    /// <param name="pos1">position of the object</param>
    /// <param name="pos2">position of the object</param>
    /// <param name="distance">distance between objects corresponding to the diameter</param>
    /// <returns></returns>
    /// We can use Vector3.Distance but it would not be a fully proprietary solution
    bool CheckCollision(Vector3 pos1, Vector3 pos2, float distance)
    {
        float deltaX = Mathf.Abs(pos1.x - pos2.x);
        float deltaY = Mathf.Abs(pos1.y - pos2.y);
        float delta = Mathf.Pow(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2), 0.5f);

        if (delta <= distance)
        {
            return true;
        }
        return false;
    }

    void CheckExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void AddUi()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
