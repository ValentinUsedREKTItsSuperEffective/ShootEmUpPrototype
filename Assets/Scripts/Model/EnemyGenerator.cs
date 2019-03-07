using UnityEngine;
using UniRx;

public class EnemyGenerator : MonoBehaviour {

    public GameObject player;
    public GameObject planet;

    public EnemyWave wave;

    public Subject<int> onEnemyKilled;

    int remainingEnemy;

    int spaceAngleSize;
    int halfSpaceAngleSize;
    bool[] enemyPartitionSpace;

	// Use this for initialization
	void Start() {
        remainingEnemy = 0;

        spaceAngleSize = 6; 
        halfSpaceAngleSize = spaceAngleSize / 2; // for change range of indexes
        enemyPartitionSpace = new bool[spaceAngleSize];
        for (int i = 0; i < spaceAngleSize; i++){
            enemyPartitionSpace.SetValue (true, i);
        }

        onEnemyKilled = new Subject<int> ();
        onEnemyKilled.Subscribe (index => {
            remainingEnemy--;
            enemyPartitionSpace[index] = true;

            if(remainingEnemy == 0) {
                Generate ();
            }
        });

        Generate ();
	}

    void Generate(){
        foreach(WaveInfo info in wave.enemies){ // don't use foreach, choose a type of wave instead
            remainingEnemy = info.number;

            for (int i = 0; i < info.number; i++) {
                GameObject enemyPivot = Instantiate (info.prefab);
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
            }
        }
    }
}
