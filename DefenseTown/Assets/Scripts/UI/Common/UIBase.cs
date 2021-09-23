using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.UI
{
    using My.Game;
    public class UIBase : MonoBehaviour
    {
        public UIPanelType PanelType;

        protected virtual void OnClose()
        {

        }

        public virtual void RequestCloseUI()
        {
            //todo : 만약 삭제될때에 별도의 연출이 필요하다면 해당 함수 override해서 연출 처리 후에 UIManager에 삭제 요청
            OnClose();
            Game.Instance.UIManager.RemoveUI(PanelType);
        }
        
        //미리 생성되있는 경우 호출
        public virtual void StaticInit()
        {

        }
    }
}