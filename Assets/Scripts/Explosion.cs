using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    public Transform missile;
    private float _pulse;
    [SerializeField]
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _pulse = .4f;
        //_explosion.transform.position = missile.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _pulse -= Time.deltaTime;
        if (_pulse <= 0)
            DestroyImmediate(_explosion, true);
    }
}
