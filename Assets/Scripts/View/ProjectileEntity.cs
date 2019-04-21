using UnityEngine;

public class ProjectileEntity : Projectile {

    protected override void OnTriggerEnter2D(Collider2D other) {
        BaseEntity entity = other.GetComponent<BaseEntity> ();

        if (entity != null && entityTag != entity.tag) {
            entity.Hit (damage);
            Destroy (gameObject);

            Enemy enemy = GetComponent<Enemy> ();
            enemy.generator.onEnemyKilled.OnNext (enemy.spaceIndex);
        }
    }
}
