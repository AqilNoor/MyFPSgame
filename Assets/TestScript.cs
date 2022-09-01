using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static TestScript instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    private int age;
    public int Age
    {
        get
        {
            print("We are gettin age : " + age);
            return age;
        }
        set
        {
            print(value);
            if(value < 100)
            {
                age = value;
            }
        }
    }








}
