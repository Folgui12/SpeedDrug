using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 parallaxVelocity = new Vector2(0, 1f);
    public MeshRenderer meshRenderer;

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += parallaxVelocity * Time.deltaTime;
    }
}
