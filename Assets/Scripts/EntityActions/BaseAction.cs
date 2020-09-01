using System.Collections;
using SwaPPawS.Entities;
using UniRx;
using UnityEngine.Events;

namespace SwaPPawS.EntityActions
{
    public class BaseAction
    {
        public Entity Entity;

        private UnityAction<Entity> _onCompleteAction;        
        private bool _activated;
        public bool Activated { get => _activated; set => _activated = value; }

        public virtual void OnComplete(UnityAction<Entity> action)
        {
            _onCompleteAction = action;
        }

        public virtual IEnumerator Action(){
            yield return null;
        }

        public virtual BaseAction StartAction(Entity entity)
        {
            _activated = true;
            Entity = entity;
            Observable.FromCoroutine(Action).Subscribe(x => Complete());
            return this;
        }

        protected virtual void Complete()
        {
            _activated = false;
            if (_onCompleteAction != null)
            {
                _onCompleteAction.Invoke(Entity);
            }
        }
    }
}