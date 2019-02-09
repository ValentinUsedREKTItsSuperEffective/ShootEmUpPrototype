using UnityEngine;

public class Projectile : MonoBehaviour {

    public ProjectileModel model;

    Vector3 direction;

    float topPoint;

	void Awake() {
        topPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f)).y;
	}

	public void InitProjectile(Vector3 direction) {
        this.direction = direction;

        float stepMovement = model.speed * Time.fixedDeltaTime;

        Debug.Log ("Projectile go to direction : " + direction + " at a speed of " + model.speed);
        Debug.Log ("Projectile move : " + model.speed * Time.deltaTime + " unit every " + Time.fixedDeltaTime);
        this.direction *= stepMovement;
        Debug.Log ("Projectile go to direction : " + new Vector3 (0f, 0.02f, 0f) + " at a speed of " + model.speed + " after ");

    }

	void FixedUpdate() {
        transform.position += direction;

        if (transform.position.y > topPoint) {
            Destroy (gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.GetComponent<Enemy> ();

        if (enemy != null) {
            enemy.HitBy (this);
            // Destroy (gameObject);
        }
	}
}
