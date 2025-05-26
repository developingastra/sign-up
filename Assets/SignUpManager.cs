using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SignUpManager : MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;
    public InputField confirmPasswordInput;
    public Text feedbackText;

    private const string signupUrl = "https://binusgat.rf.gd/unity-api-test/api/auth/signup.php";

    public void OnSubmit()
    {
        Debug.Log("OnSubmit called!");
        string email = emailInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        Debug.Log($"Input Values - Email: '{email}', Password: '{password}', Confirm: '{confirmPassword}'");


        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            feedbackText.text = "All fields must be filled.";
            return;
        }

        if (password != confirmPassword)
        {
            feedbackText.text = "Password does not match.";
            return;
        }


        signup form = new signup(email, password, confirmPassword);
        string json = form.ToJson();

        Debug.Log("Generated JSON: " + json);

        StartCoroutine(PostSignUpData(email, password));
    }

    private IEnumerator PostSignUpData(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        feedbackText.text = "Submitting...";

        using (UnityWebRequest www = UnityWebRequest.Post(signupUrl, form))
        {
            yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.Success)
#else
            if (www.isNetworkError || www.isHttpError)
#endif
            {
                feedbackText.text = "Error: " + www.error;
                Debug.LogError("Sign-up failed: " + www.error);
            }
            else
            {
                feedbackText.text = "Sign-up successful!";
                Debug.Log("Response: " + www.downloadHandler.text);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SignUpManager script running!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
