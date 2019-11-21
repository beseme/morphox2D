using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTexture : MonoBehaviour
{
    [SerializeField]
    private Sprite[] texture = new Sprite[2];
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = texture[0];
    }

    public void GetTexture(int i)
    {
        GetComponent<SpriteRenderer>().sprite = texture[ChangeTextureIndex(i)];
    }

    public static int ChangeTextureIndex(int t)
    {
        return t;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
