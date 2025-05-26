using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signup : MonoBehaviour
{
    public string email;
    public string password;
    public string confirmPassword;

    public signup(string email, string password, string confirmPassword)
    {
        this.email = email;
        this.password = password;
        this.confirmPassword = confirmPassword;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
