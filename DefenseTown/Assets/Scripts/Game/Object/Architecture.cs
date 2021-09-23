using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public class Architecture : WorldObject
    {
        Coroutine _behaviourCoroutine;

        public override void OnPickStart()
        {
            base.OnPickStart();
        }

        public override void OnPickEnd()
        {
            base.OnPickEnd();
        }

        public override void OnPickUpdate()
        {
            base.OnPickUpdate();
            if(IsPlayerOwner)
                CheckInput();
        }

        public void MoveTo(Vector3 destination)
        {
            if (_behaviourCoroutine != null)
                StopCoroutine(_behaviourCoroutine);
            _behaviourCoroutine = StartCoroutine(CoroutineMove(destination));
        }

        public void RequestCreateTower(int towerID, Cell targetCell)
        {
            if (_behaviourCoroutine != null)
                StopCoroutine(_behaviourCoroutine);
            _behaviourCoroutine = StartCoroutine(CoroutineCreateTower(towerID, targetCell));
        }

        void CheckInput()
        {
            if (Input.GetMouseButtonDown(1) && Game.Instance.UIManager.IsMousePointerOverUI() == false)
            {
                RaycastHit result;
                if (Grid.RaycastToGrid(Camera.main,Input.mousePosition,out result))
                {
                    Cell cell = Game.Instance.World.Grid.GetCell(result.point);
                    if (cell != null)
                    {
                        result.point = new Vector3(result.point.x, transform.position.y, result.point.z);
                        MoveTo(result.point);
                    }
                }
            }
        }

        IEnumerator CoroutineMove(Vector3 destination)
        {
            while(Vector3.Distance(destination,transform.position) >= 0.7f)
            {
                //todo speed Config파일에 정의하기
                transform.position = Vector3.Lerp(transform.position, destination, 2f * Time.deltaTime);
                yield return null;
            }

            _behaviourCoroutine = null;
        }

        IEnumerator CoroutineCreateTower(int towerID, Cell targetCell)
        {
            while (Vector3.Distance(targetCell.Position, transform.position) >= 1f)
            {
                //todo speed Config파일에 정의하기
                transform.position = Vector3.Lerp(transform.position, targetCell.Position, 2f * Time.deltaTime);
                yield return null;
            }

            //todo targetCell 주변 셀도 검사해서 설치 가능한지 판정
            if(Game.Instance.World.Grid.CanCreate(towerID,targetCell))
            {
                Game.Instance.World.CreateBuildTower(towerID, targetCell, TeamType, IsPlayerOwner, (tower) => 
                {
                    
                });
            }

            _behaviourCoroutine = null;
        }
    }
}