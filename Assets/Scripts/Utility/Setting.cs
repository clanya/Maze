using UnityEngine;

//���̃N���X�͒P��ӔC�ł��ĂȂ�����
namespace Utility
{
    public class Setting : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas = null;
        public bool clickedEsc => Input.GetKeyDown(KeyCode.Escape);

        private void Awake()
        {
            _canvas.enabled = false;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (clickedEsc)
            {
                _canvas.enabled = true;
            }

        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_WEBGL || UNITY_STANDALONE)
        Application.Quit();
#endif
        }
        public void BackToGame()
        {
            _canvas.enabled = false;
        }
    }
}