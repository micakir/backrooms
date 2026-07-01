using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashLight;
    public float battery = 100f;
    public float drainSpeed = 4f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashLight.enabled = !flashLight.enabled;
        }

        if (flashLight.enabled)
        {
            battery -= drainSpeed * Time.deltaTime;
            if (battery <= 0)
            {
                battery = 0;
                flashLight.enabled = false;
            }
        }
    }
}
