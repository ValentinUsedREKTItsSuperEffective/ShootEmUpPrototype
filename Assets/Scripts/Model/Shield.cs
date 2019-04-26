using System;
using UnityEngine;
using UniRx;

public class Shield : MonoBehaviour {

    [SerializeField] EntityModel model;

    int currentShield;

    Subject<Unit> shieldBreak;

    void Awake() {
        currentShield = model.shield;

        shieldBreak = new Subject<Unit> ();
        shieldBreak.Subscribe (_ => {
            gameObject.SetActive (false);

            Observable.Timer (TimeSpan.FromSeconds (model.shieldReloadCooldown)).Subscribe (__ => {
                currentShield = (int)(model.shield * model.shieldReloadCapacity);

                gameObject.SetActive (true);
                InitShieldRegen ();
            });
        }).AddTo (gameObject);

        InitShieldRegen ();
    }

    public void Hit(Projectile projectile){
        if (currentShield > 0) {
            int remainingShield = currentShield;
            currentShield -= projectile.damage;

            if (currentShield <= 0) {
                currentShield = 0;
                projectile.damage -= remainingShield;

                shieldBreak.OnNext (Unit.Default);
            } else {
                projectile.damage = 0;
            }
        }
    }

    void InitShieldRegen(){
        Observable.Timer (TimeSpan.FromSeconds (0.0), TimeSpan.FromSeconds (1.0)).TakeUntil(shieldBreak).Subscribe (_ => {
            if (currentShield < model.shield) {
                currentShield += model.shieldRegen;

                if (currentShield > model.shield) {
                    currentShield = model.shield;
                }
            }
        });
    }
}
