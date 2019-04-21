using UnityEngine;

public class Projectile : MonoBehaviour {

    [HideInInspector] public string entityTag;

    Vector3 direction;
    protected int damage;

    float topPoint;

	void Awake() {
        topPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f)).y + 5;
	}

    public void InitProjectile(int damage, float projectileSpeed, Vector3 direction, string originTag) {
        this.damage = damage;
        this.direction = direction;
        entityTag = originTag;

        float stepMovement = projectileSpeed * Time.fixedDeltaTime;
        this.direction *= stepMovement;

    }

	void FixedUpdate() {
        transform.position += direction;

        if (transform.position.y > topPoint || transform.position.y < -18) {
            Destroy (gameObject);
        }
	}

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        BaseEntity entity = other.GetComponent<BaseEntity> ();

        if (entity != null && entityTag != entity.tag) {
            entity.Hit (damage);
            Destroy (gameObject);
        }
	}
}
