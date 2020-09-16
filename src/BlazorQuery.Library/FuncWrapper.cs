using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorQuery.Library
{


    public class FuncWrapper<T>
    {
        private readonly Func<T> _func;

        public FuncWrapper(Func<T> func)
        {
            this._func = func;
        }
        // Callbacks
        [JSInvokable]
        public T FuncCallback()
        {
            return _func();
        }
    }

    public class FuncWrapper<T1, TResult>
    {
        private readonly Func<T1, TResult> _func;

        public FuncWrapper(Func<T1, TResult> func)
        {
            this._func = func;
        }
        // Callbacks
        [JSInvokable]
        public TResult ActionCallback(T1 objT1)
        {
            return _func(objT1);
        }
    }

    public class FuncWrapper<T1, T2, TResult>
    {
        private readonly Func<T1, T2, TResult> _func;

        public FuncWrapper(Func<T1, T2, TResult> func)
        {
            this._func = func;
        }
        // Callbacks
        [JSInvokable]
        public TResult ActionCallback(T1 objT1, T2 objT2)
        {
            return _func(objT1, objT2);
        }
    }
}
