using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData       stageData;
    [SerializeField]
    private GameObject      enemyPrefab;
    [SerializeField]
    private GameObject      enemyHPSliderPrefab;
    [SerializeField]
    private Transform       canvasTransform;
    [SerializeField]
    private BGMController   bgmController;
    [SerializeField]
    private GameObject      textBossWarning;
    [SerializeField]
    private GameObject      panelBossHP;
    [SerializeField]
    private GameObject      boss;
    [SerializeField]
    private float           spawnTime;
    [SerializeField]
    private int             maxEnemyCount = 100;

    private void Awake(){
        textBossWarning.SetActive(false);
        panelBossHP.SetActive(false);
        boss.SetActive(false);
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy(){

        int currentEnemyCount = 0;
        while(true){
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y+1.0f, 0.0f), Quaternion.identity);

            SpawnEnemyHPSlider(enemyClone);

            currentEnemyCount++;

            if(currentEnemyCount == maxEnemyCount){
                StartCoroutine("SpawnBoss");
                break;
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy){
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);

        sliderClone.transform.SetParent(canvasTransform);

        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);

        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss(){
        bgmController.ChangeBGM(BGMType.Boss);
        textBossWarning.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        textBossWarning.SetActive(false);

        panelBossHP.SetActive(true);

        boss.SetActive(true);

        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
