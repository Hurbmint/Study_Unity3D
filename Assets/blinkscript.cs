using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject cubeObj;

    private void OnEnable()
    {
        if (cubeObj == null)
        {
            cubeObj = this.gameObject;
        }

        float randX = Random.Range(0.0f, 10.0f);
        float randZ = Random.Range(0.0f, 10.0f);
        cubeObj.transform.localPosition = new Vector3(randX, 0, randZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
