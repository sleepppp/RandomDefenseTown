using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.UI
{
    //todo : UIBase ��� ���� �гε��� ���� �̰����� �����ϰ� �� ó��
    public partial class UIManager
    {
        public void AsyncCreateMainHUD()
        {
            string path = "Assets/Deploy/Game/UI/MainHUD.prefab";
            CreateUI<MainHUD>(path, UIPanelType.MainHUD, (ui) => 
            {
                ui.Init();
            });
        }
    }
}