using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //object to follow
    public GameObject _player;
    [SerializeField]
    private Rigidbody2D _reference;

    //camera movement
    private Vector3 offset;
    private Vector3 _mouseOffset;

    //numbers
    private float _camDefault;
    private float _maxZoom;
    private float _timer = 3f;
    private float _timerFix = 3f;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        offset = transform.position - _player.transform.position;
        _camDefault = Camera.main.orthographicSize;
        _maxZoom = _camDefault * 2f;
    }

    void DynamicCam()
    {
        _mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseOffset -= _player.transform.position;
    }

    void LateUpdate()
    {
        if(_reference.velocity.magnitude > 1 && Camera.main.orthographicSize <= _maxZoom)
        {
            Camera.main.orthographicSize += .2f;
            _timer = _timerFix;
        }
        if(_reference.velocity.magnitude <= 1 && Camera.main.orthographicSize > _camDefault)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0) 
                Camera.main.orthographicSize -= .1f;
            if (Camera.main.orthographicSize <= _camDefault)
                _timer = _timerFix;
            }
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 mouseOffsetMemory = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _player.transform.position;
        Vector3 playerOffsetMemory = Camera.main.ScreenToWorldPoint(_player.transform.position) + new Vector3 (1,1,0);
        if(Mathf.Abs(mouseOffsetMemory.magnitude) < Mathf.Abs(playerOffsetMemory.magnitude))
        DynamicCam();

        transform.position = _player.transform.position + offset + _mouseOffset*.2f;
    }
}
