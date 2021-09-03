using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Core
{
    public static class ObjectFinder
    {
        public static List<GameObject> FindObjectsWithLayer(this GameObject root,int layer)
        {
            List<GameObject> result = new List<GameObject>();

            Transform[] arrChild = root.GetComponentsInChildren<Transform>();
            for(int i =0; i < arrChild.Length; ++i)
            {
                if(arrChild[i].gameObject.layer == layer)
                {
                    result.Add(arrChild[i].gameObject);
                }
            }

            return result;
        }

        public static void FindObjectsWithLayer(this GameObject root, int layer, List<GameObject> result,System.Func<GameObject, bool> ignore = null)
        {
            Transform[] arrChild = root.GetComponentsInChildren<Transform>();
            for (int i = 0; i < arrChild.Length; ++i)
            {
                if (arrChild[i].gameObject.layer == layer)
                {
                    if(ignore != null)
                    {
                        if (ignore.Invoke(arrChild[i].gameObject) == true)
                            continue;
                    }

                    result.Add(arrChild[i].gameObject);
                }
            }
        }
    }
}
