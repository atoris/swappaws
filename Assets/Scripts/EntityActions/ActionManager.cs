using System;
using System.Collections.Generic;
using SwaPPawS.Entities;
using UnityEngine;

namespace SwaPPawS.EntityActions
{

    public class ActionManager : MonoBehaviour
    {

        public List<Entity> Entities;

        private Dictionary<Entity, List<BaseAction>> _actions;
        

        private void Awake()
        {
            _actions = new Dictionary<Entity, List<BaseAction>>();
            Entities = new List<Entity>();
        }

        public List<Entity> AddEntity(Entity entity){
            
            Entities.Add(entity);

            return Entities;
        }

        public Entity AddAction(Entity entity, BaseAction action, bool forced = false)
        {
            if(forced){
                action.StartAction(entity);
                return entity;
            }

            if(!_actions.ContainsKey(entity)){
                _actions.Add(entity, new List<BaseAction>());
            }

            _actions[entity].Add(action);

            OnStartAction(entity);

            return entity;
        }

        public bool HasActive(Entity entity)
        {
            return _actions.Count > 0 && _actions[entity] != null && _actions[entity].Count > 0 && _actions[entity][0].Activated;
        }

        private void OnCompleteAction(Entity entity)
        {
            _actions[entity].RemoveAt(0);
            if (_actions[entity].Count > 0)
            {
                OnStartAction(entity);
            }
        }

        private void OnStartAction(Entity entity)
        {
            if (!HasActive(entity))
            {
                _actions[entity][0].StartAction(entity).OnComplete(OnCompleteAction);
            }

        }
    }
}