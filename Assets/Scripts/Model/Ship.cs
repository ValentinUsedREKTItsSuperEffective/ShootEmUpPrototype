using UnityEngine;

public class Ship : BaseEntity {

    IEquipment equipment;

    public GameObject projectilePrefab;

    public Transform planet;

    Vector3 leftBottomCorner;
    Vector3 topRightCorner;

    float reloadingTime;

    readonly float EPSILON = 0.00001f;

    protected override void Awake() {
        base.Awake ();

        equipment = new BaseEquipment ();
        equipment = new BigRifle (equipment);
        equipment = new BiggerRiffle (equipment);

        reloadingTime = 0f;

        topRightCorner = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
        leftBottomCorner = topRightCorner * -1f;
	}

    void FixedUpdate() {
        reloadingTime -= Time.fixedDeltaTime;

        if (Input.GetKey (KeyCode.Space)){
            if (reloadingTime <= EPSILON) {
                Shoot ();
                reloadingTime = 1f / model.fireRate;
            }
        }

        transform.position = new Vector3 (
            Mathf.Clamp (transform.position.x, leftBottomCorner.x, topRightCorner.x), 
            Mathf.Clamp (transform.position.y, leftBottomCorner.y, topRightCorner.y),
            0.0f
        );
	}

	public void Shoot() {
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;

        Vector3 direction = transform.position - planet.position;
        direction.Normalize ();

        Vector3 center = direction * model.fireDispersion + transform.position + direction;
        float xRand = Random.Range (0f, Mathf.PI);
        float yRand = Random.Range (0f, Mathf.PI);
        Vector3 randomPoint = new Vector3 (Mathf.Cos (xRand), Mathf.Sin (yRand), 0f) * model.fireDispersion + center;

        direction = randomPoint - transform.position;
        direction.Normalize ();

        p.InitProjectile (model.damage + equipment.GetDamage(), model.fireSpeed, direction, tag);
    }

    public override void Hit(int damage){
        base.Hit (damage);

        if (model.currentLife.Value <= 0) {
            Destroy (gameObject);
        }
    }
}
