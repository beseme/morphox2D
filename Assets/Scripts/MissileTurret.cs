using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileTurret : MonoBehaviour
{
    //objects and physics
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private GameObject _wall;
    [SerializeField]
    private Rigidbody2D _rgbd;
    [SerializeField]
    private Rigidbody2D _eProjectile;

    //bools
    [SerializeField]
    private bool _last;
    [SerializeField]
    private bool _isTrigger;
    private bool _played;
    private bool _soundPlayable;
    private bool shotPossible = true;
    private bool inRange = false;

    //numbers
    [SerializeField]
    private float hp = 500f;
    [SerializeField]
    private float shotCooldownFix = 1f;
    [SerializeField]
    private float shotCooldown = 1f;
   
    //aim at
    public Transform following;

    [SerializeField]
    private Sprite[] colour = new Sprite[2];

    //movement
    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector2 lookAt = Vector2.zero;

    //audio
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioSource _globalSource;
    [SerializeField]
    private AudioSource _trigger;
    [SerializeField]
    private AudioClip _shotSound;
    [SerializeField]
    private AudioClip _explosion;
    [SerializeField]
    private AudioClip _triggerSound;

    //UI
    [SerializeField]
    private Image _healthBar;

    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        shotPossible = true;
        //GetComponent<SpriteRenderer>().sprite = colour[0];
    }

    public void TakeDamage(float HP)
    {
        hp -= HP;
        _healthBar.GetComponent<UIBars>().Bar(hp, 1, 500, 0, 1);
    }

    private IEnumerator Death()
    {
        if (!_played)
        {
            _source.PlayOneShot(_explosion, 1);
            _played = true;
        }
        yield return new WaitForSeconds(_explosion.length * 0.5f);
        if (_isTrigger)
        {
            _wall.SetActive(false);
            if (_soundPlayable)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                _trigger.PlayOneShot(_triggerSound, .5f);
                _soundPlayable = false;
                Destroy(_enemy);
            }
        }
        else
            Destroy(_enemy);
    }

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
            GameObject obj = Instantiate(enemyProjectile);
            Missile missile = obj.GetComponent<Missile>();
            obj.transform.position = transform.position;

            missile.target = following;


        }

        if (hp <= 0)
        {
            if (_last)
            {
                if (_animator)
                    _animator.SetTrigger("dead");
                StartCoroutine(Death());
                Time.timeScale = 0;
                UnityEngine.Cursor.visible = true;
                _globalSource.volume = .05f;
                //_cleared.gameObject.SetActive(true);
            }

            else
            {
                if (_animator)
                    _animator.SetTrigger("dead");
                StartCoroutine(Death());
            }
           

        }
        lookAt = following.transform.position - transform.position;
        transform.up = lookAt.normalized;
    }
}
