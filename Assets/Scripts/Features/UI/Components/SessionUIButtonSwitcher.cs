using UnityEngine;

public class SessionUiButtonSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _startSessionButton;
    [SerializeField] private GameObject _continueSessionButton;

    

    private void OnEnable()
    {
        bool isSessionActive = PlayerPrefs.GetInt(
            SessionPersistenceService.IS_SESSION_ACTIVE_STRING,
            SessionPersistenceService.NOT_SESSION_ACTIVE) == SessionPersistenceService.SESSION_ACTIVE;
        Debug.Log($"Session active: {isSessionActive}");
        _startSessionButton.SetActive(isSessionActive);
        _continueSessionButton.SetActive(!isSessionActive);
    }
}
