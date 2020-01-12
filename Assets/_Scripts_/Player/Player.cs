#pragma warning disable CS0649
using System;
using UnityEngine;
using System.Collections;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour
{
    #region Singleton
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    #endregion

    #region Variables
    public Action<int> OnPlayerDeath;
    public Action OnItemPickup;
    public bool isDead;
    #endregion

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Obstacle":
                KillPlayer();
                break;
            case "Desert":
                KillPlayer();
                break;
            case "Pickup":
                col.gameObject.GetComponent<IPickupable>()?.Pickup();
                OnItemPickup?.Invoke();
                break;
        }
    }

    public void KillPlayer()
    {
        OnPlayerDeath?.Invoke(ScoreSystem.Instance.currentScore);
        StartCoroutine(WaitAndDie());
    }

    private IEnumerator WaitAndDie()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(1);
        Time.timeScale = 0.00001f;
        isDead = true;
    }
}