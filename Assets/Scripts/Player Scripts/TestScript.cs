using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private string _newCityName;

    public string newCityName
    {
        set
        {
            print("new city name is ; " + value);
            _newCityName = value;
        }

        get
        {
            _newCityName = "Multan";
            return _newCityName;
        }
    }

    private void Start()
    {

        newCityName = "Lahore";
        newCityName = "Karachi";
        newCityName = "Islamabaad";
        print("Let us get new city name : " + newCityName);

    }

    //    //CityName("Lahore");
    //    //CityName("Karachi");
    //    //CityName("Islambaad");
    //}

    //public void CityName(string city)
    //{
    //    newCityName = city;
    //    print("new city name is ; " + city);
    //}
}
