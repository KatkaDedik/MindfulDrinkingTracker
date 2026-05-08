using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ProfileScreenUiController : ScreenUIControllerBase
{
    [SerializeField] private TMP_Text _nicknameText;
    [SerializeField] private TMP_Text _weightText;
    [SerializeField] private TMP_Text _heightText;
    [SerializeField] private TMP_Text _ageText;
    [SerializeField] private TMP_Text _genderText;

    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _weightInput;
    [SerializeField] private TMP_InputField _heightInput;
    [SerializeField] private TMP_InputField _ageInput;
    [SerializeField] private TMP_Dropdown _genderDropdown;

    [SerializeField] private Button _editButton;
    [SerializeField] private Button _saveButton;

    private UserProfile _profile;

    protected override bool IsMyScreen(ScreenType screenType)
    {
        return screenType == ScreenType.Profile;
    }

    protected override void OnScreenShown()
    {
        base.OnScreenShown();
        LoadDataToUI();
    }

    public void OnEditClicked()
    {
        _nicknameInput.gameObject.SetActive(true);
        _weightInput.gameObject.SetActive(true);
        _heightInput.gameObject.SetActive(true);
        _ageInput.gameObject.SetActive(true);
        _genderDropdown.gameObject.SetActive(true);

        _nicknameInput.text = _profile.Nickname;
        _weightInput.text = _profile.WeightKg.ToString();
        _heightInput.text = _profile.HeightCm.ToString();
        _ageInput.text = _profile.Age.ToString();
        _genderDropdown.value = _profile.Gender == Gender.Male ? 0 : (_profile.Gender == Gender.Female ? 1 : 2);

        _editButton.gameObject.SetActive(false);
        _saveButton.gameObject.SetActive(true);
    }

    public void OnSaveClicked()
    {
        _profile = new UserProfile
        {
            Nickname = _nicknameInput.text,
            WeightKg = float.TryParse(_weightInput.text, out var w) ? w : 0,
            HeightCm = float.TryParse(_heightInput.text, out var h) ? h : 0,
            Age = int.TryParse(_ageInput.text, out var a) ? a : 0,
            Gender = _genderDropdown.value == 0 ? Gender.Male : (_genderDropdown.value == 1 ? Gender.Female :
            Gender.Other)
        };

        _nicknameInput.gameObject.SetActive(false);
        _weightInput.gameObject.SetActive(false);
        _heightInput.gameObject.SetActive(false);
        _ageInput.gameObject.SetActive(false);
        _genderDropdown.gameObject.SetActive(false);

        ProfileService.SaveProfile(_profile);

        _saveButton.gameObject.SetActive(false);
        _editButton.gameObject.SetActive(true);

        LoadDataToUI();
    }

    private void LoadDataToUI()
    {
        _profile = ProfileService.LoadProfile();
        _nicknameText.text = _profile.Nickname;
        _weightText.text = $"{_profile.WeightKg.ToString()} kg";
        _heightText.text = $"{_profile.HeightCm.ToString()} cm";
        _ageText.text = $"{_profile.Age.ToString()} years";
        _genderText.text = _profile.Gender.ToString();

        _genderDropdown.value = _profile.Gender == Gender.Male ? 0 : (_profile.Gender == Gender.Female ? 1 : 2);
    }

    public void DiscardChanges()
    {
        LoadDataToUI();
        _nicknameInput.gameObject.SetActive(false);
        _weightInput.gameObject.SetActive(false);
        _heightInput.gameObject.SetActive(false);
        _ageInput.gameObject.SetActive(false);
        _genderDropdown.gameObject.SetActive(false);
        _saveButton.gameObject.SetActive(false);
        _editButton.gameObject.SetActive(true);
    }

    public void OnHomeClicked()
    {
        DiscardChanges();
        ScreenManager.ShowScreen(ScreenType.Home);
    }

    public void OnChangeThemeClicked()
    {
        ThemeManager.Instance.ToggleTheme();
    }
}
