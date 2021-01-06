using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Reactive.Disposables;

namespace AppRootStack.ViewModels
{
    public class BaseViewModel : ReactiveObject, IDisposable, IRoutableViewModel
    {
        [Reactive]
        public bool IsBusy { get; set; }

        [Reactive]
        public string Message { get; set; }

        public bool IfNotFirtsTime { get; set; } = false;

        protected readonly CompositeDisposable disposables;

        public BaseViewModel(IScreen hostScreen = null)
        {
            disposables = new CompositeDisposable();

            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

        }

        protected void LogException(Exception ex, string errorMessage)
        {
            this.Log().Write(ex.Message, LogLevel.Error);
            this.Log().Write(ex.StackTrace, LogLevel.Error);
        }

        public void Dispose()
        {
            disposables?.Dispose();
        }

        public string UrlPathSegment
        {
            get;
            protected set;
        }

        public IScreen HostScreen
        {
            get;
            protected set;
        }
    }
}
