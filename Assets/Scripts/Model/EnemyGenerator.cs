using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;
using DG.Tweening;

// Est-ce qu'il faudrait pas mieux dissocier cette classe en 2 ?
// Une classe qui gère les vagues
// Une classe dont hériterait chaque type d'ennemi pour instancier ces objets en jeu
public class EnemyGenerator : MonoBehaviour {

    public GameObject player;
    public GameObject planet;

    SpacePartition spacePartition;

    public List<EnemyWave> waves;
    EnemyWave currentWave;
    int waveIndex;
    WaveInfo currentWaveInfo;

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

        currentWave = waves[waveIndex];
        currentWaveInfo = currentWave.infos[0];
        remainingSpawn = remainingEnemy = currentWaveInfo.number;
        Observable.Timer (TimeSpan.FromSeconds (0), TimeSpan.FromSeconds (currentWaveInfo.respawnRate)).Subscribe (_ => {
            if (remainingSpawn > 0 && spacePartition.haveSpace ()) {
                Generate (1); // On ne sait pas vraiment quel type d'ennemi on doit pondre ...
            }
        }).AddTo(disposables);

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

            int angleIndex = spacePartition.findSpace ();
            shooter.spaceIndex = angleIndex;
            shooter.transform.RotateAround (planet.transform.position, new Vector3 (0, 0, 1), (angleIndex -spacePartition.halfSpaceAngleSize) * 4);

            Vector3 finalPosition = shooter.transform.position - planet.transform.position;
            finalPosition.Normalize ();
            finalPosition *= 4f;
            shooter.PerformArrival (finalPosition);

            remainingSpawn--;
        }
    }
}
