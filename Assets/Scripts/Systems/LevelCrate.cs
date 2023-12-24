using TMPro;
using UnityEngine;

public class LevelCrate : MonoBehaviour
{
    public int level;
    public GameObject locked;
    public GameObject unlocked;
    public GameObject bronzeTrophy;
    public GameObject silverTrophy;
    public GameObject goldTrophy;

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
