using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public class WorldObjectPickSystem
    {
        WorldObject _pickObject;

        public WorldObject PickObject { get { return _pickObject; } }

        public void GameUpdate()
        {
            if(Input.GetMouseButtonDown(0) && Game.Instance.UIManager.IsMousePointerOverUI() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layerMask = 1 << LayerMask.NameToLayer("WorldObject");
                RaycastHit result;
                if(Physics.Raycast(ray, out result,100f,layerMask))
                {
                    WorldObject worldObject = result.collider.GetComponent<WorldObject>();
                    ChangePick(worldObject);
                }
            }

            _pickObject?.OnPickUpdate();
        }

        public void ChangePick(WorldObject newPick)
        {
            if (_pickObject == newPick)
                return;

            _pickObject?.OnPickEnd();
            _pickObject = newPick;
            _pickObject?.OnPickStart();
        }
    }
}
