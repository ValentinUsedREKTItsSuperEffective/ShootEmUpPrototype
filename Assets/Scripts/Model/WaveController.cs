using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;
using DG.Tweening;

public class WaveController : MonoBehaviour {

    public GameObject player;
    public GameObject planet;

    GameGrid spacePartition;

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

        spacePartition = new GameGrid (planet.transform);
    }

    void Start() {
        onEnemyKilled = new Subject<int> ();
        onEnemyKilled.Subscribe (index => {
            remainingEnemy--;
            spacePartition.FreeSpaceAtIndex (index);

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
                if (remainingSpawn > 0 && spacePartition.HaveSpace ()) {
                    Generate (info);
                }
            }).AddTo (disposables);

            for (int i = 0; i < info.initialNumber; i++) {
                Generate (info);
            }
        });
    }

    void Generate(WaveInfo info){
        GridCase gc = spacePartition.FindSpace (); 

        Enemy enemy = Instantiate (info.prefab).GetComponent<Enemy> ();
        enemy.InitializeEnemy (this, gc.index, planet.transform);
        enemy.transform.position = gc.position;

        // Same as LookAt *Blow my mind*
        enemy.transform.right = planet.transform.position - enemy.transform.position;

        enemy.Generate ();

        remainingSpawn--;
    }
}
