using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    private float timer = 0;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioSource audio;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private Image cdImage;

    private void Start()
    {
        shootEffect.Stop();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (gameObject.CompareTag("Friend") && cdImage)
            cdImage.fillAmount = 1 - Mathf.Clamp01(timer/cooldown);
    }

    public void Shoot()
    {
        if (timer > cooldown)
        {
            timer = 0;
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            audio.Play();
            shootEffect.Play();
        }
    }
}
