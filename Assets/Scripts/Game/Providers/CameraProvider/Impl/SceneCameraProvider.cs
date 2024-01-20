using UnityEngine;

namespace Game.Providers.CameraProvider.Impl
{
    public class SceneCameraProvider : ICameraProvider
    {
        private readonly Camera _camera;

        public SceneCameraProvider(Camera camera)
        {
            _camera = camera;
        }
        
        public Camera GetCamera()
        {
            return _camera;
        }
    }
}