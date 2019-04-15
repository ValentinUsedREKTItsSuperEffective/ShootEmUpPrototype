﻿using UnityEngine;
using DG.Tweening;

public class Shooter : Enemy {
    
    public GameObject projectilePrefab;

    float reloadingTime;
    bool invulnerability;

    public Vector3 finalPosition;

    public override void Generate(){
        invulnerability = true;

        transform.localScale.Set (1, 3, 1);
        transform.DOMove (finalPosition, 0.4f).From (true).SetEase (Ease.OutQuint).OnComplete (() => {
            invulnerability = false;
        });
        transform.DOScaleY (1, 0.1f).From (true);
        transform.DOPlay ();
    }

    void Update() {
        float reloadTime = 2f;

        reloadingTime += Time.deltaTime;

        if (reloadingTime >= reloadTime) {
            reloadingTime = 0f;
            Shoot ();
        }
    }

    void Shoot() {
        Vector3 direction = transform.parent.position - transform.position;
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;
        direction.Normalize ();
        p.InitProjectile (direction, tag);
    }

    public override void Hit(Projectile projectile) {
        if (invulnerability) {
            return;
        }

        base.Hit (projectile);
    }
}