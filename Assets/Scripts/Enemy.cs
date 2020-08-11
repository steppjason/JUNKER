using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 1;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        ResetShotCounter();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot(){
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0){
            Fire();
            ResetShotCounter();
        }
    }

    private void ResetShotCounter(){
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Fire(){
        GameObject bullet = Instantiate(projectile, transform.position + new Vector3(0,-1,0), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider){
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
