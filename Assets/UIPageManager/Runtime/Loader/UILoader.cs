using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIPackage.UI
{
    [AddComponentMenu("UIPackage/UILoader")]
    public class UILoader : MonoBehaviour
    {
        #region fields
        [SerializeField]
        [Tooltip("Scene UI to load additive")]
        private string _sceneUI = "UI";


        [SerializeField]
        [Tooltip("Unload Scene UI when this game object is destroyed")]
        private bool _unloadOnDestroy = true;
        #endregion

        #region private
        private bool _isLoaded;
        #endregion

        #region methods
        private void Awake()
        {
            SceneManager.LoadScene(_sceneUI, LoadSceneMode.Additive);
            _isLoaded = true;
        }
        private void OnDestroy()
        {
            if (_isLoaded && _unloadOnDestroy)
            {
                SceneManager.UnloadSceneAsync(_sceneUI);
            }
        }

        public void GetUICamera()
        {

        }
        #endregion
    }
}
