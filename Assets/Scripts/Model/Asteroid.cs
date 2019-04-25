using UnityEngine;

public class Asteroid : Enemy {
    
    public override void Generate() {
        Projectile projectile = gameObject.GetComponent<Projectile> ();
        Vector3 direction = transform.parent.position - transform.position;
        direction.Normalize ();
        transform.position -= 4 * direction;
        projectile.InitProjectile (model.damage, model.speed, direction, tag);
    }

    public override void Hit(int damage) {
        // Invulnerability
        if (transform.position.y > 10) {
            return;
        }

        base.Hit (damage);
    }
}
