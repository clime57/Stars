using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Stars
{
    public class TaskManager : GameSubSystem
    {
        override public void update(float time)
        {
            for (int i = 0; i < _updateList.Count; ++i)
            {
                if (!(_updateList[i].doUpdate(time)))
                {
                    _removeList.Add(_updateList[i]);
                }
            }
            for (int j = 0; j < _removeList.Count; ++j)
            {
                _updateList.Remove(_removeList[j]);

            }
            _removeList.Clear();
        }

        public void addTask(Task task)
        {
            task.enable();
            if (task.isUpdating())
            {
                _updateList.Add(task);
            }
            _disableList.Add(task);
        }

        public void removeTask(Task task)
        {
            task.disable();
            _updateList.Remove(task);
            _disableList.Remove(task);
        }

        public void clearAll()
        {
            for (int i = 0; i < _disableList.Count; ++i)
            {
                _disableList[i].disable();
            }
            _disableList.Clear();
            _updateList.Clear();
            _removeList.Clear();
        }

        List<Task> _updateList = new List<Task>();
        List<Task> _disableList = new List<Task>();
        List<Task> _removeList = new List<Task>();
    };


}