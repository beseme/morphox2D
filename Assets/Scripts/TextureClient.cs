using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureClient : MonoBehaviour
{

    [SerializeField]
    private Sprite[] texture = new Sprite[2];
    public int debug = 0;

  
   

    // Start is called before the first frame update

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = texture[0];
    }

    public void GetTexture(int i)
    {
        GetComponent<SpriteRenderer>().sprite =  texture[ChangeTextureIndex(i)];
        if (i == 0)
        {
            BackgroundTexture Background = GetComponent<BackgroundTexture>();
            Background.GetTexture(0);
        }
        if (i == 1)
        {
            BackgroundTexture Background = GetComponent<BackgroundTexture>();
            Background.GetTexture(1);
        }


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
