using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    
    [SerializeField] GameObject player;
    [SerializeField] GameObject tanker;
    [SerializeField] GameObject soldier;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] powerUpSpawnPoints;
    [SerializeField] GameObject[] heroSpawnPoints;
    [SerializeField] GameObject healthPowerUp;
    [SerializeField] Text levelText;
    [SerializeField] Text endText;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    private const int theNumberOfEnemiesYouHaveToKillToGetAPowerUp = 3;
    private const int maxLevel = 20;

    private Vector3 cameraOffset = new Vector3(0f, 3.5f, -6.1f);

    private int enemiesKilled;
    private int currentLevel;
    private float timeBetweenSpawns = 1f;
    private float timeSinceLastSpawn;
    private bool gameOver = false;
    //private GameObject newEnemy;
    private GameObject newPowerUp;
    //private Random rdm = new Random();

    private GameObject[] enemies;

    private List<EnemyHealth> registeredEnemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    public bool GameOver
    {
        get { return gameOver; }
    }

    public GameObject Player
    {
        get { return player; }
    }

    public GameObject Arrow
    {
        get { return arrow; }
    }
    
    // Use this for initialization
    void Start () {
        heroSpawn();
        currentLevel = 1;
        enemies = new GameObject[] { tanker, ranger, soldier };
        StartCoroutine(spawn());
        enemiesKilled = 0;
        endText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastSpawn += Time.deltaTime;
    }

    public void RegisterEnemy(EnemyHealth enemy)
    {
        registeredEnemies.Add(enemy);
    }

    public void KillEnemy(EnemyHealth enemy)
    {
        killedEnemies.Add(enemy);
        enemiesKilled++;
        if (enemiesKilled % theNumberOfEnemiesYouHaveToKillToGetAPowerUp == 0)
        {
            spawnPowerUp();
        }
    }

    public void playerHit(int currentHP)
    {
        if(currentHP > 0)
        {
            gameOver = false;
        } else
        {
            gameOver = true;
            endGame();
        }
    }

    private void heroSpawn()
    {
        GameObject spawnPoint = heroSpawnPoints[Random.Range(0, heroSpawnPoints.Length)];
        player.transform.position = spawnPoint.transform.position;
        mainCamera.transform.position = player.transform.position + cameraOffset;
    }

    IEnumerator spawn()
    {
        if (timeSinceLastSpawn > timeBetweenSpawns) { 
            if(registeredEnemies.Count < currentLevel)
            {
                timeSinceLastSpawn = 0;
                GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                int rdmIndex = Random.Range(0, enemies.Length);
                print("Spawn index: " + rdmIndex);
                GameObject enemy = enemies[rdmIndex];
                //newEnemy = 
                Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(.5f);
            } else if(killedEnemies.Count >= currentLevel)
            {
                //Debug.Log("Finished Level: " + currentLevel);
                //Debug.Log("Enemies Killed this round: " + killedEnemies.Count);
                timeSinceLastSpawn = 0;
                registeredEnemies.Clear();
                killedEnemies.Clear();
                //Debug.Log("Enemies Cleared");
                //Debug.Log("Killed enemies: " + killedEnemies.Count);
                currentLevel++;
                if (currentLevel > maxLevel)
                {
                    endGame();
                    StopCoroutine(spawn());
                }
                else
                {
                    levelText.text = "Level " + currentLevel;
                    yield return new WaitForSeconds(5.5f);
                }
            }
        }
        yield return null;
        StartCoroutine(spawn());
    }

    private void spawnPowerUp()
    {
        // Pick a random spawn point
        GameObject spawnPoint = powerUpSpawnPoints[Random.Range(0, powerUpSpawnPoints.Length)];
        //newPowerUp = Instantiate(healthPowerUp);
        AudioManager.Instance.Play("Power Up");
        // spawn randomly around the chosen spawn point
        float distX = Random.Range(-4f, 4f);
        float distZ = Random.Range(-4f, 4f);
        Instantiate(healthPowerUp, spawnPoint.transform.position + new Vector3(distX, 0f, distZ), Quaternion.identity);
    }

    private void endGame()
    {
        gameOver = true;
        endText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        // If player won
        if (currentLevel > maxLevel)
        {
            endText.text = "Victory!";
        } else
        {
            endText.text = "Defeat!";
        }
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Gameplay");
    }

    public void QuitGame()
    {
        print("Quitting game...");
        Application.Quit();
    }

}
