using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedHealthBar : MonoBehaviour
{

    public Transform _target;
    public Vector2 _position;

    // Update is called once per frame
    void Update()
    {
        _position = Camera.main.WorldToScreenPoint(_target.position);
        //transform.position = _position + new Vector2(0,70);
        transform.position = _position;
    }
}
