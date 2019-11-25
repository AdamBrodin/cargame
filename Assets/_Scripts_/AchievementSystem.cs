#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[System.Serializable]
public class Achievement
{
    public enum AchievementType
    {
        TotalPlayerDeaths,
        TotalScoreEarned,
        TotalItemPickups
    }
    public AchievementType type;
    public bool isUnlocked;
    public int valueToUnlock;
    public string achievementTitle, achievementMessage;
}

public class AchievementSystem : MonoBehaviour
{
    #region Variables
    [SerializeField] private Achievement[] achievements;
    private int TotalPlayerDeaths
    {
        get
        {
            return PlayerPrefs.GetInt("TOTAL_PLAYER_DEATHS", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TOTAL_PLAYER_DEATHS", value);
            PlayerPrefs.Save();
        }
    }
    private int TotalScoreEarned
    {
        get
        {
            return PlayerPrefs.GetInt("TOTAL_SCORE_EARNED", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TOTAL_SCORE_EARNED", value);
            PlayerPrefs.Save();
        }
    }
    private int TotalItemPickups
    {
        get
        {
            return PlayerPrefs.GetInt("TOTAL_ITEM_PICKUPS", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TOTAL_ITEM_PICKUPS", value);
            PlayerPrefs.Save();
        }
    }
    #endregion

    private void Start()
    {
        Player.Instance.OnPlayerDeath += OnPlayerDeath;
        Player.Instance.OnItemPickup += OnItemPickup;
    }

    private void OnPlayerDeath(int score)
    {
        TotalPlayerDeaths++;
        TotalScoreEarned += score;

        CheckForUnlock(Achievement.AchievementType.TotalPlayerDeaths, TotalPlayerDeaths);
        CheckForUnlock(Achievement.AchievementType.TotalScoreEarned, TotalScoreEarned);
    }

    private void OnItemPickup()
    {
        TotalItemPickups++;
        CheckForUnlock(Achievement.AchievementType.TotalItemPickups, TotalItemPickups);
    }

    private void CheckForUnlock(Achievement.AchievementType selectedType, int inputValue)
    {
        foreach (Achievement ach in achievements)
        {
            if (ach.type == selectedType)
            {
                if (!ach.isUnlocked && inputValue >= ach.valueToUnlock)
                {
                    UnlockAchievement(ach);
                }
            }
        }
    }

    private void UnlockAchievement(Achievement achievement)
    {
        achievement.isUnlocked = true;
        print($"Achievement Unlocked, {achievement.achievementTitle}: {achievement.achievementMessage}");
    }
}
