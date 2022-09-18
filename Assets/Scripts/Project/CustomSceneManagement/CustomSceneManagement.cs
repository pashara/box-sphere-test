using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Sessions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.CustomSceneManagement
{
    public class CustomSceneManagement : ISceneManagement
    {
        private class SceneUnloadOperationResult : ISceneUnloadOperationResult
        {
            public bool Result { get; set; }
        }
        private class SceneLoadOperationResult : ISceneLoadOperationResult
        {
            public AsyncOperation Result { get; set; }
            public Scene Scene { get; set; }
        }
        
        public (UniTask, ISceneUnloadOperationResult) UnloadScene(IScene scene)
        {
            var result = new SceneUnloadOperationResult();
            var isProcessed = false;
            UnloadSceneAsync(scene, (r) =>
            {
                result.Result = r;
                isProcessed = true;
            });
            
            return (UniTask.WaitUntil(() => isProcessed), result);
        }

        public (UniTask, ISceneLoadOperationResult) LoadScene(string sceneName)
        {
            var result = new SceneLoadOperationResult();
            var isProcessed = false;
            LoadSceneAsync(sceneName, (r, scene) =>
            {
                result.Result = r;
                result.Scene = scene;
                isProcessed = true;
            });
            
            return (UniTask.WaitUntil(() => isProcessed), result);
        }
        
        private async void UnloadSceneAsync(IScene scene, Action<bool> onComplete)
        {
            try
            {
                var sceneDependencies = new List<UniTask>();
                
                foreach (var linkedScene in scene.LinkedScenes)
                {
                    sceneDependencies.Add(SceneManager.UnloadSceneAsync(linkedScene).ToUniTask());
                }

                await UniTask.WhenAll(sceneDependencies);

                AsyncOperation operation = null;
                do
                {
                    operation = SceneManager.UnloadSceneAsync(scene.Scene);
                    if (operation == null)
                        await UniTask.Yield();
                    else
                        break;
                } while (true);
                
                await operation;

                onComplete?.Invoke(true);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                onComplete?.Invoke(false);
            }
        }
        
        private async void LoadSceneAsync(string sceneName, Action<AsyncOperation, Scene> onComplete)
        {
            AsyncOperation loadSceneOperation = null;
            try
            {
                loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                loadSceneOperation.allowSceneActivation = false;
                
                while (!loadSceneOperation.isDone)
                {
                    if (loadSceneOperation.progress >= 0.9f)
                    {
                        break;
                    }
                    await UniTask.Yield();
                }
                
                Scene loadedScene = default;

                var scenesCount = SceneManager.sceneCount;
                for (int i = 0; i < scenesCount; i++)
                {
                    var scene = SceneManager.GetSceneAt(i);
                    if (scene.name.Equals(sceneName) && !scene.isLoaded)
                    {
                        loadedScene = scene;
                        break;
                    }
                }
                
                onComplete?.Invoke(loadSceneOperation, loadedScene);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                onComplete?.Invoke(loadSceneOperation, default);
            }
        }
    }
}