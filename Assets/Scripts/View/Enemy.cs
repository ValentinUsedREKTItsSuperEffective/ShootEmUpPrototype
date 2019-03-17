﻿using UnityEngine;
using DG.Tweening;

public class Enemy : BaseEntity {

    public GameObject projectilePrefab;
    public GameObject player;

    [HideInInspector] public EnemyGenerator generator;
    [HideInInspector] public int spaceIndex;

    float reloadingTime;

    void Update() {
        float reloadTime = 2f;

        reloadingTime += Time.deltaTime;

        if(reloadingTime >= reloadTime) {
            reloadingTime = 0f;
            Shoot ();
        }
    }

    public void PerformArrival(Vector3 finalPosition){
        transform.localScale.Set(1, 3, 1);
        transform.DOMove (finalPosition, 0.4f).From (true).SetEase (Ease.OutQuint);
        transform.DOScaleY (1, 0.4f).From(true);
        transform.DOPlay ();
    }

    void Shoot() {
        Vector3 direction = player.transform.position - transform.position;
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;
        direction.Normalize ();
        p.InitProjectile (direction, tag);
    }

    public override void Hit(Projectile projectile) {
        base.Hit (projectile);
        Debug.Log ("Enemy receive : " + projectile.model.damage + " damages !");

        if (currentLife <= 0){
            Destroy (transform.parent.gameObject);
            generator.onEnemyKilled.OnNext (spaceIndex);
        }
    }
}
