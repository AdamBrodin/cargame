using UnityEngine;

public class Body : MonoBehaviour
{
    float accelx, accely, accelz = 0;

    void Update()
    {
        accelx = Input.acceleration.x;
        accely = Input.acceleration.y;
        accelz = Input.acceleration.z;
        transform.Rotate(accelx * Time.deltaTime, accely * Time.deltaTime, accelz * Time.deltaTime);
    }
}
