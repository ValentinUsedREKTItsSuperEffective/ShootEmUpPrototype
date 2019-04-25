using UnityEngine;

public class Shield : MonoBehaviour {

    [SerializeField] EntityModel model;

    int currentShield;
    float shieldBuffer;

    void Awake() {
        currentShield = model.shield;
        shieldBuffer = 0f;
    }

    void FixedUpdate() {
        // TODO : faire ça avec UniRx
        if (currentShield < model.shield) {
            shieldBuffer += model.shieldRegen * Time.fixedDeltaTime;

            if(shieldBuffer > 1f){
                currentShield +=  (int)shieldBuffer;
                shieldBuffer -= (int)shieldBuffer;

                if(currentShield > model.shield){
                    currentShield = model.shield;
                }

                Debug.Log ("Shield up to " + currentShield);
            }
        }

        // TODO : Reactivate the shield
    }

    public void Hit(Projectile projectile){
        if (currentShield > 0) {
            int remainingShield = currentShield;
            currentShield -= projectile.damage;

            if (currentShield <= 0) {
                currentShield = 0;
                projectile.damage -= remainingShield;

                gameObject.SetActive (false);
            } else {
                projectile.damage = 0;
            }
        }

        Debug.Log ("Shiled at " + currentShield);
    }
}
