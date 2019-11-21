using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;


    // Start is called before the first frame update
    void Start()
    {
        _enemy.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            _enemy.SetActive(true);
    }
}
