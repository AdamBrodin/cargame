using UnityEngine;

//
// 2019, LBS - Bogdan Maister  
//

public class ObstacleBehavior : MonoBehaviour
{
    #region Variables

    public float speed;
    public Rigidbody rb;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = new Vector3(0, 0, speed);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
