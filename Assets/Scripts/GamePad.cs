using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePad : MonoBehaviour
{
    [SerializeField]
    private bool _shotPressed;

        public void GetMoveDir()
    {
        Vector2 stick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (stick != Vector2.zero)
            stick.Normalize();
    }

    public void GetShootDir()
    {
        Vector2 stick = new Vector2(Input.GetAxis("HorizontalRight"), Input.GetAxis("VerticalRight"));
        if (stick != Vector2.zero)
            stick.Normalize();
    }

    public bool ShootActive()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (!_shotPressed)
            {
                return _shotPressed = true;
            }
            else
                return false;
        }
        else
            return (_shotPressed = false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
