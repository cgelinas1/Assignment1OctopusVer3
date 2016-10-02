using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    // Our enemy to spawn
    public Transform enemy;

    [Header("Wave Properties")]
  
    public float timeBeforeSpawning = 1.8f;
    public float timeBetweenEnemies = .30f;
    public float timeBeforeWaves = 2.0f;
    public int enemiesPerWave = 4;
    private int currentNumberOfEnemies = 0;

    
    private int score = 0;
    private int waveNumber = 0;
    private int lives = 5;

   
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

   
    IEnumerator SpawnEnemies()
    {
       
        yield return new WaitForSeconds(timeBeforeSpawning);

        // After timeBeforeSpawning has elapsed, we will enter this loop
        while (true)
        {
            // Don't spawn until previous enemies are dead
            if (currentNumberOfEnemies <= 0)
            {
                waveNumber++;
                

                //Spawn 10 enemies in a random position
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    // enemies off screen
                    float randDistance = Random.Range(10, 25);

                    // Enemies can come from any direction
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position;

                    
                    enemyPos.x += randDirection.x * randDistance;
                    enemyPos.y += randDirection.y * randDistance;

                    
                    Instantiate(enemy, enemyPos, this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            // time before checking for new spawn
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    // Allows outside class to know when enemy killed
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        //scoreText.text = "Score: " + score;
    }

    public void DecreaseLives(int decrease)
    {
        lives -= decrease;
        //livesText.text = "Lives: " + lives;
    }
}
