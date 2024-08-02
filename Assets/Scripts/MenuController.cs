using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private GameObject login;
    [SerializeField] private GameObject password;
    [Header("Registration")]
    [SerializeField] private GameObject newLogin;
    [SerializeField] private GameObject newPassword;
    [SerializeField] private GameObject newPassword2;
    [Header("login or registration")]
    [SerializeField] private GameObject LogIn;
    [SerializeField] private GameObject signUp;
    [Header("Some windows")]
    [SerializeField] private GameObject GameMenu;
    [SerializeField] private GameObject LoadMenu;
    [Header("errors, notification, text")]  
    [SerializeField] private GameObject errors;
    [SerializeField] private GameObject notification;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TextMeshProUGUI noticeText;
    [Header("Database")]
    [SerializeField] private WebManager webManager;

    public void Login()
    {
        webManager.OnLogged.AddListener(OnLoginSuccess);
        webManager.OnError.AddListener(OnLoginError);

        webManager.Login(getInput(login), getInput(password));
    }

    public void Register()
    {
        webManager.OnRegistered.AddListener(OnRegisterSuccess);
        webManager.OnError.AddListener(OnLoginError);

        webManager.Registration(getInput(newLogin), getInput(newPassword), getInput(newPassword2));
    }

    private void OnLoginSuccess()
    {
        login.GetComponent<InputField>().text = "";
        password.GetComponent<InputField>().text = "";

        LogIn.SetActive(false);
        GameMenu.SetActive(true);
    }

    private void OnRegisterSuccess()
    {
        notification.SetActive(true);
        noticeText.text = "Succesfull: Користувача створено";

        newLogin.GetComponent<InputField>().text = "";
        newPassword.GetComponent<InputField>().text = "";
        newPassword2.GetComponent<InputField>().text = "";
    }

    public void ExitGame()
    {
        Debug.Log("Гра завершується");
        Application.Quit();
    }

    private void OnLoginError()
    {
        errors.SetActive(true);
        errorText.text = WebManager.usersData.error.errorText;
    }

    private string getInput(GameObject input)
    {
        return input.GetComponent<InputField>().text;
    }

    public void CloseError()
    {
        errors.SetActive(false);
    }

    public void CloseNotification()
    {
        notification.SetActive(false);
    }

    public void switchOnSignUp()
    {
        LogIn.SetActive(false);
        signUp.SetActive(true);
    }

    public void switchOnLogIn()
    {
        signUp.SetActive(false);
        LogIn.SetActive(true);
    }
}
