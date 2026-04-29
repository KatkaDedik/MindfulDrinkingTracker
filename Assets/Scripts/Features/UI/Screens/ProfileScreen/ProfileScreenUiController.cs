using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ProfileScreenUiController : ScreenUIControllerBase
{
    [SerializeField] private TMP_Text _nicknameText;
    [SerializeField] private TMP_Text _weightText;
    [SerializeField] private TMP_Text _heightText;
    [SerializeField] private TMP_Text _ageText;

    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private TMP_InputField _weightInput;
    [SerializeField] private TMP_InputField _heightInput;
    [SerializeField] private TMP_InputField _ageInput;

    [SerializeField] private Button _editButton;
    [SerializeField] private Button _saveButton;

    [SerializeField] private TMP_Dropdown _genderDropdown;


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

        _editButton.gameObject.SetActive(false);
        _saveButton.gameObject.SetActive(true);
    }

    public void OnSaveClicked()
    {
        var profile = new UserProfile
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

        ProfileService.SaveProfile(profile);

        _saveButton.gameObject.SetActive(false);
        _editButton.gameObject.SetActive(true);

        LoadDataToUI();
    }

    private void LoadDataToUI()
    {
        var profile = ProfileService.LoadProfile();
        _nicknameText.text = profile.Nickname;
        _weightText.text = $"{profile.WeightKg.ToString()} kg";
        _heightText.text = $"{profile.HeightCm.ToString()} cm";
        _ageText.text = $"{profile.Age.ToString()} years";

        _genderDropdown.value = profile.Gender == Gender.Male ? 0 : (profile.Gender == Gender.Female ? 1 : 2);
    }

    public void OnHomeClicked()
    {
        ScreenManager.ShowScreen(ScreenType.Home);
    }

    public void OnChangeThemeClicked()
    {
        ThemeManager.Instance.ToggleTheme();
    }
}
