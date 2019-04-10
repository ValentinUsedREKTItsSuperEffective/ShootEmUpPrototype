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

    int remainingSpawn;
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
            remainingEnemy--;
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

            if(remainingSpawn > 0 && HaveSpace ()){
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
        remainingSpawn = remainingEnemy = currentWaveInfo.number;
        respawnTimer = 0;

        // TODO : Generate enemy of each type
        Generate (currentWaveInfo.initialNumber);
    }

    // TODO : Rendre la fonction Generate propre à la classe Enemy + fonction abstraite, chaque classe fille réecrit sa facon de s'instancier
    // TODO : Generer un ennemi de chaque type
    void Generate(int numberOfEnnemies){
        for (int i = 0; i < numberOfEnnemies; i++) {
            Shooter shooter = Instantiate (currentWaveInfo.prefab).GetComponent<Shooter>();
            shooter.transform.parent = planet.transform;
            shooter.generator = this;

            int angleIndex;
            do {
                angleIndex = Random.Range (0, spaceAngleSize);
            } while (enemyPartitionSpace[angleIndex] == false);

            enemyPartitionSpace[angleIndex] = false;
            shooter.spaceIndex = angleIndex;
            shooter.transform.RotateAround (planet.transform.position, new Vector3 (0, 0, 1), (angleIndex - halfSpaceAngleSize) * 4);

            Vector3 finalPosition = shooter.transform.position - planet.transform.position;
            finalPosition.Normalize ();
            finalPosition *= 4f;
            shooter.PerformArrival (finalPosition);

            remainingSpawn--;
        }
    }
}
