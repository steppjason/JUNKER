using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] int health = 1;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.75f;
    [SerializeField] GameObject laserPrefabOne;
    [SerializeField] GameObject laserPrefabTwo;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float fireRate = 0.05f;

    Coroutine fireCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    
    // Start is called before the first frame update
    void Start()
    {
        SetMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire(){
        if(Input.GetButtonDown("Fire1")){
            fireCoroutine = StartCoroutine(FireContinue());
        }

        if(Input.GetButtonUp("Fire1")){
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinue(){
        while(true){
            GameObject laserOne = Instantiate(laserPrefabOne, transform.position + new Vector3(-0.2f,0.2f,0), Quaternion.identity) as GameObject;
            GameObject laserTwo = Instantiate(laserPrefabTwo, transform.position + new Vector3(0.2f,0.2f,0), Quaternion.identity) as GameObject;

            laserOne.GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectileSpeed);
            laserTwo.GetComponent<Rigidbody2D>().velocity = new Vector2(0,projectileSpeed);
            yield return new WaitForSeconds(fireRate);
        }        
    }

    private void Move(){
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        
        transform.position = new Vector2(newXPos,newYPos);
    }

    private void SetMoveBoundaries(){
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
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
