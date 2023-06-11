using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxVelocity = new Vector2(0, 1f);
    public MeshRenderer meshRenderer;
    private float maxVelocity = 2f;
    
    //private float minVelocity = 1f;
    //public Walter_Car car;

    // Update is called once per frame
    void Update()
    {
        if(parallaxVelocity.y < maxVelocity) meshRenderer.material.mainTextureOffset += parallaxVelocity * Time.deltaTime;
    }
}
