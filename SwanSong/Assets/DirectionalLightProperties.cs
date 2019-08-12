using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DirectionalLightProperties : MonoBehaviour
{
    [SerializeField]
    Light dirLight;

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_LightDirectionVec", -transform.forward);
        Shader.SetGlobalFloat("_LightIntensity", dirLight.intensity);
    }

}
