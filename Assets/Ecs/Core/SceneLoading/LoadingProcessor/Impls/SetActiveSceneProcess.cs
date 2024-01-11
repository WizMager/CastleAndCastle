using System;
using Ecs.Core.SceneLoading.SceneLoadingManager;
using UnityEngine.SceneManagement;

namespace Core.LoadingProcessor.Impls
{
    public class SetActiveSceneProcess : Process, IProgressable
    {
        private readonly string _levelName;
        public float Progress => .5f;

        public SetActiveSceneProcess(ELevelName levelName)
        {
            _levelName = levelName.ToString();
        }
        
        public SetActiveSceneProcess(string levelName)
        {
            _levelName = levelName;
        }

        public override void Do(Action onComplete)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_levelName));
            onComplete();
        }
    }
}