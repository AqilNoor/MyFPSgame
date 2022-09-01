using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            TestScript.instance.Age = Random.Range(90, 110);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            int tempAge = TestScript.instance.Age;
        }
    }
}
