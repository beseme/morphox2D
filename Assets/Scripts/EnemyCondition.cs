using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyCondition : MonoBehaviour
{
    //audio and animation
    [SerializeField]
    private AudioClip _triggerSound;
    [SerializeField]
    private AudioClip _expSound;
    private AudioSource _source;
    [SerializeField]
    private AudioSource _globalVolume;
    [SerializeField]
    private AudioSource _parentSource;
    [SerializeField]
    private Animator _animator;

    //objects
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private GameObject _wall;
    [SerializeField]
    private GameObject _inactiveEnemy;

    //numbers
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _maxHp;

    //bools
    private bool _soundPlayable; 
    [SerializeField]
    private bool _last;
    [SerializeField]
    private bool _trigger;
    [SerializeField]
    private bool _wake;
    private bool IsAlive;
    private bool _played;

    //UI
    [SerializeField]
    private UIBars _healthBar;
    [SerializeField]
    private Image _cleared;

    // Start is called before the first frame update
    void Start()
    {
        IsAlive = true;
        _source = GetComponent<AudioSource>();
        _soundPlayable = true;
        _cleared.gameObject.SetActive(false);
        _played = false;
    }

    private IEnumerator Death()
    {
        if (!_played)
        {
            _source.PlayOneShot(_expSound, 1);
            _played = true;
        }
        yield return new WaitForSeconds(_expSound.length*0.5f);
        if (_trigger)
        {
            _wall.SetActive(false);
            if (_soundPlayable)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                _parentSource.PlayOneShot(_triggerSound, .5f);
                _soundPlayable = false;
                Destroy(Enemy);
            }
        }
        if (_wake)
        {
            _inactiveEnemy.SetActive(true);
            if (_soundPlayable)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                _parentSource.PlayOneShot(_triggerSound, .5f);
                _soundPlayable = false;
                Destroy(Enemy);
            }
        }
        else
        Destroy(Enemy);
    }

    public void TakeDamage(float dmg)
    {
        _hp -= dmg;
        _healthBar.GetComponent<UIBars>().Bar(_hp, 1, _maxHp, 0, 1);
    }


    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
            IsAlive = false;
        if (IsAlive == false)
        {
            if (_last)
            {
                if (_animator)
                    _animator.SetTrigger("dead");
                StartCoroutine(Death());
                Time.timeScale = 0;
                UnityEngine.Cursor.visible = true;
                _globalVolume.volume = .05f;
                _cleared.gameObject.SetActive(true);
            }

            else
            {
                if (_animator)         
                    _animator.SetTrigger("dead");
                
                StartCoroutine(Death());
            }
        }

    }
}
