using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;
    public Button loginButton;
    public Button signUpButton;
    public Button aboutButton;
    private void Awake()
    {
        // fetch the panel as soon as it is initialized
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        loginButton = ui.Q<Button>("LoginButton");
        loginButton.clicked += OnLoginClicked;

        signUpButton = ui.Q<Button>("SignUpButton");
        signUpButton.clicked += OnSignUpClicked;


        aboutButton = ui.Q<Button>("AboutButton");
        aboutButton.clicked += OnAboutClicked;
    }

    private void OnLoginClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnSignUpClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnAboutClicked()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
