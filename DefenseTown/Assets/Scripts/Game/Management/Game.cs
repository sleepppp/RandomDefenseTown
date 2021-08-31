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
            //todo 임시. .추 후 SceneTransition 작업하면 변경
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