using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public GameObject enemyPlanetCenterPrefab;
    public GameObject player;
    public GameObject planet;

	// Use this for initialization
	void Start () {
        GameObject enemyPivot = Instantiate (enemyPlanetCenterPrefab);
        enemyPivot.transform.parent = planet.transform;
        Enemy enemy = enemyPivot.transform.Find ("Enemy").GetComponent<Enemy>();
        enemy.player = player;
        enemyPivot.transform.Rotate (new Vector3 (0, 0, Random.Range(-12, 12)));
	}
}
