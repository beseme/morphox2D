using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCondition : MonoBehaviour
{
    //animation and audio
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _volume;

    //numbers
    [SerializeField]
    private float _hp = 30;
    private int _trailIndex = 3;

    //objects and physics
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Rigidbody2D _self;

    //bools
    [SerializeField]
    private bool _shapeStatus;
    private bool _isAlive = true;
    [SerializeField]
    private bool _hasIFrames;

    //UI
    [SerializeField]
    private Image _failScreen;
    [SerializeField]
    private Image _clearedScreen;
    [SerializeField]
    private Image _hpBar;

   
    private void Start()
    {
        transform.Find("heartHitbox").GetComponent<TrailRenderer>().enabled = false;
        transform.Find("trailSprite").GetComponent<TrailRenderer>().enabled = false;
        _hasIFrames = false;
        _failScreen.gameObject.SetActive(false);
        _clearedScreen.gameObject.SetActive(false);
    }


    public void TakeDamage(float hp)
    {
        if (!_hasIFrames)
        {
            _hp = _hp - hp;
            _hpBar.GetComponent<UIBars>().Bar(_hp, 1, 30, 0, 1);
            _trailIndex--;
            TrailColour(_trailIndex);
        }
    }

    public void TrailColour(int i)
    {
        if (i == 2)
        {
            GetComponent<TrailRenderer>().enabled = false;
            transform.Find("heartHitbox").GetComponent<TrailRenderer>().enabled = true;
        }
        if(i == 1)
        {
            transform.Find("heartHitbox").GetComponent<TrailRenderer>().enabled = false;
            transform.Find("trailSprite").GetComponent<TrailRenderer>().enabled = true;
        }

    }

    /*public void Shaper(bool shape)
    {
        ShapeStatus = shape;
    }
    // Start is called before the first frame update
    void Start()
    {
        ShapeStatus = true;
    }*/

    // Update is called once per frame

    public void OnColliderStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            if (_animator)
                _animator.SetTrigger("Portal");
        }

    }


    void Update()
    {
        //print(_self.velocity.magnitude);
        if (_self.velocity.magnitude > 18f)
            _hasIFrames = true;
        else
            _hasIFrames = false;
        if (_hp <= 0)
            _isAlive = false;
        if (_isAlive == false)
        {
            _volume.volume = .1f;
            _failScreen.gameObject.SetActive(true);
            UnityEngine.Cursor.visible = true;
            Destroy(_player);
        }       
    }
}
