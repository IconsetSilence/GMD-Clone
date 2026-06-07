using System.Text.RegularExpressions;
using UnityEngine;

namespace GMDClone.Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        GameObject singletonObject = new();
                        _instance = singletonObject.AddComponent<T>();

                        //When singleton object is not in the scene, there is creating new game object with this component
                        //But game object`s name get the name of the component and the namespace, need only the component`s name
                        //Because there is the regular expressions in order to remove the namespace`s name
                        string pattern = "(GMDClone.Core\\.?)";
                        singletonObject.name = Regex.Replace(typeof(T).ToString(), pattern, string.Empty);
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}