using System;

using UnityEngine;

public class AppFlowController
{
    private readonly ScreenManager _screenManager;
    private readonly SessionService _sessionService;
    private SessionConfigBuilder _sessionConfigBuilder;
    public SessionConfigBuilder SessionConfigBuilder =>
    _sessionConfigBuilder;

    public AppFlowState CurrentState { get; private set; }
    public event Action<AppFlowState> OnFlowStateChanged;

    public AppFlowController(
        ScreenManager screenManager,
        SessionService sessionService)
    {
        _screenManager = screenManager;
        _sessionService = sessionService;
    }

    public void Initialize()
    {
        if (SessionRepository.HasActiveSession())
        {   
            CurrentState = AppFlowState.ActiveSession;
            _sessionService.LoadSession();
        }
        else
        {
            CurrentState = AppFlowState.NoActiveSession;
        }

        _screenManager.ShowScreen(ScreenType.Home);
        OnFlowStateChanged?.Invoke(CurrentState);
    }

    public void OpenHome()
    {
        if(CurrentState == AppFlowState.ReadyConfiguringSession || CurrentState == AppFlowState.ConfiguringSession || CurrentState == AppFlowState.ViewingResult)
        {
            CurrentState = AppFlowState.NoActiveSession;
            OnFlowStateChanged?.Invoke(CurrentState);
        }
        _screenManager.ShowScreen(ScreenType.Home);
    }

    public void OpenProfile()
    {
        _screenManager.ShowScreen(ScreenType.Profile);
    }

    public void OpenConfigurateSession()
    {
        _sessionConfigBuilder = new SessionConfigBuilder();
        CurrentState = AppFlowState.ConfiguringSession;
        OnFlowStateChanged?.Invoke(CurrentState);
        _screenManager.ShowScreen(ScreenType.ConfigurateNewSession);
    }

    public void ContinueSession()
    {
        if (_sessionService.GetCurrentSession() == null)
        {
            Debug.LogWarning("No active session to continue.");
            return;
        }
        CurrentState = AppFlowState.ActiveSession;
        OnFlowStateChanged?.Invoke(CurrentState);
        _screenManager.ShowScreen(ScreenType.ActiveSession);
    }

    public void StartConfiguratedSession()
    {
        var config = _sessionConfigBuilder.BuildConfig();
        _sessionService.StartSession(config);
        CurrentState = AppFlowState.ActiveSession; 
        _screenManager.ShowScreen(ScreenType.ActiveSession);
        OnFlowStateChanged?.Invoke(CurrentState);
    }

    public void SetStateToConfigReady(bool isConfigReady)
    {
        if (CurrentState != AppFlowState.ConfiguringSession && CurrentState != AppFlowState.ReadyConfiguringSession)
        {
            Debug.LogWarning($"Invalid change of state. It should not be valid to switch beween config states from state{CurrentState.ToString()}");
        }
        CurrentState = isConfigReady ? AppFlowState.ReadyConfiguringSession : AppFlowState.ConfiguringSession;
        OnFlowStateChanged?.Invoke(CurrentState);
    }

    public void EndSession()
    {

        // TODO:
        // - finalize session
        // - move to history
        // - clear active session

        _sessionService.EndSession();
        CurrentState = AppFlowState.ViewingResult;
        OnFlowStateChanged?.Invoke(CurrentState);
        _screenManager.ShowScreen(ScreenType.Result);
    }
}
