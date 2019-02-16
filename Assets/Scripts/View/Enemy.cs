using UnityEngine;

public class Enemy : BaseEntity {

    public GameObject projectilePrefab;
    public GameObject player;

    [HideInInspector] public EnemyGenerator generator;

    float reloadingTime;

    void Update() {
        float reloadTime = 2f;

        reloadingTime += Time.deltaTime;

        if(reloadingTime >= reloadTime) {
            reloadingTime = 0f;
            Shoot ();
        }
    }

    void Shoot() {
        Vector3 direction = player.transform.position - transform.position;
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;
        direction.Normalize ();
        p.InitProjectile (direction, tag);
    }

    public override void Hit(Projectile projectile) {
        Debug.Log ("Enemy receive : " + projectile.model.damage + " damages !");
        currentLife -= projectile.model.damage;

        if (currentLife <= 0){
            Destroy (transform.parent.gameObject);
            generator.onEnemyKilled.OnNext (UniRx.Unit.Default);
        }
    }
}
