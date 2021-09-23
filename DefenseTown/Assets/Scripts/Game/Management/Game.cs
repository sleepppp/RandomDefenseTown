using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.UI;
    public class Game : MonoBehaviourSingleton<Game>
    {
        public World World;

        public DataTableManager DataTableManager;

        public UIManager UIManager;

        public RoutineManager RoutineManager;

        MemoryManager _memoryManager;

        public MemoryManager MemoryManager
        {
            get
            {
                if (_memoryManager == null)
                    _memoryManager = new MemoryManager();
                return _memoryManager;
            }
        }
        private void Awake()
        {
            GameStart();
        }

        private void Update()
        {
            RoutineManager.GameUpdate();

            if (World != null)
                World.GameUpdate();
        }

        public void GameStart()
        {
            if (RoutineManager == null)
                RoutineManager = new RoutineManager();
            if (DataTableManager == null)
                DataTableManager = new DataTableManager();
            //todo SceneTransision 생기면 월드는 나중에 초기화
            if (World == null)
                World = FindObjectOfType<World>();
            if (UIManager == null)
                UIManager = new UIManager();
            if (_memoryManager == null)
                _memoryManager = new MemoryManager();

            DataTableManager.Init();
            World.Init();
            UIManager.Init();
        }
    }
}