using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] int hp = 20000;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip laserSound;

    Space space;

    Coroutine firingRoutine;

    float y;

    float xMin, xMax, yMin, yMax;
    private void Awake()
    {
        y = 0.3f;
    }
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+0.5f;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-0.5f;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y+1f;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-5f;

        space = FindObjectOfType<Space>();
        if(space==null)
        {
            print("a");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingRoutine= StartCoroutine(FireContinously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopFireRoutine();
            y = 0.1f;
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.position = new Vector2( Mathf.Clamp(deltaX + transform.position.x,xMin,xMax), Mathf.Clamp(transform.position.y+deltaY,yMin,yMax));
    }

    IEnumerator FireContinously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 7f);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, 0.75f);
            yield return new WaitForSeconds(0.1f);
        }
        
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageDealer dd = collision.gameObject.GetComponent<DamageDealer>();
        hp -= dd.GetDamage();
        if (hp <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 0.75f);
            FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>().GoLastScene();

        }
        dd.Hit();
        space.GetHP(dd.GetDamage());
    }

    public int GetHp()
    {
        return hp;
    }

    public void StopFireRoutine()
    {
        StopCoroutine(firingRoutine);
    }
}
