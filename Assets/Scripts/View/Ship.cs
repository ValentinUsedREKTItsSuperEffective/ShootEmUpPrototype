using UnityEngine;

public class Ship : MonoBehaviour {

    public BaseEntity model;

    public GameObject projectilePrefab;

    public GameObject shipRotation;

    Vector3 leftBottomCorner;
    Vector3 topRightCorner;

    float shotCooldown;

    readonly float EPSILON = 0.00001f;

	void Awake() {
        shotCooldown = 0f;

        topRightCorner = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
        leftBottomCorner = topRightCorner * -1f;
	}

    void FixedUpdate() {
        if (Input.GetKey (KeyCode.Space)){
            if (shotCooldown <= EPSILON) {
                Shoot ();
                shotCooldown = 1f / model.fireRate;
            } else {
                shotCooldown -= Time.fixedDeltaTime;
            }
        }

        transform.position = new Vector3 (
            Mathf.Clamp (transform.position.x, leftBottomCorner.x, topRightCorner.x), 
            Mathf.Clamp (transform.position.y, leftBottomCorner.y, topRightCorner.y),
            0.0f
        );
	}

	public void Shoot() {
        Vector3 direction = transform.position - shipRotation.transform.position;
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;
        direction.Normalize ();
        p.InitProjectile (direction, tag);
    }
}
