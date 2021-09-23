using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
namespace My.UI
{
    using My.Game;
    public partial class UIManager
    {
        public Canvas MainCanvas;

        Dictionary<UIPanelType, UIBase> _uiList = new Dictionary<UIPanelType, UIBase>();
        List<UIPanelType> _uiHistory = new List<UIPanelType>();

        public void Init()
        {
            //todo 추 후 멀티 캔버스 사용하면 변경
            if (MainCanvas == null)
                MainCanvas = GameObject.FindObjectOfType<Canvas>();

            UIBase[] arrUIBase = MainCanvas.GetComponentsInChildren<UIBase>();
            for(int i = 0; i < arrUIBase.Length; ++i)
            {
                arrUIBase[i].StaticInit();
                AddUI(arrUIBase[i].PanelType, arrUIBase[i]);
            }
        }

        public void CreateUI<T>(string path,UIPanelType type, Action<T> callback) where T : UIBase
        {
            if(HasUI(type))
                return;

            AssetManager.LoadAssetAsync<GameObject>(path, (prefab) => 
            {
                CreateUI<T>(prefab, type, callback);
            });
        }

        public void CreateUI<T>(GameObject prefab,UIPanelType type,Action<T> callback) where T : UIBase
        {
            if (HasUI(type))
                return;

            GameObject newUI = Game.Instance.MemoryManager.Instantiate(prefab, MainCanvas.transform);
            UIBase uiBase = newUI.GetComponent<UIBase>();

            AddUI(type, uiBase);

            callback?.Invoke(uiBase as T);
        }

        public void RemoveUI(UIPanelType uiType)
        {
            UIBase ui = _uiList.TryGetValue(uiType);

            if (ui == null)
                return;

            MonoBehaviour.Destroy(ui.gameObject);

            _uiHistory.Remove(uiType);
            _uiList.Remove(uiType);
        }

        public void RangeRemoveUI(UIPanelType targetType)
        {
            if (HasUI(targetType) == false)
                return;

            for(int i = _uiHistory.Count - 1; i >= 0; --i)
            {
                UIPanelType type = _uiHistory[i];
                RemoveUI(type);
                if (type != targetType)
                {
                    break;
                }
            }
        }

        public T GetUI<T>(UIPanelType type) where T : UIBase
        {
            T result = _uiList.TryGetValue(type) as T;
            return result;
        }

        public bool HasUI(UIPanelType type)
        {
            return _uiList.ContainsKey(type);
        }

        public bool IsMousePointerOverUI()
        {
            if (EventSystem.current == null)
                return false;
            return EventSystem.current.IsPointerOverGameObject();
        }

        void AddUI(UIPanelType uiType, UIBase ui)
        {
            if (HasUI(uiType))
                return;

            ui.PanelType = uiType;

            _uiHistory.Add(uiType);
            _uiList.Add(uiType, ui);
        }
    }
}