using UnityEngine;
using UniRx;
using DG.Tweening;

public class EnemyGenerator : MonoBehaviour {

    public GameObject player;
    public GameObject planet;

    public EnemyWave wave;
    WaveInfo currentWave;

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

        respawnTimer = 0;

        remainingEnemy = 0;

        spaceAngleSize = 7; 
        halfSpaceAngleSize = spaceAngleSize / 2; // for change range of indexes
        enemyPartitionSpace = new bool[spaceAngleSize];
        for (int i = 0; i < spaceAngleSize; i++){
            enemyPartitionSpace.SetValue (true, i);
        }

        onEnemyKilled = new Subject<int> ();
        onEnemyKilled.Subscribe (index => {
            enemyPartitionSpace[index] = true;

            // if remaining ennemies == 0 go to the next wave
        });

        if (currentWave == null) {
            currentWave = wave.enemies[0];
            remainingEnemy = currentWave.number;
        }

        Generate (currentWave.initialNumber);
	}

    void Update() {
        respawnTimer += Time.deltaTime;

        while(respawnTimer > currentWave.respawnRate){
            respawnTimer -= currentWave.respawnRate;

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

    void Generate(int firstSpawn){
        for (int i = 0; i < firstSpawn; i++) {
            GameObject enemyPivot = Instantiate (currentWave.prefab);
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
