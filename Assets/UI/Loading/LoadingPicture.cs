using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingPicture : MonoBehaviour
{
    Image image;
    public Sprite[] sprites;
    int currentSprite;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        
            currentSprite = Mathf.RoundToInt(AcrossSceneTransfer.instance.data[0]);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = sprites[currentSprite];
        
            AcrossSceneTransfer.instance.data[0] = currentSprite;
        
    }
    int randomSprite()
    {
        int r = Random.Range(0, sprites.Length - 1);
        if(r == currentSprite)
        {
            return randomSprite();
        }
        else
        {
            return r;
        }
        
    }
    public void getRandomSprite()
    {
        currentSprite = randomSprite(); 
    }
}
