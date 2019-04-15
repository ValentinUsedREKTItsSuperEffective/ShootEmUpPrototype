using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;
using DG.Tweening;

// Est-ce qu'il faudrait pas mieux dissocier cette classe en 2 ?
// Une classe qui gère les vagues
// Une classe dont hériterait chaque type d'ennemi pour instancier ces objets en jeu
public class WaveController : MonoBehaviour {

    public GameObject player;
    public GameObject planet;

    SpacePartition spacePartition;

    public List<EnemyWave> waves;
    int waveIndex;

    public Subject<int> onEnemyKilled;

    int remainingSpawn;
    int remainingEnemy;

    CompositeDisposable disposables;

    void Awake() {
        // !BETTER NOT DOING THIS HERE!
        DOTween.Init ();

        disposables = new CompositeDisposable ();

        spacePartition = new SpacePartition ();
    }

    // Use this for initialization
    void Start() {
        onEnemyKilled = new Subject<int> ();
        onEnemyKilled.Subscribe (index => {
            remainingEnemy--;
            spacePartition.freeSpaceAtIndex (index);

            if(remainingEnemy == 0){
                waveIndex++;

                if(waveIndex < waves.Count){
                    InitializeNextWave ();
                }
            }
        });

        waveIndex = 0;
        InitializeNextWave ();
	}

    void InitializeNextWave(){
        disposables.Clear ();

        waves[waveIndex].infos.ForEach ((info) => {
            remainingSpawn += info.number;
            remainingEnemy += info.number;

            Observable.Timer (TimeSpan.FromSeconds (0), TimeSpan.FromSeconds (info.respawnRate)).Subscribe (_ => {
                if (remainingSpawn > 0 && spacePartition.haveSpace ()) {
                    Generate (info);
                }
            }).AddTo (disposables);

            for (int i = 0; i < info.initialNumber; i++) {
                Generate (info);
            }
        });
    }

    void Generate(WaveInfo info){
        int angleIndex = spacePartition.findSpace ();

        Enemy enemy = Instantiate (info.prefab).GetComponent<Enemy> ();
        enemy.InitializeEnemy (this, angleIndex, planet.transform);
        enemy.transform.RotateAround (planet.transform.position, new Vector3 (0, 0, 1), (angleIndex - spacePartition.halfSpaceAngleSize) * 4);
        enemy.Generate ();

        remainingSpawn--;
    }
}
