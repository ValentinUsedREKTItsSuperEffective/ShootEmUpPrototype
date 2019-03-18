using UnityEngine;
using System.Collections.Generic;
using UniRx;
using DG.Tweening;

public class EnemyGenerator : MonoBehaviour {

    public GameObject player;
    public GameObject planet;

    public List<EnemyWave> waves;
    EnemyWave currentWave;
    int waveIndex;
    WaveInfo currentWaveInfo;

    public Subject<int> onEnemyKilled;

    int remainingEnemy;

    int spaceAngleSize;
    int halfSpaceAngleSize;
    bool[] enemyPartitionSpace;

    float respawnTimer;

	// Use this for initialization
	void Start() {
        // !BETTER NOT DOING THIS HERE!
        DOTween.Init ();

        spaceAngleSize = 7; 
        halfSpaceAngleSize = spaceAngleSize / 2; // for change range of indexes
        enemyPartitionSpace = new bool[spaceAngleSize];
        for (int i = 0; i < spaceAngleSize; i++){
            enemyPartitionSpace.SetValue (true, i);
        }

        onEnemyKilled = new Subject<int> ();
        onEnemyKilled.Subscribe (index => {
            enemyPartitionSpace[index] = true;

            if(remainingEnemy == 0){
                waveIndex++;

                if(waveIndex < waves.Count){
                    Debug.Log ("Wave : " + waveIndex);
                    InitializeNextWave ();
                }
            }
        });

        waveIndex = 0;
        InitializeNextWave ();
	}

    void Update() {
        respawnTimer += Time.deltaTime;

        while(respawnTimer > currentWaveInfo.respawnRate){
            respawnTimer -= currentWaveInfo.respawnRate;

            if(remainingEnemy > 0 && HaveSpace ()){
                Generate (1);
            }
        }
    }

    bool HaveSpace(){
        for (int i = 0; i < enemyPartitionSpace.Length; i++){
            if(enemyPartitionSpace[i] == true){
                return true;
            }
        }

        return false;
    }

    void InitializeNextWave(){
        currentWave = waves[waveIndex];
        currentWaveInfo = currentWave.infos[0];
        remainingEnemy = currentWaveInfo.number;
        respawnTimer = 0;

        Generate (currentWaveInfo.initialNumber);
    }

    void Generate(int numberOfEnnemies){
        for (int i = 0; i < numberOfEnnemies; i++) {
            GameObject enemyPivot = Instantiate (currentWaveInfo.prefab);
            enemyPivot.transform.parent = planet.transform;
            Enemy enemy = enemyPivot.transform.Find ("Enemy").GetComponent<Enemy> ();
            enemy.player = player;
            enemy.generator = this;

            int angleIndex;
            do {
                angleIndex = Random.Range (0, spaceAngleSize);
            } while (enemyPartitionSpace[angleIndex] == false);

            enemyPartitionSpace[angleIndex] = false;
            enemy.spaceIndex = angleIndex;
            enemyPivot.transform.Rotate (new Vector3 (0, 0, (angleIndex - halfSpaceAngleSize)*4));

            Vector3 finalPosition = enemy.transform.position - planet.transform.position;
            finalPosition.Normalize ();
            finalPosition *= 4f;
            enemy.PerformArrival (finalPosition);

            remainingEnemy--;
        }
    }
}
