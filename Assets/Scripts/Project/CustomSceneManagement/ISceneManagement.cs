using Cysharp.Threading.Tasks;
using Project.Sessions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.CustomSceneManagement
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