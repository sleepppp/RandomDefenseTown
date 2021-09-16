using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public class MemoryManager
    {
        public bool IsTracking = false;

        public void Destroy(GameObject gameobject)
        {
#if UNITY_EDITOR
            if(IsTracking)
            {
                Debug.Log("Destroy : " + gameobject.name);
            }
#endif

            GameObject.Destroy(gameobject);
        }

        public GameObject Instantiate(GameObject prefab)
        {
#if UNITY_EDITOR
            if (IsTracking)
            {
                Debug.Log("Instantiate : " + prefab.name);
            }
#endif

            return Instantiate(prefab);
        }

        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
#if UNITY_EDITOR
            if (IsTracking)
            {
                Debug.Log("Instantiate : " + prefab.name);
            }
#endif

            return Instantiate(prefab, parent);
        }

        public GameObject Instantiate()
        {
#if UNITY_EDITOR
            if (IsTracking)
            {
                Debug.Log("Instantiate EmptyGameObject ");
            }
#endif

            return new GameObject();
        }
    }
}
