using UnityEngine;
using DG.Tweening;

public class Shooter : Enemy {
    
    public GameObject projectilePrefab;

    float reloadingTime;
    bool invulnerability;

    readonly float EPSILON = 0.00001f;

    public override void Generate(){
        reloadingTime = 1.5f;
        invulnerability = true;

        Vector3 finalPosition = transform.position - transform.parent.position;
        finalPosition.Normalize ();
        finalPosition *= 4f;

        transform.localScale.Set (1, 3, 1);
        transform.DOMove (finalPosition, 0.4f).From (true).SetEase (Ease.OutQuint).OnComplete (() => {
            invulnerability = false;
        });
        transform.DOScaleX (1, 0.1f).From (true);
        transform.DOPlay ();
    }

    void Update() {
        if(invulnerability){
            return;
        }

        reloadingTime -= Time.deltaTime;

        if (reloadingTime <= EPSILON) {
            Shoot ();
            reloadingTime = 1f / model.fireRate;
        }
    }

    void Shoot() {
        Vector3 direction = transform.parent.position - transform.position;
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;
        direction.Normalize ();
        p.InitProjectile (model.damage, model.fireSpeed, direction, tag);
    }

    public override void Hit(int damage) {
        if (invulnerability) {
            return;
        }

        base.Hit (damage);
    }
}
