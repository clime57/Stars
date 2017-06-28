using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Stars
{
    public class Task
    {
        protected Task()
        {
        }

        public virtual bool enable()
        {
            _isEnable = true;
            return _isEnable;
        }
        public virtual void disable()
        {
            _isEnable = false;
        }
        public bool doUpdate(float interval)
        {
            bool ret = update(interval);
            if (!ret)
            {
                _isUpdating = false;
            }
            return ret;
        }

        public virtual bool update(float interval)
        {
            return false;
        }

        public virtual bool isEnable()
        {
            return _isEnable;
        }

        public virtual bool isUpdating()
        {
            return _isUpdating;
        }

        public bool _isEnable = false;
        public bool _isUpdating = true;


    };

}