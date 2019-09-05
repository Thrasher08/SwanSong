using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class EditorPoistionMarker : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(this.transform.position - new Vector3(0.02f, 0, 0), this.transform.position + new Vector3(0.02f, 0, 0), Color.cyan);
        Debug.DrawLine(this.transform.position - new Vector3(0, 0.02f, 0), this.transform.position + new Vector3(0, 0.02f, 0), Color.cyan);
        Debug.DrawLine(this.transform.position - new Vector3(0, 0, 0.02f), this.transform.position + new Vector3(0, 0, 0.02f), Color.cyan);
    }
}
