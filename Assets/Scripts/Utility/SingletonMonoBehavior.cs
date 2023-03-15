using UnityEngine;

namespace Utility
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance is null)
                {
                    var t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance is null)
                    {
                        Debug.LogWarning(t + " をアタッチしているGameObjectはありません");
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            if (this != Instance)
            {
                Destroy(gameObject);
                Debug.LogWarning(
                    typeof(T) +
                    " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                    " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}