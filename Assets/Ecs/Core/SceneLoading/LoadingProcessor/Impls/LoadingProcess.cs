using System;
using System.Collections;
using Ecs.Core.SceneLoading.SceneLoadingManager;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.LoadingProcessor.Impls
{
    public class LoadingProcess : Process, IProgressable
    {
        public float Progress => _operation?.progress ?? 0;

        private readonly string _levelName;
        private readonly LoadSceneMode _mode;
        private AsyncOperation _operation;
        private Action _complete;

        public LoadingProcess(ELevelName levelName, LoadSceneMode mode)
        {
            _levelName = levelName.ToString();
            _mode = mode;
        }
        
        public LoadingProcess(string levelName, LoadSceneMode mode)
        {
            _levelName = levelName;
            _mode = mode;
        }

        public override void Do(Action complete)
        {
            _complete = complete;
            _operation = SceneManager.LoadSceneAsync(_levelName, _mode);
            Observable.FromCoroutine(() => VerifySceneLoad(_operation))
                .Subscribe(_ => _complete());
        }

        private IEnumerator VerifySceneLoad(AsyncOperation operation)
        {
            operation.allowSceneActivation = true;
            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
}