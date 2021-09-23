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
            //todo : ���� �����ɶ��� ������ ������ �ʿ��ϴٸ� �ش� �Լ� override�ؼ� ���� ó�� �Ŀ� UIManager�� ���� ��û
            OnClose();
            Game.Instance.UIManager.RemoveUI(PanelType);
        }
        
        //�̸� �������ִ� ��� ȣ��
        public virtual void StaticInit()
        {

        }
    }
}