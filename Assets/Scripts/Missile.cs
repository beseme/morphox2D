using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //objects and physics
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private GameObject explosion;
    Rigidbody2D _rgbd;

    //numbers
    [SerializeField]
    private float speed;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float timerFix;
    private double pulseFix;
    [SerializeField]
    private double pulse;

    //bools
    private bool isFlying;
    private bool isExploding;
    private bool _played;

    //follow player
    public Transform target;

    //audio
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip _explode;

    public enum IsMissile
    {
        Missile,
        Explosion
    }

    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        timer = 8;     
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Shot")
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg - 90));
            Destroy(missile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y, target.transform.position.x) * Mathf.Rad2Deg - 90));
            Destroy(missile);
        }
        missile.transform.position = gameObject.transform.position;
        Vector2 _dir = target.position - missile.transform.position;
        _rgbd.velocity = _dir.normalized * speed;
        Vector2 _lookAt = target.transform.position - transform.position;
        transform.up = _lookAt.normalized;
    }
}
