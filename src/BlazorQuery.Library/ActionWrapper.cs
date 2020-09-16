using Microsoft.JSInterop;
using System;

namespace BlazorQuery.Library
{
    public class ActionWrapper<T>
    {
        private readonly Action<T> _action;

        public ActionWrapper(Action<T> action)
        {
            this._action = action;
        }
        // Callbacks
        [JSInvokable]
        public void ActionCallback(T obj)
        {
            _action(obj);
        }
    }

    public class ActionWrapper<T1, T2>
    {
        private readonly Action<T1, T2> _action;

        public ActionWrapper(Action<T1, T2> action)
        {
            this._action = action;
        }
        // Callbacks
        [JSInvokable]
        public void ActionCallback(T1 objT1, T2 objT2)
        {
            _action(objT1, objT2);
        }
    }

    public class ActionWrapper<T1,T2,T3>
    {
        private readonly Action<T1, T2, T3> _action;

        public ActionWrapper(Action<T1, T2, T3> action)
        {
            this._action = action;
        }
        // Callbacks
        [JSInvokable]
        public void ActionCallback(T1 objT1, T2 objT2, T3 objT3)
        {
            _action(objT1, objT2, objT3);
        }
    }

}
