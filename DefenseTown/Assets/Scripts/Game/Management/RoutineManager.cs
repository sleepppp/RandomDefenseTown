using System;
using System.Collections.Generic;
using System.Collections;

namespace My.Game
{
    public class RoutineManager
    {
        List<IEnumerator> _updator = new List<IEnumerator>();

        public void GameUpdate()
        {
            for (int i = 0; i < _updator.Count; ++i)
            {
                if (_updator[i].MoveNext() == false)
                {
                    _updator.RemoveAt(i);
                    --i;
                }
            }
        }

        public IEnumerator StartRoutine(IEnumerator routine)
        {
            _updator.Add(routine);
            return routine;
        }

        public void RemoveRoutine(IEnumerator routine)
        {
            for (int i = 0; i < _updator.Count; ++i)
            {
                if (_updator[i] == routine)
                {
                    _updator.RemoveAt(i);
                    break;
                }
            }
        }

        public bool IsValid(IEnumerator handler)
        {
            for (int i = 0; i < _updator.Count; ++i)
            {
                if (_updator[i] == handler)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
