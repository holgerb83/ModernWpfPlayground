using System;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace Dialogs
{
    public class MessageBoxViewModel : IDialogAware
    {
        /// <inheritdoc />
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <inheritdoc />
        public void OnDialogClosed()
        {
            
        }

        /// <inheritdoc />
        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        /// <inheritdoc />
        public string Title { get; } = "Hallo Holger";


        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand => _closeDialogCommand ??= new DelegateCommand<string>(CloseDialog);

        private void CloseDialog(string obj)
        {
            if (obj == "true")
            {
                RaiseRequestClose(new DialogResult(ButtonResult.OK));
            }

        }

        /// <inheritdoc />
        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
    }
}