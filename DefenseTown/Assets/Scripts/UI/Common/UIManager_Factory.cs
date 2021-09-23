using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.UI
{
    //todo : UIBase 상속 받은 패널들은 전부 이곳에서 생성하게 끔 처리
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