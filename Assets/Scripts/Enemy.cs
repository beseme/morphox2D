using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //audio
    [SerializeField]
    private AudioClip _shot;
    private AudioSource _source;

    //objects and physics
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private GameObject Destructible;
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

    //movement
    [SerializeField]
    private Transform following;
    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector2 lookAt = Vector2.zero;

   
    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        shotPossible = true;
        _source = GetComponent<AudioSource>();
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
            Vector2 ShootDirection;
            ShootDirection = following.transform.position - transform.position;

            if (Random.Range(1, 5) > 3)
            {
                GameObject curProjectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
                curProjectile.GetComponent<Shot>().ProjectileDirection = ShootDirection.normalized;
            }
            else
            {
                GameObject Destruct = Instantiate(Destructible, transform.position, Quaternion.identity);
                Destruct.GetComponent<Shot>().ProjectileDirection = ShootDirection.normalized;
            }
            _source.PlayOneShot(_shot, 1);

        }
        lookAt = following.transform.position - transform.position;

        transform.up = lookAt.normalized;
    }
}
