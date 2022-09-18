using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ProjectCore.Sessions
{
    public interface IScene
    {
        Scene Scene { get; }
        List<Scene> LinkedScenes { get; }
    }
    
    public class SceneProvider : IScene
    {
        public Scene Scene { get; }
        public List<Scene> LinkedScenes { get; } = new();

        public SceneProvider(Scene scene)
        {
            Scene = scene;
        }
    }
}
