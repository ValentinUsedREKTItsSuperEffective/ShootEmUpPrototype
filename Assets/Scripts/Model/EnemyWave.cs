using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveInfo {
    public EntityModel enemyType;
    public GameObject prefab;
    public int number;
}

[CreateAssetMenu (fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave", order = 2)]
public class EnemyWave : ScriptableObject {

    public List<WaveInfo> enemies;

}
