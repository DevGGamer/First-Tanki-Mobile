using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject boomEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.GetDamage(damage);
            GameObject clone = Instantiate(boomEffect, other.transform);
            Destroy(clone, 3f);
            Destroy(gameObject);
        }
    }
}
