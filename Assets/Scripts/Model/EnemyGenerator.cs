using UnityEngine;
using UniRx;

public class EnemyGenerator : MonoBehaviour {

    public GameObject player;
    public GameObject planet;

    public EnemyWave wave;

    public Subject<Unit> onEnemyKilled;

    private int remainingEnemy;

	// Use this for initialization
	void Start () {
        remainingEnemy = 0;

        onEnemyKilled = new Subject<Unit> ();
        onEnemyKilled.Subscribe (_ => {
            remainingEnemy--;

            if(remainingEnemy == 0) {
                Generate ();
            }
        });
        Generate ();
	}

    void Generate(){
        foreach(WaveInfo info in wave.enemies){ // don't use foreach, choose a type of wave instead
            remainingEnemy = info.number;

            for (int i = 0; i < info.number; i++){
                GameObject enemyPivot = Instantiate (info.prefab);
                enemyPivot.transform.parent = planet.transform;
                Enemy enemy = enemyPivot.transform.Find ("Enemy").GetComponent<Enemy> ();
                enemy.player = player;
                enemy.generator = this;
                enemyPivot.transform.Rotate (new Vector3 (0, 0, Random.Range (-12, 12)));
            }
        }
    }
}
