using TMPro;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class LevelCrate : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private float goldenClear;
    [SerializeField] private float silverClear;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject unlocked;
    [SerializeField] private GameObject bronzeTrophy;
    [SerializeField] private GameObject silverTrophy;
    [SerializeField] private GameObject goldTrophy;

    private void Start()
    {
        float highestClearedLevel = PlayerPrefs.GetInt("LevelProgress");

        // Enable icons based on player achivements
        if (level == highestClearedLevel + 1)
        {
            unlocked.SetActive(true);
        }
        else if (level > highestClearedLevel)
        {
            locked.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }
        else if (level <= highestClearedLevel)
        {
            float clearTime = PlayerPrefs.GetFloat("ClearTime_" + level);
            if (clearTime <= goldenClear)
            {
                goldTrophy.SetActive(true);
            }
            else if (clearTime <= silverClear)
            {
                silverTrophy.SetActive(true);
            }
            else
            {
                bronzeTrophy.SetActive(true);
            }
        }
    }

    public void SetClearTime(TMP_Text text)
    {
        float clearTime = PlayerPrefs.GetFloat("ClearTime_" + level);
        text.text = clearTime.ToString("0.0") + "s";
    }

    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
