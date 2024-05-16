using UnityEngine;

public class SkyControl : MonoBehaviour
{
    private Light dirLight;
    private float startIntensity;
    [SerializeField] private float speed = 10f;

    private void Start()
    {
        dirLight = GetComponent<Light>();
        startIntensity = dirLight.intensity;
    }

    private void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
        dirLight.intensity = transform.rotation.eulerAngles.x < 180 ? startIntensity : 0; 
    }
}
