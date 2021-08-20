using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace My.Core
{
    using My.Game;
    [Serializable]
    public class Muid
    {
        [SerializeField] WorldObjectType _objectType;
        [NonSerialized] Guid _guid;

        public Muid(WorldObjectType type)
        {
            _objectType = type;
            _guid = Guid.NewGuid();
        }

        public Muid()
        {
            _objectType = WorldObjectType.None;
            _guid = Guid.NewGuid();
        }

        public bool IsValid()
        {
            return _guid == Guid.Empty;
        }

        public static bool operator ==(Muid left, Muid right)
        {
            return object.ReferenceEquals(left, right);
        }

        public static bool operator !=(Muid left, Muid right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Muid);
        }
        public bool Equals(Muid obj)
        {
            return object.ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}


