using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Transform Enemy;

    [Header("Oleadas de enemigos")]
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = 0.25f;
    public float timeBetweenWaves = 2.0f;

    public int enemiesPerWave = 10;

    private int currentNumberOfEnemies = 0;

    [Header("User Interface")]
    private int score = 0;
    private int wave = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        if(!PauseMenuBehavior.isPaused) //si no estamos en el menu de pausa
        {
            //1.5 segundos de espera en esta linea
            yield return new WaitForSeconds(timeBeforeSpawning);

            while (true)
            {
                if (currentNumberOfEnemies <= 0)
                {
                    IncreaseWave();
                    for (int i = 0; i < enemiesPerWave; i++)
                    {

                        float randDistance = Random.Range(10, 25);
                        Vector2 randDirection = Random.insideUnitCircle; //te devuelve un punto cualquiera de la circunferencia de un circulo unidad

                        Vector3 enemyPos = this.transform.position; //0,0,0

                        enemyPos.x += randDirection.x * randDistance; //El punto random de esa circunferencia lo multiplicamos por el randDistance con lo que pasa de ser un punto de una circunferencia de radio 1 a una de 10 a 25 segun el random
                        enemyPos.y += randDirection.y * randDistance;

                        Instantiate(Enemy, enemyPos, this.transform.rotation);
                        currentNumberOfEnemies++;

                        //0.25 segundos de espera en esta linea
                        yield return new WaitForSeconds(timeBetweenEnemies);
                    }
                }

                //Quedan enemigos aun y por tanto hacemos se espere en esta linea 2 segundos
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        
    }

    public void KillEnemy()
    {
        currentNumberOfEnemies--;
    }


    public void IncreaseScore(int increaseScore)
    {
        score += increaseScore;
        scoreText.text = "Score : " + score;

    }

    private void IncreaseWave()
    {
        wave++;
        waveText.text = "Wave : " + wave;
    }
}
