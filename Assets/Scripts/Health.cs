using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    private float currHP;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool isBot;
    [Header("Bot")]
    [SerializeField] private HPBar hpBar;
    [SerializeField] private bool isFriendly;
    [Header("Player")]
    [SerializeField] private Slider slider;
    [SerializeField] private Text hpText;

    private EnemyControl[] bots;
    private List<EnemyControl> listBots = new List<EnemyControl>();

    private void Start()
    {
        currHP = maxHealth;
        bots = FindObjectsOfType<EnemyControl>();
        foreach (EnemyControl bot in bots)
        {
            listBots.Add(bot);
        }
        if (!isBot)
        {
            slider.value = currHP/maxHealth;
            hpText.text = currHP.ToString();
        }
    }

    public void GetDamage(int damage)
    {
        currHP = Mathf.Clamp(currHP - damage, 0, maxHealth);

        if (isBot)
        {
            hpBar.ChangeValue(currHP/maxHealth);
            if (currHP == 0)
            {
                if (isFriendly)
                {
                    foreach(EnemyControl enemy in listBots)
                    {
                        if (enemy.isFriendly == false)
                        {
                            enemy.RemoveFromList(gameObject);
                        }
                    }
                    GameManager.instance.EnemyKill();
                }
                else
                {
                    foreach(EnemyControl enemy in listBots)
                    {
                        if (enemy.isFriendly == true)
                        {
                            enemy.RemoveFromList(gameObject);
                        }
                    }
                    GameManager.instance.FriendKill();
                }
                Destroy(gameObject, 5f);
                GetComponent<Animator>().SetTrigger("Dead");
                Destroy(GetComponent<EnemyControl>());
                Destroy(GetComponent<NavMeshAgent>());
                Destroy(GetComponent<Collider>());
                listBots.Remove(GetComponent<EnemyControl>());
            }
        }
        else
        {
            slider.value = currHP/maxHealth;
            hpText.text = currHP.ToString();
            if (currHP==0)
            {
                GetComponent<Animator>().SetTrigger("Dead");
                Destroy(GetComponent<TankController>());
                Destroy(GetComponent<Collider>());
                GameManager.instance.GameOver();
            }
        }
    }
}
