using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Core;
    public class Game : MonoBehaviourSingleton<Game>
    {
        public World World;

        public DataTableManager DataTableManager;

        private void Start()
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

            DataTableManager.Init();
            World.Init();
        }
    }
}