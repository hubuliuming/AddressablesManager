using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Excute : MonoBehaviour
{
    public AssetReference Reference;
    // Start is called before the first frame update
    void Start()
    {
        // AssetsManager.Manager.LoadAssetsAsync();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public abstract class A{}

public class B : A{};
public class C : A{};
