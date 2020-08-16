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
    [SerializeField] GameObject particleDebris;
    [SerializeField] GameObject particleExplosion;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathSFXVolume = 0.75f;
    [SerializeField] int enemyScorePoints = 10;
    [SerializeField] int collisionDamage = 1;

    ScoreBoard scoreBoard;


    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
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
            if(GameObject.Find("Player"))
            {
                Fire();
                ResetShotCounter();
            }
        }
    }

    private void ResetShotCounter(){
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Fire(){
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
        bullet.GetComponent<Rigidbody2D>().velocity = (GameObject.Find("Player").transform.position - transform.position).normalized * projectileSpeed; 
    }

    private void OnTriggerEnter2D(Collider2D collider){
    
        Debug.Log(collider.gameObject.name);

        if(collider.gameObject.name == "Player"){
            Player player = collider.gameObject.GetComponent<Player>();
            health -= player.GetDamage();
        } else if(collider.gameObject.name == "PlayerLaser(Clone)"){
            DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
            
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
        }


        Death();
    }

    public void Death(){
        if(health <= 0){
            scoreBoard.AddScore(enemyScorePoints);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
            Destroy(gameObject);
            GameObject explosion = Instantiate(particleExplosion, transform.position, Quaternion.identity);
            Destroy(explosion,3);
            GameObject debris = Instantiate(particleDebris, transform.position, Quaternion.identity);
            Destroy(debris,3);
        }
    }

    public int GetDamage(){
        return collisionDamage;
    }
}
