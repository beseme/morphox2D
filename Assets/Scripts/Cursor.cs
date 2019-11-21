using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    private Vector2 _lookAt = Vector2.zero;
    [SerializeField]
    private Transform _following;

    private void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;
        _lookAt = _following.transform.position - transform.position;
        transform.up = _lookAt.normalized;
    }
}
