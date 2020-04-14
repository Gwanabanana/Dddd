using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class CameraShader : MonoBehaviour
{
    public Material _mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("A");
        Graphics.Blit(source, destination, _mat);
    }

    
}
