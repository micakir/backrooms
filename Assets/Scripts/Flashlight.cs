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
    }
}
