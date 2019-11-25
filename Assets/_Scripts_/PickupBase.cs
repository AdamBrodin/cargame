#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PickupBase : MonoBehaviour, IPickupable
{
    #region Variables
    public string PickupSoundName => pickupSoundName;
    [SerializeField] private string pickupSoundName;
    #endregion

    public virtual void Pickup()
    {
        Destroy(gameObject);
        AudioManager.Instance?.SetState(pickupSoundName, true);
    }
}
