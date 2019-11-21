using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    //stops the heart shaped hitbox marker from rotating

    [SerializeField]
    private GameObject _marker;
    [SerializeField]
    private GameObject _player;

    // Update is called once per frame
    void Update()
    {
        _marker.transform.position = Vector3.Scale(_player.transform.position, new Vector3 (1,1,0));
    }
}
