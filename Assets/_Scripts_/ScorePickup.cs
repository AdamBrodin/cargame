#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class ScorePickup : PickupBase
{
    #region Variables
    [SerializeField] private float scoreBlinkTime;
    [SerializeField] private int minReward, maxReward;
    [SerializeField] private Color scoreBlinkColor;
    #endregion

    public override void Pickup()
    {
        base.Pickup();
        int reward = Random.Range(minReward, maxReward);
        ScoreSystem.Instance.IncreaseScore(reward);
        StartCoroutine(ScoreSystem.Instance.ScoreRewardEffect(scoreBlinkColor, scoreBlinkTime, reward, transform.position));
    }
}
