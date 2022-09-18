using System;
using Cysharp.Threading.Tasks;
using Project.Sessions;
using UnityEngine;
using Zenject;

namespace Project.BootstrapLogic
{
    public class UserRunGameEmulator : MonoBehaviour
    {
        private const string SceneName = "BattleScene";
        private ISessionInfoWritable _sessionInfoWritable;
        private ISessionRunner _sessionRunner;

        [Inject]
        void Construct(ISessionInfoWritable sessionInfoWritable, ISessionRunner sessionRunner)
        {
            _sessionRunner = sessionRunner;
            _sessionInfoWritable = sessionInfoWritable;
        }
        
        private void Start()
        {
            EmulateUserInput();
        }

        private async void EmulateUserInput()
        {
            _sessionInfoWritable.Configure(new SessionDTO()
            {
                SceneName = SceneName,
            });
            
            //Non-controlled - OKAY
            await UniTask.Delay(3000);

            try
            {
                _sessionRunner.Run();
            }
            catch (NotConfiguredLevelDTOException e)
            {
                Debug.LogError(e.Message);
                //In prod - repeat selection logic
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}