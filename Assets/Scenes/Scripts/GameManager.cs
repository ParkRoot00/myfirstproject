using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("#Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f; //분단위 계산하기
    [Header("#Player")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("#GameObject")]
    public Player player;
    public PoolManager pool;
    public Enemy enemy;
    public GameObject uiResult;
    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        isLive = true;
    }
    void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    public void GameOver()
    {
        GameOverRoutine();
    }
    IEnumerable GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.SetActive(true);
        Stop();
    }
    public void GameReStart()
    {
        SceneManager.LoadScene(0);
    }
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void ReStart()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
