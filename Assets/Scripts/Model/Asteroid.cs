using UnityEngine;

public class Asteroid : Enemy {

    public override void Generate() {
        Projectile projectile = gameObject.GetComponent<Projectile> ();
        Vector3 direction = transform.parent.position - transform.position;
        direction.Normalize ();
        projectile.InitProjectile (model.damage, model.speed, direction, tag);
    }
}
