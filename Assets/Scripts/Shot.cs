using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    //audio
    [SerializeField]
    private AudioClip _impactWall;
    [SerializeField]
    private AudioClip _impactEnemy;
    private AudioSource _source;

    //bool
    [SerializeField]
    private bool _isEnemy = false;
    [SerializeField]
    private bool _isUlt = false;
    [SerializeField]
    private bool _isDestructible = false;

    [SerializeField]
    private ParticleSystem _particleSystem;

    //float
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _activity = 10f;

    //GameObjects
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _projectile;

    //vector
    public Vector2 ProjectileDirection = Vector2.zero;

    //misc
    private Rigidbody2D _rgbd;
    public Transform Target;


    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        if (!_isEnemy)
            _speed = 20;
        else
            _speed = 10;
        _source = GetComponent<AudioSource>();
    }

    private IEnumerator StartParticleEffect()
    {
        if (_particleSystem)
            _particleSystem.Play();
        _rgbd.simulated = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(_particleSystem.main.duration);
        Destroy(gameObject);
    }

    private IEnumerator PlaySounds()
    {
        _rgbd.simulated = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(_impactEnemy.length);
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isUlt)
        {
            if (collision.gameObject.tag == "Wall")
            {
                StartCoroutine(StartParticleEffect());
                //Object.Destroy(_projectile);
                _source.PlayOneShot(_impactWall, 1);
            }
            if (collision.gameObject.tag == "Floor")
                Object.Destroy(_projectile);
            if (collision.gameObject.tag == "Enemy" && _isEnemy == false)
            {
                _source.PlayOneShot(_impactEnemy, 1);
                StartCoroutine(PlaySounds());
                //Object.Destroy(_projectile);
                EnemyCondition hp = collision.gameObject.GetComponent<EnemyCondition>();
                hp.TakeDamage(10f);
            }
            if (collision.gameObject.tag == "Player" && _isEnemy == true)
            {
                Object.Destroy(_projectile);
                PlayerCondition hp = collision.gameObject.GetComponent<PlayerCondition>();
                hp.TakeDamage(10f);
            }
            if (collision.gameObject.tag == "Turret" && _isEnemy == false)
            {
                _source.PlayOneShot(_impactEnemy, 1);
                StartCoroutine(PlaySounds());
                MissileTurret hp = collision.gameObject.GetComponent<MissileTurret>();
                hp.TakeDamage(10f);
            }

            if (collision.gameObject.tag == "Ceiling")
                Object.Destroy(_projectile);

            if (collision.gameObject.tag == "Background")
                Object.Destroy(_projectile);
            if (collision.gameObject.tag == "Destruct" && _isEnemy == false)
            {
                //Object.Destroy(_projectile);
                DestructWalls status = collision.gameObject.GetComponent<DestructWalls>();
                status.Destroy(1f);
                _source.PlayOneShot(_impactEnemy, 1);
                StartCoroutine(PlaySounds());
            }
            if (collision.gameObject.tag == "Destruct" && _isEnemy == true)
                Object.Destroy(_projectile);
            if (collision.gameObject.tag == "Projectile" && _isDestructible == true)
                Object.Destroy(_projectile);
        }


        //------ below is Ult script -----//


        if (_isUlt)
        {
            if (collision.gameObject.tag == "Enemy" && _isEnemy == false)
            {
                EnemyCondition hp = collision.gameObject.GetComponent<EnemyCondition>();
                hp.TakeDamage(100f);
            }
            if (collision.gameObject.tag == "Turret" && _isEnemy == false)
            {
                MissileTurret hp = collision.gameObject.GetComponent<MissileTurret>();
                hp.TakeDamage(100f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rgbd.velocity = ProjectileDirection * _speed;
        _activity -= Time.deltaTime;
        if (_activity <= 0)
            Destroy(gameObject);
    }
}
