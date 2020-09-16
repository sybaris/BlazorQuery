using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorQuery.Library
{

    //public class B
    //{
    //    public void toto()
    //    {

    //    }
    //}


    //public static class A
    //{
    //    public static void µ(string s)
    //    {

    //    }

    //    public static B µ()
    //    {
    //        return null;
    //    }
    //}




    public class BlazorQueryDOM : ComponentBase
    {
        public IJSRuntime JSRuntimeInstance { get; private set; }

        public List<BlazorQueryDOMElement> Elements { get; private set; }
        public string CurrentSelector { get; private set; }

        public BlazorQueryDOM(IJSRuntime JSRuntime)
        {
            this.JSRuntimeInstance = JSRuntime;
            Elements = new List<BlazorQueryDOMElement>();
        }

        public async Task<BlazorQueryDOM> Select(string selector)
        {
            BlazorQueryDOM dom = new BlazorQueryDOM(JSRuntimeInstance);
            dom.CurrentSelector = selector;
            var data = await JSRuntimeInstance.InvokeAsync<string>(BlazorQueryList.Select, selector);
            dom.Elements = JsonSerializer.Deserialize<List<BlazorQueryDOMElement>>(data);

            return dom;
        }

        // Utilities
        public async Task Alert(string message) => await JSRuntimeInstance.InvokeAsync<Task>(BlazorQueryList.Utils_Alert, message);
        public async Task ConsoleLog(string message) => await JSRuntimeInstance.InvokeAsync<Task>(BlazorQueryList.Utils_ConsoleLog, message);

        // Functions - Actions
        public async Task<BlazorQueryDOM> Text(Func<int, string, string> func)
        {
            var actionWrapper = new FuncWrapper<int, string, string>(func);
            var dotNetObjectReference = DotNetObjectReference.Create(actionWrapper);
            await JSRuntimeInstance.InvokeAsync<string>(BlazorQueryList.Text_Func, CurrentSelector, dotNetObjectReference);
            return this;
        }

        public async Task<BlazorQueryDOM> FadeOut(Action<string> completed)
        {
            var actionWrapper = new ActionWrapper<string>(completed);
            var dotNetObjectReference = DotNetObjectReference.Create(actionWrapper);
            await JSRuntimeInstance.InvokeAsync<Task>(BlazorQueryList.FadeOut, CurrentSelector, dotNetObjectReference);
            return this;
        }

        public async Task<BlazorQueryDOM> Load(string url, object data, Action<string, string, object> complete)
        {
            var actionWrapper = new ActionWrapper<string, string, object>(complete);
            var dotNetObjectReference = DotNetObjectReference.Create(actionWrapper);
            await JSRuntimeInstance.InvokeAsync<Task>(BlazorQueryList.Load, CurrentSelector, url, data, dotNetObjectReference);
            return this;
        }
  
    }

    public static class BlazorQueryDOMHelpers
    {
        #region private methods
        private async static Task<BlazorQueryDOM> JsInvokeChain(Task<BlazorQueryDOM> dom, string identifier, params object[] args)
        {
            var d = (await dom);
            var newArgs = args.Prepend(d.CurrentSelector).ToArray();
            await d.JSRuntimeInstance.InvokeAsync<Task>(identifier, newArgs);
            return d;
        }

        private async static Task<T> JsInvokeEndChain<T>(Task<BlazorQueryDOM> dom, string identifier, params object[] args)
        {
            var d = (await dom);
            var newArgs = args.Prepend(d.CurrentSelector).ToArray();
            return await d.JSRuntimeInstance.InvokeAsync<T>(identifier, newArgs);
        }
        #endregion private methods

        // Functions - Actions
        public async static Task<BlazorQueryDOM> AddClass(this Task<BlazorQueryDOM> dom, string className) => await JsInvokeChain(dom, BlazorQueryList.AddClass, className);
        public async static Task<BlazorQueryDOM> RemoveClass(this Task<BlazorQueryDOM> dom, string className) => await JsInvokeChain(dom, BlazorQueryList.RemoveClass, className);
        public async static Task<BlazorQueryDOM> CSS(this Task<BlazorQueryDOM> dom, string style, string styleValue) => await JsInvokeChain(dom, BlazorQueryList.CSS, style, styleValue);
        public async static Task<BlazorQueryDOM> Height(this Task<BlazorQueryDOM> dom, int height) => await JsInvokeChain(dom, BlazorQueryList.Height_Set, height);
        public async static Task<BlazorQueryDOM> Width(this Task<BlazorQueryDOM> dom, int width) => await JsInvokeChain(dom, BlazorQueryList.Width_Set, width);
        public async static Task<BlazorQueryDOM> Text(this Task<BlazorQueryDOM> dom, string text) => await JsInvokeChain(dom, BlazorQueryList.Text_Set, text);
        
        // Signature with delegates
        public async static Task<BlazorQueryDOM> FadeOut(this Task<BlazorQueryDOM> dom, Action<string> completed)
        {
            return await (await dom).FadeOut(completed);
        }
        public async static Task<BlazorQueryDOM> Load(this Task<BlazorQueryDOM> dom, string url, object data, Action<string, string, object> complete)
        {
            return await (await dom).Load(url, data, complete);
        }
        public async static Task<BlazorQueryDOM> Text(this Task<BlazorQueryDOM> dom, Func<int, string, string> func)
        {
            return await (await dom).Text(func);
        }

        // Functions - Chain-enders
        public async static Task<int> Height(this Task<BlazorQueryDOM> dom) => await JsInvokeEndChain<int>(dom, BlazorQueryList.Height_Get);
        public async static Task<int> Width(this Task<BlazorQueryDOM> dom) => await JsInvokeEndChain<int>(dom, BlazorQueryList.Width_Get);
        public async static Task<string> Text(this Task<BlazorQueryDOM> dom) => await JsInvokeEndChain<string>(dom, BlazorQueryList.Text_Get);
    }
}
