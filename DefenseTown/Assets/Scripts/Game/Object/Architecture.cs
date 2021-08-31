using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public class Architecture : WorldObject
    {
        Coroutine _moveCoroutine;

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
            CheckInput();
        }

        public void MoveTo(Vector3 destination)
        {
            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(CoroutineMove(destination));
        }

        void CheckInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit result;
                //if (Physics.Raycast(ray, out result, 100f, 1 << LayerMask.NameToLayer("Grid")))
                if (Physics.Raycast(ray, out result, 100f))
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

            _moveCoroutine = null;
        }
    }
}