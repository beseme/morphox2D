using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morpher : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            BasicPlayerMovement controlls = collision.gameObject.GetComponent<BasicPlayerMovement>();
            controlls.Morph(ChooseInput.Morphed);
            AimAndShoot ShotPossible = collision.gameObject.GetComponent<AimAndShoot>();
            ShotPossible.Morph(ChooseInput.Morphed);
            BasicPlayerMovement gravity = collision.gameObject.GetComponent<BasicPlayerMovement>();
            gravity.GravityClient(0.0f);
            TextureClient texture = collision.gameObject.GetComponent<TextureClient>();
            texture.GetTexture(1);
        }

    }
}
