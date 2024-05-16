using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int KillPointsEnemy, KillPointsFriend;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject deathPanel;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        scoreText.text = $"{KillPointsFriend} : {KillPointsEnemy}";
        Time.timeScale = 1f;
    }

    public void FriendKill()
    {
        KillPointsFriend++;
        scoreText.text = $"{KillPointsFriend} : {KillPointsEnemy}";
    }

    public void EnemyKill()
    {
        KillPointsEnemy++;
        scoreText.text = $"{KillPointsFriend} : {KillPointsEnemy}";
    }

    public void GameOver()
    {
        StartCoroutine(ActivePanel());
    }

    IEnumerator ActivePanel()
    {
        yield return new WaitForSeconds(5f);
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
