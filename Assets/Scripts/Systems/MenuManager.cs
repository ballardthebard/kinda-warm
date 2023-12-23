using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager Instance;

    [SerializeField] private InputActionProperty pause;
    [SerializeField] private GameObject menuHolder;
    [SerializeField] private float menuHeight;

    private Transform anchor;
    private Transform cameraTransform;
    private bool isPaused = false;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (pause.action.WasPressedThisFrame())
        {
            // Open Menu
            isPaused = !isPaused;
            menuHolder.SetActive(isPaused);

            // Tell time manager if it should update
            if (TimeManager.Instance != null)
                TimeManager.Instance.isPaused = isPaused;

            if (isPaused)
            {
                Time.timeScale = 0;

                // Update position 
                transform.position = new Vector3(anchor.position.x, menuHeight, anchor.position.z);

                // Get the direction to the camera
                Vector3 directionToTarget = cameraTransform.position - transform.position;
                directionToTarget.y = 0f; // Set the Y component to zero to only rotate on the Y-axis

                if (directionToTarget != Vector3.zero)
                {
                    // Rotate the object towards the camera
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    transform.rotation = targetRotation;
                }
            }
            else
                Time.timeScale = 1;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Get new anchor and cameraTransform
        anchor = GameObject.FindGameObjectWithTag("MenuAnchor").GetComponent<Transform>();
        cameraTransform = anchor.parent.transform;
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(LevelManager.Instance.currentLevel);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteKey("LevelProgress");
        PlayerPrefs.DeleteKey("ClearTime_1");
        PlayerPrefs.DeleteKey("ClearTime_2");
        PlayerPrefs.DeleteKey("ClearTime_3");
    }
}
