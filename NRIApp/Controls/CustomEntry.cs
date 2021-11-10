using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace NRIApp.Controls
{
    public class CustomEntry : Entry
    {
        public new event EventHandler Completed;

        public static readonly BindableProperty ReturnKeyTypeProperty =
            BindableProperty.Create(nameof(CustomEntry), typeof(ReturnKeyTypes), typeof(CustomEntry), ReturnKeyTypes.Next);

        public ReturnKeyTypes ReturnKeyType
        {
            get
            {
                return (ReturnKeyTypes)GetValue(ReturnKeyTypeProperty);
            }
            set
            {
                SetValue(ReturnKeyTypeProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty =
           BindableProperty.Create(nameof(BorderColor), typeof(Xamarin.Forms.Color), typeof(CustomEntry), Xamarin.Forms.Color.Transparent, BindingMode.TwoWay);

        public Xamarin.Forms.Color BorderColor
        {
            get { return (Xamarin.Forms.Color)GetValue(BorderColorProperty); }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty HideBorderProperty =
            BindableProperty.Create(nameof(CustomEntry), typeof(bool), typeof(CustomEntry), default(bool));

        public bool HideBorder
        {
            get
            {
                return (bool)GetValue(HideBorderProperty);
            }
            set
            {
                SetValue(HideBorderProperty, value);
            }
        }

        public static readonly BindableProperty ShowToolbarProperty =
            BindableProperty.Create(nameof(CustomEntry), typeof(bool), typeof(CustomEntry), default(bool));

        public bool ShowToolbar
        {
            get
            {
                return (bool)GetValue(ShowToolbarProperty);
            }
            set
            {
                SetValue(ShowToolbarProperty, value);
            }
        }

        public static readonly BindableProperty ToolbarReturnKeyTypeProperty =
            BindableProperty.Create(nameof(CustomEntry), typeof(ReturnKeyTypes), typeof(CustomEntry), ReturnKeyTypes.Next);

        public ReturnKeyTypes ToolbarReturnKeyType
        {
            get
            {
                return (ReturnKeyTypes)GetValue(ToolbarReturnKeyTypeProperty);
            }
            set
            {
                SetValue(ToolbarReturnKeyTypeProperty, value);
            }
        }

        public static readonly BindableProperty NextViewProperty =
        BindableProperty.Create("NextView", typeof(View), typeof(CustomEntry));

        public View NextView
        {

            get { return (View)GetValue(NextViewProperty); }
            set { SetValue(NextViewProperty, value); }
        }

        public void OnNext()
        {
            if (ReturnKeyType == ReturnKeyTypes.Next)
                NextView?.Focus();
            if (ReturnKeyType == ReturnKeyTypes.Done)
                NextView?.Unfocus();
        }

        //public void OnNextUnfocus()
        // {
        //     NextView?.Unfocus();
        // }
        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("EntryCommand", typeof(ICommand), typeof(CustomEntry), null);
        public ICommand EntryCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }
        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
            NextView?.Unfocus();
        }






        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomEntry), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomEntry), null);
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(CustomEntry), null);
        public static readonly BindableProperty InputConverterParameterProperty = BindableProperty.Create(nameof(ConverterParameter), typeof(object), typeof(CustomEntry), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        public object ConverterParameter
        {
            get { return GetValue(InputConverterParameterProperty); }
            set { SetValue(InputConverterParameterProperty, value); }
        }

        public async Task<bool> Execute(object sender, object parameter)
        {
            if (Command == null)
            {
                return false;
            }

            object resolvedParameter;
            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(parameter, typeof(object), ConverterParameter, null);
            }
            else
            {
                resolvedParameter = parameter;
            }

            if (!Command.CanExecute(resolvedParameter))
            {
                return false;
            }

            Command.Execute(resolvedParameter);
            return true;
        }
    }

    public enum ReturnKeyTypes : int
    {
        Go,
        Next,
        Search,
        Send,
        Done,
        Continue
    }

}

