using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicPlayerMovement : MonoBehaviour
{
    //bools
    private bool _dashPossible = true;
    private bool _isJumping;
    [SerializeField]
    private bool _shotPressed;

    //floats
    private float _angle;
    [SerializeField]
    private float _dash;
    [SerializeField]
    private float _dashCooldownFix = 5f;
    [SerializeField]
    private float _dashCooldown = 0.5f;
    [SerializeField]
    private float _dashTime;
    [SerializeField]
    private float _dashTimeFix;
    [SerializeField]
    private float _jumpHeight;
    float resetTimer;
    [SerializeField]
    private float _speed = 10;
    private float _speedAdd = 1;

    //GameObjects
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _projectile;

    //Rigidbodies
    [SerializeField]
    private Rigidbody2D _rgbd;
    [SerializeField]
    private Rigidbody2D _prjectileRGBD;


    //vectors
    private Vector3 _aimDir = Vector3.zero;
    private Vector3 _cursorPos;
    private Vector2 _dir = Vector2.zero;
    private Vector3 _objPos;
    private Vector3 _targetPos = Vector3.zero;

    //misc
    AsyncOperation bigMissile;
    [SerializeField]
    private ChooseInput _controlls = ChooseInput.Default;
    [SerializeField]
    private UIBars _cooldownBar;
    [SerializeField]
    private UIBars _dashTimeBar;
    private GamePad _gamePad;
    private Transform _target;
    [SerializeField]
    private Image _pauseMenu;
    [SerializeField]
    private AudioSource _globalSource;


   

    // Start is called before the first frame update
    void Start()
    {
        _prjectileRGBD = GetComponent<Rigidbody2D>();
        if (!_rgbd)
        {
            _rgbd = GetComponent<Rigidbody2D>();
        }
        _isJumping = false;

        _dashPossible = true;

        _pauseMenu.gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isJumping = false;
            _rgbd.velocity = Vector2.zero;
        }
    }

    public void Morph(ChooseInput controlls)
    {
        _controlls = controlls;
    }

    public void GravityClient(float gravity)
    {
        this._rgbd.gravityScale = gravity;
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        _dir.x = 0;
        _dir.y = 0;



        _aimDir = Vector3.zero;
        _targetPos = Input.mousePosition;

        switch (_controlls)
        {
            case ChooseInput.Default:
               if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
                    {
                    _isJumping = true;
                   // GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpHeight*80;
                    _rgbd.AddForce(new Vector2(_rgbd.velocity.x, _jumpHeight * 80));

                }

                if (Input.GetKey(KeyCode.LeftArrow))
                    _dir.x -= 5;
                if (Input.GetKey(KeyCode.RightArrow))
                    _dir.x += 5;
                _rgbd.velocity = new Vector2(0,_rgbd.velocity.y) +  _dir * _speed;
                break;
            case ChooseInput.Morphed:

                //pause menu
                if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale > 0)
                {
                    Time.timeScale = 0;
                    _pauseMenu.gameObject.SetActive(true);
                    _globalSource.volume = .1f;
                }

                if(Input.GetKeyUp(KeyCode.Escape) && Time.timeScale < 1)
                {
                    Time.timeScale = 1;
                }

                // basic movement
                if (Input.GetKey(KeyCode.W))
                    _dir.y += _speedAdd;
                if (Input.GetKey(KeyCode.S))
                    _dir.y -= _speedAdd;
                if (Input.GetKey(KeyCode.A))
                    _dir.x -= _speedAdd;
                if (Input.GetKey(KeyCode.D))
                    _dir.x += _speedAdd;
                //----------------------------

                // dash
                if (Input.GetKey(KeyCode.LeftShift) && _dashPossible == true)
                {
                    _dashTime -= Time.deltaTime*10;
                    _dashTimeBar.GetComponent<UIBars>().Bar(_dashTime, 1, _dashTimeFix, 0, 1);

                    if(_dashTime > 0)
                    {
                        _speedAdd = 2;
                    }

                    if (_dashTime <= 0)
                    {
                        _speedAdd = 1;
                        _dashPossible = false;
                    }
                }

                if(!_dashPossible)
                {
                    _dashCooldown -= Time.deltaTime;
                   // _cooldownBar.GetComponent<UIBars>().Bar(_dashCooldown, 1, _dashCooldownFix, 0, 1);
                    if(_dashCooldown <= 0)
                    {
                        _dashTime = _dashTimeFix;
                        _dashCooldown = _dashCooldownFix;
                       // _cooldownBar.GetComponent<UIBars>().Bar(_dashCooldown, 1, _dashCooldownFix, 0, 1);
                        _dashTimeBar.GetComponent<UIBars>().Bar(_dashTime, 1, _dashTimeFix, 0, 1);
                        _dashPossible = true;
                    }
                }

                if (!Input.GetKey(KeyCode.LeftShift))
                    _speedAdd = 1;

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    resetTimer = 2;
                }

                resetTimer -= Time.deltaTime;
                if (resetTimer <= 0 && _dashTime <= _dashTimeFix && _dashPossible && Input.GetKey(KeyCode.LeftShift) == false)
                {
                    _dashTime += Time.deltaTime;
                    _dashTimeBar.GetComponent<UIBars>().Bar(_dashTime, 1, _dashTimeFix, 0, 1);
                }
                //-----------------------------------------------------------------------------

                _cursorPos = Input.mousePosition;
                _objPos = Camera.main.ScreenToWorldPoint(_cursorPos);
                _objPos.z = 0;
                Vector3 dir = _objPos - transform.position;
                _rgbd.rotation = Vector2.SignedAngle(Vector2.up, dir.normalized);
                //transform.up = dir;
                //transform.up = dir.normalized;
                _rgbd.velocity = _dir * _speed;
                break;
        }
        //--Normalize vector--//
        _dir.Normalize();
        //--Also possible--// 
        //_dir = _dir.normalized;

        //--Move object with the rigidbody component--//

    }

}
