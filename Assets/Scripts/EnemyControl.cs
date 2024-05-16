using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private Transform target;
    private GameObject[] player;
    private List<GameObject> listPlayers = new List<GameObject>();
    private GameObject[] points;
    private NavMeshAgent agent;
    private Transform enemyTarget;
    private Animator anim;

    [SerializeField] private string pointsTag = "point";
    [SerializeField] private string targetTag;
    [SerializeField] private float angryDist = 30f;
    [SerializeField] private float nearDist = 5f;
    public bool isFriendly;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectsWithTag(targetTag);
        points = GameObject.FindGameObjectsWithTag(pointsTag);
        target = points[Random.Range(0, points.Length)].transform;
        anim = GetComponent<Animator>();
        foreach (GameObject enemy in player)
        {
            listPlayers.Add(enemy);
        }
    }

    private void Update()
    {
        UpdateTarget();
        if (enemyTarget.gameObject == null) return;
        if (Vector3.Distance(transform.position, enemyTarget.position) > angryDist)
        {
            if (Vector3.Distance(transform.position, target.position) < nearDist)
            {
               target = points[Random.Range(0, points.Length)].transform; 
            }
            agent.SetDestination(target.position);
            anim.SetBool("isAngry", false);
        }
        else
        {
            agent.SetDestination(enemyTarget.position);
            GetComponent<Shooting>().Shoot();
            anim.SetBool("isAngry", true);
        }
    }

    private void UpdateTarget()
    {
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in listPlayers)
        {
            float DistanceToTarget = Vector3.Distance(transform.position, enemy.transform.position);
            if (DistanceToTarget < minDistance)
            {
                minDistance = DistanceToTarget;
                enemyTarget = enemy.transform;
            }
        }
    }

    public void RemoveFromList(GameObject bot)
    {
        listPlayers.Remove(bot);
    }
}
