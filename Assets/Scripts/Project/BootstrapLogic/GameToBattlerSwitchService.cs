using System;
using Project.CustomSceneManagement;
using Project.Sessions;
using ProjectShared.Sessions;
using UnityEngine;

namespace Project.BootstrapLogic
{
    public interface ISessionRunner
    {
        void Run();
    }

    public class NotConfiguredLevelDTOException : Exception
    {
        public NotConfiguredLevelDTOException(string description) : base(description) { }
    }
    
    public class GameToBattlerSwitchService : ISessionRunner
    {
        private readonly ISessionInfo _sessionInfo;
        private readonly IScene _scene;
        private readonly ISceneManagement _sceneManagement;

        public GameToBattlerSwitchService(ISessionInfo sessionInfo, IScene scene, ISceneManagement sceneManagement)
        {
            _sessionInfo = sessionInfo;
            _scene = scene;
            _sceneManagement = sceneManagement;
        }

        public async void Run()
        {
            if (string.IsNullOrEmpty(_sessionInfo.SceneName))
            {
                throw new NotConfiguredLevelDTOException("Not configured scene name");
            }

            try
            {
                var loadedSceneInfo = _sceneManagement.LoadScene(_sessionInfo.SceneName);
                await loadedSceneInfo.Item1;
                loadedSceneInfo.Item2.Result.allowSceneActivation = true;
                await _sceneManagement.UnloadScene(_scene).Item1;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }
        
    }
}
