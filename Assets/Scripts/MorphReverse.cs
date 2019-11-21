using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphReverse : MonoBehaviour
{


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BasicPlayerMovement controlls = collision.gameObject.GetComponent<BasicPlayerMovement>();
            controlls.Morph(ChooseInput.Default);
            AimAndShoot ShotPossible = collision.gameObject.GetComponent<AimAndShoot>();
            ShotPossible.Morph(ChooseInput.Default);
            BasicPlayerMovement gravity = collision.gameObject.GetComponent<BasicPlayerMovement>();
            gravity.GravityClient(10f);
            TextureClient texture = collision.gameObject.GetComponent<TextureClient>();
            texture.GetTexture(0);
        }

    }
}
