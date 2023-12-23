using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private int currentLevel;
    [SerializeField] private float fadeDelay = 1.2f;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private int[] enemiesCountOnLevel;

    private int enemiesKilled = 0;

    private float levelStartTime;

    private void Start()
    {
        Instance = this;
        levelStartTime = Time.unscaledTime;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled == enemiesCountOnLevel[currentLevel])
        {
            // Clear level
            PlayerPrefs.SetInt("LevelProgress", currentLevel);

            float bestClearTime = PlayerPrefs.GetFloat("ClearTime_" + currentLevel);
            float currentClearTime = levelStartTime - Time.unscaledTime;
            if (currentClearTime < bestClearTime)
                PlayerPrefs.SetFloat("ClearTime_" + currentLevel, currentClearTime);

            // Load hub
            LoadLevel(0);
        }
    }

    public void RestartLevel()
    {
        cameraAnimator.SetBool("FadeIn", true);
        Invoke("LoadScene", fadeDelay);
    }

    public void LoadLevel(int level)
    {
        cameraAnimator.SetBool("FadeIn", true);
        currentLevel = level;
        Invoke("LoadScene", fadeDelay);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(currentLevel);
    }
}
