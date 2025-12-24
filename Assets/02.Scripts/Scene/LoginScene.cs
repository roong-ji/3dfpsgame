using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Start()
    {
        AddButtonEvents();
        Refresh();
        LoadLastLoggedinID();
    }

    private void TestAES()
    {
        string originalData = "내 아이디는 test@gmail.com 입니다.";
        string myKey = "MySecretKey123";

        Debug.Log($"<color=green>[원본]</color> {originalData}");

        string encryptedData = AES.Encrypt(originalData, myKey);
        Debug.Log($"<color=yellow>[암호화됨]</color> {encryptedData}");

        string decryptedData = AES.Decrypt(encryptedData, myKey);
        Debug.Log($"<color=cyan>[복호화됨]</color> {decryptedData}");

        if (originalData == decryptedData)
        {
            Debug.Log("성공! 원본과 완벽하게 일치합니다.");
        }
        else
        {
            Debug.LogError("실패.. 데이터가 다릅니다.");
        }

        //string wrongDecryption = AES.Decrypt(encryptedData, "WrongPassword");
        //Debug.Log($"<color=red>[틀린 비번 시도]</color> 결과: {wrongDecryption}");
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
    }

    private void Login()
    {
        string id = _idInputField.text;
        if (string.IsNullOrEmpty(id))
        {
            _messageTextUI.text = "아이디를 입력해주세요";
            return;
        }

        if (!Reg.IsEmailType(id))
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

        string idHash = Hash.GetHash(id);
        if (!PlayerPrefs.HasKey(idHash))
        {
            _messageTextUI.text = "아이디를 확인해주세요.";
            return;
        }

        string passwordDB = PlayerPrefs.GetString(idHash);
        string passwordHash = Hash.GetHash(password);

        if (passwordDB != passwordHash)
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

        if (!Reg.IsEmailType(id))
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

        if (!Reg.IsValidPassword(password))
        {
            SetPasswordErrorMessage(password);
            return;
        }

        string password2 = _passwordConfirmInputField.text;
        if (string.IsNullOrEmpty(password2) || password != password2)
        {
            _messageTextUI.text = "패스워드를 확인해주세요";
            return;
        }

        string idHash = Hash.GetHash(id);
        if (PlayerPrefs.HasKey(idHash))
        {
            _messageTextUI.text = "중복된 아이디입니다.";
            return;
        }

        string passwordHash = Hash.GetHash(password);
        PlayerPrefs.SetString(idHash, passwordHash);

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

    private void SetPasswordErrorMessage(string password)
    {
        if (!Reg.IsAllowedChars(password))
        {
            _messageTextUI.text = "패스워드는 영어/숫자/특수문자만 가능합니다.";
            return;
        }

        if (!Reg.IsAllowedLength(password))
        {
            _messageTextUI.text = $"패스워드는 {Reg.MinLength}자리 이상 {Reg.MaxLength}자리 이하여야 합니다.";
            return;
        }

        if (!Reg.HasSpecialChar(password))
        {
            _messageTextUI.text = "패스워드는 특수문자를 하나 이상 포함해야 합니다.";
            return;
        }

        if (!Reg.HasUpperAndLower(password))
        {
            _messageTextUI.text = "패스워드는 대소문자를 각 하나 이상 포함해야 합니다.";
            return;
        }
    }
}