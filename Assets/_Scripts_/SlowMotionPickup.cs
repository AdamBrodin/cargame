#pragma warning disable CS0649
using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */ 

public class SlowMotionPickup : PickupBase
{
    #region Variables
    [SerializeField] private float slowDownTime, slowDownMultiplier;
    #endregion

    public override void Pickup()
    {
        base.Pickup();
        StartCoroutine(SlowDownEffect(slowDownTime, slowDownMultiplier));
    }

    private IEnumerator SlowDownEffect(float time, float multiplier)
    {
        Time.timeScale = slowDownMultiplier;
        float fixedStart = Time.fixedDeltaTime;
        Time.fixedDeltaTime = 0.02f * slowDownMultiplier;
        print($"Slowmoing for {time} seconds");
        yield return new WaitForSeconds(time);
        print("Regularmo");
        Time.timeScale = 1f;
        Time.fixedDeltaTime = fixedStart;
    }
}
