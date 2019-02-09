using UnityEngine;

public class Ship : MonoBehaviour {

    ShipModel model;

    public GameObject projectilePrefab;

    Vector3 leftBottomCorner;
    Vector3 topRightCorner;

    float shotCooldown;

    readonly float EPSILON = 0.00001f;

	void Awake() {
        model = new ShipModel ();
        shotCooldown = 0f;

        topRightCorner = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
        leftBottomCorner = topRightCorner * -1f;
	}

    void FixedUpdate() {
        if (Input.GetKey (KeyCode.Space)){
            if (shotCooldown <= EPSILON) {
                Shoot (transform.position + new Vector3 (0f, 1f, 0f));
                shotCooldown = 1f / model.fireRate;
            } else {
                shotCooldown -= Time.fixedDeltaTime;
            }
        }

        if (Input.GetKey (KeyCode.UpArrow)) {
            transform.position += new Vector3(0f, model.speed * Time.fixedDeltaTime, 0f);
        }

        if (Input.GetKey (KeyCode.DownArrow)) {
            transform.position += new Vector3 (0f, -model.speed * Time.fixedDeltaTime, 0f);
        }

        if (Input.GetKey (KeyCode.LeftArrow)) {
            transform.position += new Vector3 (-model.speed * Time.fixedDeltaTime, 0f, 0f);
        }

        if (Input.GetKey (KeyCode.RightArrow)) {
            transform.position += new Vector3 (model.speed * Time.fixedDeltaTime, 0f, 0f);
        }

        transform.position = new Vector3 (
            Mathf.Clamp (transform.position.x, leftBottomCorner.x, topRightCorner.x), 
            Mathf.Clamp (transform.position.y, leftBottomCorner.y, topRightCorner.y),
            0.0f
        );
	}

	public void Shoot(Vector3 clickedPoint) {
        Vector3 direction = clickedPoint - transform.position;
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;
        direction.Normalize ();
        p.InitProjectile (direction);
    }
}
