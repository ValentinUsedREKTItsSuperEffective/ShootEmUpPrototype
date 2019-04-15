using UnityEngine;

public class Asteroid : Enemy {

    [HideInInspector] public Vector3 direction;

    void Awake() {
        direction = new Vector3 ();
    }

    public override void Generate() {
        direction = transform.parent.position - transform.position;
    }

    void Update () {
        transform.position = new Vector3 (
            transform.position.x + direction.x * model.speed * Time.deltaTime, 
            transform.position.y + direction.y * model.speed * Time.deltaTime, 
            transform.position.z
        );
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Ship> () != null) {
            Debug.Log ("COLLIDE SHIP : INFLICT DAMAGE TO THE SHIP");
            Destroy (gameObject);
            generator.onEnemyKilled.OnNext (spaceIndex);
        } else if (other.GetComponent<Planet>() != null){
            Debug.Log ("COLLIDE PLANET: Inflict DAMAGE TO THE PLANET");
            Destroy (gameObject);
            generator.onEnemyKilled.OnNext (spaceIndex);
        };
    }
}
