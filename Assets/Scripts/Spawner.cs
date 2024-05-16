using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer;
    private GameObject obj;

    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private GameObject[] objects;

    private void Update()
    {
        if (!obj)
        {
            timer += Time.deltaTime;
            if (timer>=spawnTime)
            {
                timer = 0;
                obj = objects[Random.Range(0, objects.Length)];
                obj = Instantiate(obj, transform.position, obj.transform.rotation);
            }
        }
    }
}
