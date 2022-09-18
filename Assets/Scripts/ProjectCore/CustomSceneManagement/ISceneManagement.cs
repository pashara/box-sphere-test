using Cysharp.Threading.Tasks;
using ProjectCore.Sessions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectCore.CustomSceneManagement
{
    public interface ISceneUnloadOperationResult
    {
        bool Result { get; }
    }

    public interface ISceneLoadOperationResult
    {
        AsyncOperation Result { get; }
        Scene Scene { get; }
    }
    
    public interface ISceneManagement
    {
        (UniTask, ISceneUnloadOperationResult) UnloadScene(IScene scene);
        (UniTask, ISceneLoadOperationResult) LoadScene(string sceneName);
        
    }
}