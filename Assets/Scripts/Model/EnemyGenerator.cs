using UnityEngine;
using UniRx;

public class EnemyGenerator : MonoBehaviour {

    public GameObject enemyPlanetCenterPrefab;
    public GameObject player;
    public GameObject planet;

    public Subject<Unit> onEnemyKilled;

	// Use this for initialization
	void Start () {
        onEnemyKilled = new Subject<Unit> ();
        onEnemyKilled.Subscribe (_ => {
            Generate ();
        });
        Generate ();
	}

    void Generate(){
        GameObject enemyPivot = Instantiate (enemyPlanetCenterPrefab);
        enemyPivot.transform.parent = planet.transform;
        Enemy enemy = enemyPivot.transform.Find ("Enemy").GetComponent<Enemy> ();
        enemy.player = player;
        enemy.generator = this;
        enemyPivot.transform.Rotate (new Vector3 (0, 0, Random.Range (-12, 12)));
    }
}
