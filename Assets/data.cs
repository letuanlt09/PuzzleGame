using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    //create data variable
    private Dictionary<string, string> data1;
    // input the data when getDate being called
    public Dictionary<string, string> getData()
        {
            data1 = new Dictionary<string, string>();
            data1.Add("Student", "A person who is studying at a school or college");
            data1.Add("Teacher", "A person who teaches, especially in a school");
            data1.Add("Lawyer", "A person who practices or studies law; an attorney or a counselor");
            data1.Add("Doctor", "A qualified practitioner of medicine; a physician");

            return data1;
        }
    // check if data
    public bool checkData(string key, string value)
    {
        if (data1[key] == value)
            return true;
        else
            return false;
    }
  
}
