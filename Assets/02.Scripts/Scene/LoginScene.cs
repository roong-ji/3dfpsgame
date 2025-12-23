using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LoginScene : MonoBehaviour
{
    private enum SceneMode
    {
        Login,
        Register
    }

    private SceneMode _mode = SceneMode.Login;

    [SerializeField] private GameObject _passwordCofirmObject;

    [SerializeField] private Button _gotoRegisterButton;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _gotoLoginButton;
    [SerializeField] private Button _registerButton;

    [SerializeField] private TMP_InputField _idInputField;
    [SerializeField] private TMP_InputField _passwordInputField;
    [SerializeField] private TMP_InputField _passwordConfirmInputField;

    [SerializeField] private TextMeshProUGUI _messageTextUI;

    private const int MinLength = 7;
    private const int MaxLength = 20;

    private void Start()
    {
        AddButtonEvents();
        Refresh();
        LoadLastLoggedinID();
    }

    private void AddButtonEvents()
    {
        _gotoRegisterButton.onClick.AddListener(GotoRegister);
        _loginButton.onClick.AddListener(Login);
        _gotoLoginButton.onClick.AddListener(GotoLogin);
        _registerButton.onClick.AddListener(Register);
    }

    private void Refresh()
    {
        _passwordCofirmObject.SetActive(_mode == SceneMode.Register);
        _gotoRegisterButton.gameObject.SetActive(_mode == SceneMode.Login);
        _loginButton.gameObject.SetActive(_mode == SceneMode.Login);
        _gotoLoginButton.gameObject.SetActive(_mode == SceneMode.Register);
        _registerButton.gameObject.SetActive(_mode == SceneMode.Register);
        _messageTextUI.text = string.Empty;
    }

    private void LoadLastLoggedinID()
    {
        if (!PlayerPrefs.HasKey("LastLoggedinID")) return;

        _idInputField.text = PlayerPrefs.GetString("LastLoggedinID");
        _passwordInputField.text = PlayerPrefs.GetString(_idInputField.text);
    }

    private void Login()
    {
        string id = _idInputField.text;
        if (string.IsNullOrEmpty(id))
        {
            _messageTextUI.text = "아이디를 입력해주세요";
            return;
        }

        if (!IsEmailType(id))
        {
            _messageTextUI.text = "아이디는 이메일 형식이어야 합니다.";
            return;
        }

        string password = _passwordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            _messageTextUI.text = "패스워드를 입력해주세요.";
            return;
        }

        if (!PlayerPrefs.HasKey(id))
        {
            _messageTextUI.text = "아이디를 확인해주세요.";
            return;
        }

        string myPassword = PlayerPrefs.GetString(id);
        if (myPassword != password)
        {
            _messageTextUI.text = "패스워드를 확인해주세요";
            return;
        }

        PlayerPrefs.SetString("LastLoggedinID", id);

        _messageTextUI.text = "* 로그인 성공";

        SceneManager.LoadScene("LoadingScene");
    }

    private void Register()
    {
        string id = _idInputField.text;
        if (string.IsNullOrEmpty(id))
        {
            _messageTextUI.text = "아이디를 입력해주세요";
            return;
        }

        if (!IsEmailType(id))
        {
            _messageTextUI.text = "아이디는 이메일 형식이어야 합니다.";
            return;
        }

        string password = _passwordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            _messageTextUI.text = "패스워드를 입력해주세요";
            return;
        }

        if (!IsValidPassword(password))
        {
            CheckType(password);
            return;
        }

        string password2 = _passwordConfirmInputField.text;
        if (string.IsNullOrEmpty(password2) || password != password2)
        {
            _messageTextUI.text = "패스워드를 확인해주세요";
            return;
        }

        if (PlayerPrefs.HasKey(id))
        {
            _messageTextUI.text = "중복된 아이디입니다.";
            return;
        }

        PlayerPrefs.SetString(id, password);

        GotoLogin();

        _messageTextUI.text = "* 아이디가 생성되었습니다.";
    }

    private void GotoLogin()
    {
        _mode = SceneMode.Login;
        Refresh();
    }

    private void GotoRegister()
    {
        _mode = SceneMode.Register;
        Refresh();
    }

    private void CheckType(string password)
    {
        if (!IsAllowedChars(password))
        {
            _messageTextUI.text = "패스워드는 영어/숫자/특수문자만 가능합니다.";
            return;
        }

        if (!IsAllowedLength(password))
        {
            _messageTextUI.text = $"패스워드는 {MinLength}자리 이상 {MaxLength}자리 이하여야 합니다.";
            return;
        }

        if (!HasSpecialChar(password))
        {
            _messageTextUI.text = "패스워드는 특수문자를 하나 이상 포함해야 합니다.";
            return;
        }

        if (!HasUpperAndLower(password))
        {
            _messageTextUI.text = "패스워드는 대소문자를 각 하나 이상 포함해야 합니다.";
            return;
        }
    }

    private bool IsEmailType(string input)
    {
        return Regex.IsMatch(input, @"^[a-zA-Z0-9]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
    }

    private bool IsAllowedChars(string input)
    {
        return Regex.IsMatch(input, @"^[a-zA-Z0-9!@#$%^&*()_+=.-]+$");
    }

    private bool IsAllowedLength(string input)
    {
        return Regex.IsMatch(input, $@"^.{{{MinLength},{MaxLength}}}$");
    }

    private bool HasSpecialChar(string input)
    {
        return Regex.IsMatch(input, @"[!@#$%^&*()_+=.-]");
    }

    private bool HasUpperAndLower(string input)
    {
        return Regex.IsMatch(input, @"(?=.*[a-z])(?=.*[A-Z])");
    }

    private bool IsValidPassword(string input)
    {
        return Regex.IsMatch(input, $@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+=.-])[a-zA-Z0-9!@#$%^&*()_+=.-]{{{MinLength},{MaxLength}}}");
    }
}