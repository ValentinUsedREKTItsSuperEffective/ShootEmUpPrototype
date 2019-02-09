using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject projectilePrefab;
    public GameObject player;

    float reloadingTime;

    public void HitBy(Projectile p){
        Debug.Log ("Receive : " + p.model.damage + " damages !");
    }
     
    void Update() {
        float reloadTime = 2f;

        reloadingTime += Time.deltaTime;

        if(reloadingTime >= reloadTime) {
            reloadingTime = 0f;
            Shoot ();
        }
    }

    void Shoot() {
        Vector3 direction = player.transform.position - transform.position;
        Projectile p = Instantiate (projectilePrefab).GetComponent<Projectile> ();
        p.transform.position = transform.position;
        direction.Normalize ();
        p.InitProjectile (direction);
    }
}
