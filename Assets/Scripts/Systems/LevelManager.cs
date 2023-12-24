using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int currentLevel;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private float fadeDelay = 1.2f;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private int enemiesCountOnLevel;

    private int enemiesKilled = 0;

    private float levelStartTime;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        levelStartTime = Time.unscaledTime;
        SoundManager.Instance.PlayMusic(levelMusic);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled == enemiesCountOnLevel)
        {
            // Update player prefs and unlock next level
            int currentProgress = PlayerPrefs.GetInt("LevelProgress");
            if (currentProgress < currentLevel)
                PlayerPrefs.SetInt("LevelProgress", currentLevel);

            // Set record clear time for this level
            float bestClearTime = PlayerPrefs.GetFloat("ClearTime_" + currentLevel);
            float currentClearTime = Time.unscaledTime - levelStartTime;
            if (currentClearTime < bestClearTime || bestClearTime == 0)
                PlayerPrefs.SetFloat("ClearTime_" + currentLevel, currentClearTime);

            PlayerPrefs.Save();

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
