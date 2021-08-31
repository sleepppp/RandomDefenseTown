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

        public RoutineManager RoutineManager;

        private void Awake()
        {
            //todo 임시. .추 후 SceneTransition 작업하면 변경
            SceneStart();
        }

        private void Update()
        {
            RoutineManager.GameUpdate();

            if (World != null)
                World.GameUpdate();
        }

        public void SceneStart()
        {
            if (RoutineManager == null)
                RoutineManager = new RoutineManager();
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