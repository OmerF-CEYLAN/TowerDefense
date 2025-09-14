using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    int currentWaveIndex = 0;

    [SerializeField] List<EnemyWave> enemyWaves;

    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] float initialWaveDelay;

    [SerializeField] Image clockImage;

    [SerializeField] TextMeshProUGUI waveText;

    float duration;

    float counter = 0;

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
            return;  

        counter += Time.deltaTime;

        //if (currentWaveIndex > 0 && FindAnyObjectByType<Enemy>() == null)
        //    counter = enemyWaves[currentWaveIndex].duration; E�ER SK�P BASILIRSA BU TARZDA B�R KOD �ALI�ACAK

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

        counter = enemyWaves[currentWaveIndex].duration;
    }

}
