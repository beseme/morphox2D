using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    //audio
    [SerializeField]
    private AudioClip _shotSound;
    [SerializeField]
    private AudioClip _ultChargeSound;
    private AudioSource _source;

    //bools
    private bool _charged;
    private bool _isCharging;
    private bool _ultPossible = false;
    private bool _shotPossible = true;

    //floats;
    private float _ultChargeFix = .5f;
    private float _ultCharge = .5f;
    [SerializeField]
    private float _ultCooldown = 10f;
    [SerializeField]
    private float _shotCooldownFix = 5f;
    [SerializeField]
    private float _shotCooldown = 0.5f;
    private float _volume = .6f;

    //GameObjects
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private GameObject _ult;
    
    //Rigidbody
    [SerializeField]
    private Rigidbody2D _rgbd;

    //vectors
    private Vector3 _aimDirection = Vector3.zero;
    private Vector3 _target = Vector3.zero;

    //misc
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Sprite[] _curSprite = new Sprite[3];
    [SerializeField]
    private ChooseInput _inputActive = ChooseInput.Default;
    [SerializeField]
    private UIBars bar;


    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        _isCharging = false;
        _charged = false;
        _source = GetComponent<AudioSource>();
    }

    public void Morph(ChooseInput controlls)
    {
        _inputActive = controlls;
    }


    // Update is called once per frame
    void Update()
    {
        _aimDirection = Vector3.zero;
        _target = Input.mousePosition;

        switch (_inputActive)
        {
            case ChooseInput.Default:
                break;
            case ChooseInput.Morphed:

                //regular shot
                if (_shotPossible == false)
                    _shotCooldown -= Time.deltaTime;
                if (_shotCooldown <= 0.0f)
                {
                    _shotPossible = true;
                    _shotCooldown = _shotCooldownFix;
                }               

                if (_shotPossible == true && Input.GetKey(KeyCode.Mouse0))
                {
                    _shotPossible = false;

                    _source.PlayOneShot(_shotSound, _volume);

                    Vector3 ShootDirection;
                    ShootDirection = Input.mousePosition;
                    ShootDirection.z = 0.0f;
                    ShootDirection = Camera.main.ScreenToWorldPoint(ShootDirection);
                    ShootDirection.z = 0.0f;
                    ShootDirection = (ShootDirection - transform.position).normalized;
                    ShootDirection.z = 0.0f;

                    GameObject curProjectile = Instantiate(_projectile, transform.position + transform.up, Quaternion.Euler(0, 0, Mathf.Atan2(ShootDirection.y, ShootDirection.x) * Mathf.Rad2Deg - 90));
                    curProjectile.GetComponent<Shot>().ProjectileDirection = new Vector2(ShootDirection.x, ShootDirection.y).normalized;
                }
              
                //ult
                if (Input.GetKeyDown(KeyCode.Space) && _ultPossible)
                {
                    if (_animator)
                    {
                        _animator.SetTrigger("Charge");
                        _animator.SetBool("Charging", true);
                    }
                    _source.PlayOneShot(_ultChargeSound, 1);
                    }

                if (Input.GetKey(KeyCode.Space ))
                {
                    _isCharging = true;
                    _ultCharge -= Time.deltaTime;
                    if (_ultCharge <= 0)
                    {
                        _ultCharge = _ultChargeFix;
                        _charged = true;
                    }
                }

                if (!_ultPossible)
                {
                    _ultCooldown += Time.deltaTime;
                    bar.GetComponent<UIBars>().Bar(_ultCooldown, 1, 10, 0, 1);
                    if (_ultCooldown >= 10)
                    {
                        _ultCooldown = 0;
                        _ultPossible = true;
                    }
                }
                

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (_animator)
                        _animator.SetBool("Charging", false);

                    _isCharging = false;

                    if (_ultPossible && _charged)
                    {
                        _ultPossible = false;

                        GetComponent<SpriteRenderer>().sprite = _curSprite[0];

                        Vector3 ShootDirection;
                        ShootDirection = Input.mousePosition;
                        ShootDirection.z = 0.0f;
                        ShootDirection = Camera.main.ScreenToWorldPoint(ShootDirection);
                        ShootDirection.z = 0.0f;
                        ShootDirection = (ShootDirection - transform.position).normalized;
                        ShootDirection.z = 0.0f;

                        _ultPossible = false;
                        _ultCharge = _ultChargeFix;
                        GameObject ultimate = Instantiate(_ult, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(ShootDirection.y, ShootDirection.x) * Mathf.Rad2Deg - 90));
                        ultimate.GetComponent<Shot>().ProjectileDirection = new Vector2(ShootDirection.x, ShootDirection.y).normalized;

                    }
                }
                break;
        }
    }
}
