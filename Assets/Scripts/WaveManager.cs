using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] int currentWaveIndex = 0;

    [SerializeField] List<EnemyWave> enemyWaves;

    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] float initialWaveDelay;

    [SerializeField] Image clockImage;

    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] bool autoStartNextWave,loopWaves;

    float duration;

    float counter = 0;

    bool isLastWaveEnded;

    private void Awake()
    {
        duration = initialWaveDelay;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetWaveText();

        if (currentWaveIndex >= enemyWaves.Count)
        {
            if(enemySpawner.IsSpawning == true)
            {
                CheckLastWaveEnd();
                return;
            }

            CheckLastWaveEnd();

            if (isLastWaveEnded == true)
            {
                if (loopWaves)
                {
                    currentWaveIndex = 0;
                    return;
                }
                GameManager.Instance.OnGameWin();
            }


            return;
        }

        if (autoStartNextWave && enemySpawner.IsSpawning == false && enemyWaves[currentWaveIndex].duration - counter > 15f)
        {
            counter = enemyWaves[currentWaveIndex].duration - 15f;
        }

        counter += Time.deltaTime;


        if (currentWaveIndex == 0 && counter >= initialWaveDelay)
        {
            enemySpawner.StartEnemySpawn(enemyWaves[currentWaveIndex]);

            currentWaveIndex++;

            duration = enemyWaves[currentWaveIndex].duration;

            counter = 0;

        }
        else if (counter >= enemyWaves[currentWaveIndex].duration)
        {
            MoneyManager.Instance.AddMoney(enemyWaves[currentWaveIndex - 1].prizeMoney); // this causes last wave to doesn't give money after finished which is valid

            enemySpawner.StartEnemySpawn(enemyWaves[currentWaveIndex]);

            currentWaveIndex++;

            if (currentWaveIndex >= enemyWaves.Count)
            {
                clockImage.fillAmount = 1f;
                return;
            }

            duration = enemyWaves[currentWaveIndex].duration;

            counter = 0;
        }

        SetClockImage();
    }

    void SetClockImage()
    {
        clockImage.fillAmount = counter / duration;
    }

    void SetWaveText()
    {
        waveText.text = "Wave " + (currentWaveIndex);
    }

    public void SkipWaveWait()
    {
        if (currentWaveIndex >= enemyWaves.Count)
            return;

        if(currentWaveIndex == 0)
        {
            counter = initialWaveDelay;
            return;
        }

        if(enemySpawner.IsSpawning == false)
            counter = enemyWaves[currentWaveIndex].duration;
    }

    public void CheckLastWaveEnd()
    {
        if (PlayerHealthHandler.Instance.Health <= 0)
            return;

        if(enemySpawner.IsSpawning == false)
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);

            if(enemies.Length == 0)
            {
                isLastWaveEnded = true;
            }
        }
    }

}
