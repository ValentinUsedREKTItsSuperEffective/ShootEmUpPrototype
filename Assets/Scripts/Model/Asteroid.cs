using UnityEngine;

public class Asteroid : BaseEntity {

    [HideInInspector] public Vector3 direction;

    [HideInInspector] public EnemyGenerator generator; // Duplicate with ENEMY class
    [HideInInspector] public int spaceIndex; // Duplicate with ENEMY class

    void Awake() {
        direction = new Vector3 ();
    }

    void Update () {
        transform.position = new Vector3 (
            transform.position.x + direction.x*model.speed*Time.deltaTime, 
            transform.position.y + direction.y * model.speed * Time.deltaTime, 
            transform.position.z
        );
	}

    // Duplicate with ENEMY class
    public override void Hit(Projectile projectile) {
        base.Hit (projectile);

        if (currentLife <= 0) {
            Destroy (transform.parent.gameObject);
            generator.onEnemyKilled.OnNext (spaceIndex);
        }
    }
}
