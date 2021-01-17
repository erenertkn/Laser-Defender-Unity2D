using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1000;
    [SerializeField] int scorePoint = 100;
    [SerializeField] GameObject laserEnemyPrefab;
    [SerializeField] float minFireTime = 0.1f;
    [SerializeField] float maxFireTime = 3f;
    [SerializeField] GameObject explosionEffectParticle;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip laserSound;

    Space space;
    float counter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageDealer dl = other.gameObject.GetComponent<DamageDealer>();
        int damage = dl.GetDamage();
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject explosion=Instantiate(explosionEffectParticle, transform.position, Quaternion.identity);
            Destroy(explosion, 1);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, .75f);
            print("a");
            space.GetScore(scorePoint);
        }
        dl.Hit();
    }

    private void Start()
    {
        counter = Random.Range(minFireTime, maxFireTime);
        space = FindObjectOfType<Space>();
    }

    private void Update()
    {
        counter -= Time.deltaTime;
        if(counter<=0)
        {
            var enemyLaser = Instantiate(laserEnemyPrefab, new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z), Quaternion.identity);
            enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,Random.Range(-3f, -9f));
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position,0.75f);
            counter = Random.Range(minFireTime, maxFireTime);
        }
    }

}
