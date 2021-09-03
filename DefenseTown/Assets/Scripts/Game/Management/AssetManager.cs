using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.Initialization;
using System;

namespace My.Game
{
    public class AssetManager
    {
        public static void LoadAssetAsync<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            Addressables.LoadAssetAsync<T>(path).Completed += (AsyncOperationHandle<T> result) =>
            {
                callback?.Invoke(result.Result);
            };
        }

        //public static void LoadAsset<T>(string path, Action<T> callback) where T : UnityEngine.Object
        //{
        //  
        //}
    }
}