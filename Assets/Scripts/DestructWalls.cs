using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructWalls : MonoBehaviour
{
    //objects (wall)
    [SerializeField]
    private GameObject wall;

    //"health"
    [SerializeField]
    private float Structure = 10;

    //visual representation of "health"
    [SerializeField]
    private Sprite[] status = new Sprite[3];

    //audio
    [SerializeField]
    private AudioClip _sound;
    [SerializeField]
    private AudioSource _source;

    //bools
    private bool _half;
    private bool _quarter;
    private bool _gone;
   

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = status[0];
        _half = false;
        _quarter = false;
        _gone = false;
    }

    public void SpriteSwap(int i)
    {
        if(!_half && i == 1)
        {
            GetComponent<SpriteRenderer>().sprite = status[1];
            _source.PlayOneShot(_sound, 1);
        }
        if(!_quarter && i == 2)
        {
            GetComponent<SpriteRenderer>().sprite = status[2];
            _source.PlayOneShot(_sound, 1);
        }
    }

    private IEnumerator PlaySounds()
    {
        if(!_gone)
        _source.PlayOneShot(_sound, 1);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(_sound.length);
        Destroy(wall);
    }

    public void Destroy(float f)
    {
        Structure -= f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Structure <= 7)
        {
            SpriteSwap(1);
            _half = true;
        }
        if (Structure <= 4)
        {
            SpriteSwap(2);
            _quarter = true;
        }
        if (Structure < 1)
        {
            StartCoroutine(PlaySounds());
            _gone = true;
        }
    }
}
