using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTurret : MonoBehaviour
{
    //objects and physics
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private Rigidbody2D _rgbd;
    [SerializeField]
    private Rigidbody2D _eProjectile;

    //numbers
    [SerializeField]
    private float shotCooldownFix = 2f;
    [SerializeField]
    private float shotCooldown = 0.5f;

    //bools
    private bool shotPossible = true;
    private bool inRange = false;

    //aim at ...
    [SerializeField]
    private Transform following;

    //audio
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip _shotSound;

    //movement
    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector2 lookAt = Vector2.zero;



    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        shotPossible = true;
    }



    // Update is called once per frame
    void Update()
    {

        AimDirection = Vector3.zero;
        Target = Input.mousePosition;

        if (shotPossible == false)
            shotCooldown -= Time.deltaTime;
        if (shotCooldown <= 0.0f)
        {
            shotPossible = true;
            shotCooldown = shotCooldownFix;
        }


        if (shotPossible == true)
        {
            shotPossible = false;
            _source.PlayOneShot(_shotSound, 1);
            Vector2 ShootDirection = new Vector2(Random.Range(-36, 36), Random.Range(-36, 36)); 
            GameObject curProjectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            curProjectile.GetComponent<Shot>().ProjectileDirection = ShootDirection.normalized;

        }
        lookAt = following.transform.position - transform.position;

        transform.up = lookAt.normalized;
    }
}
