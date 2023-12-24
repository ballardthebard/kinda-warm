using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    [SerializeField] private LevelCrate[] crates;

    private int highestClearedLevel;

    private void Start()
    {
        highestClearedLevel = PlayerPrefs.GetInt("LevelProgress");

        // Enable icons based on player achivements
        foreach (LevelCrate level in crates)
        {
            if (level.level == highestClearedLevel + 1)
            {
                level.unlocked.SetActive(true);
            }
            else if (level.level > highestClearedLevel)
            {
                level.locked.SetActive(true);
                level.GetComponent<Collider>().enabled = false;
            }
            else if (level.level <= highestClearedLevel)
            {
                float clearTime = PlayerPrefs.GetFloat("ClearTime_" + level.level);
                if (clearTime <= 60f)
                {
                    level.goldTrophy.SetActive(true);
                }
                else if (clearTime <= 120f)
                {
                    level.silverTrophy.SetActive(true);
                }
                else
                {
                    level.bronzeTrophy.SetActive(true);
                }
            }
        }
    }

    public void LoadLevel(LevelCrate crate)
    {
        if (crate.level <= highestClearedLevel + 1)
        {
            LevelManager.Instance.LoadLevel(crate.level);
        }
    }
}
