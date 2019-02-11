using UnityEngine;

public class Projectile : MonoBehaviour {

    public ProjectileModel model;
    public string entityTag;

    Vector3 direction;

    float topPoint;

	void Awake() {
        topPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f)).y;
	}

    public void InitProjectile(Vector3 direction, string originTag) {
        this.direction = direction;
        entityTag = originTag;

        float stepMovement = model.speed * Time.fixedDeltaTime;

        this.direction *= stepMovement;

    }

	void FixedUpdate() {
        transform.position += direction;

        if (transform.position.y > topPoint || transform.position.y < -18) {
            Destroy (gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        BaseEntity entity = other.GetComponent<BaseEntity> ();
        Debug.Log ("COLLIDE");

        if (entity != null && entityTag != entity.tag) {
            entity.Hit (this);
            Destroy (gameObject);
        }
	}
}
