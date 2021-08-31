using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Core;
    using My.UI;
    public class Game : MonoBehaviourSingleton<Game>
    {
        public World World;

        public DataTableManager DataTableManager;

        public UIManager UIManager;

        private void Awake()
        {
            //todo �ӽ�. .�� �� SceneTransition �۾��ϸ� ����
            SceneStart();
        }

        public void SceneStart()
        {
            if (DataTableManager == null)
                DataTableManager = new DataTableManager();
            if (World == null)
                World = FindObjectOfType<World>();
            if (UIManager == null)
                UIManager = new UIManager();

            DataTableManager.Init();
            World.Init();
        }
    }
}