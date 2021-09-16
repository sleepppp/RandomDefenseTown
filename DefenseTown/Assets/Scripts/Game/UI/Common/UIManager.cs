using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace My.UI
{
    public class UIManager
    {
        Dictionary<UIPanelType, UIBase> _uiList = new Dictionary<UIPanelType, UIBase>();
        List<UIPanelType> _uiHistory = new List<UIPanelType>();

        public void AddUI(UIPanelType uiType,UIBase ui)
        {
            if (_uiList.ContainsKey(uiType))
                return;

            ui.PanelType = uiType;

            _uiHistory.Add(uiType);
            _uiList.Add(uiType, ui);
        }

        public void RemoveUI(UIPanelType uiType)
        {
            if (_uiList.ContainsKey(uiType) == false)
                return;

            UIBase ui = _uiList.TryGetValue(uiType);
            ui.OnClose();

            MonoBehaviour.Destroy(ui.gameObject);

            _uiHistory.Remove(uiType);
            _uiList.Remove(uiType);
        }

        public void RangeRemoveUI(UIPanelType targetType)
        {
            if (_uiHistory.Contains(targetType) == false)
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

        public bool IsMousePointerOverUI()
        {
            if (EventSystem.current == null)
                return false;
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}