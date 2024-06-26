
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using YFramework.Extension;

public class Excute : MonoBehaviour
{
    private GameObject _go;

    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadSprites().StartCoroutine(this);
        }
    }

    private void LoadGo()
    {
        WebAssetsManager.Manager.InstantiateGameObject("Image", transform);
    }
    
    private void LoadSprite()
    {
        WebAssetsManager.Manager.LoadAssetAsync<Sprite>("HH", d =>
        {
            img.sprite = d;
        });
    }
    
    private IEnumerator LoadSprites()
    {
        IList<Sprite> list = new List<Sprite>();
        WebAssetsManager.Manager.LoadAssetsAsync<Sprite>("HH", d =>
        {
            img.sprite = d;
            
        });
        yield return null;
    }
}

