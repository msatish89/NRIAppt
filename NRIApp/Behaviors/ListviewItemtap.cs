using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using System.Reflection;

namespace NRIApp.Behaviors
{
   public class ListviewItemtap: Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(ListviewItemtap));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemTapped += ListView_ItemSelected;
            bindable.BindingContextChanged += ListView_BindingContextChanged;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= ListView_ItemSelected;
            bindable.BindingContextChanged -= ListView_BindingContextChanged;
        }

        private void ListView_BindingContextChanged(object sender, EventArgs eventArgs)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
            BindingContext = listView?.BindingContext;
        }

        private void ListView_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
            Command.Execute(e.Item);
           
        }

        private void OnItemTaped(object sender, ItemTappedEventArgs e)
        {
            Command.Execute(e.Item);
            //var listView = sender as ListView;
            //var vm = listView.BindingContext as MainViewModel;
            //vm.SelectCommand.Execute(null);
        }
    }





    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
  
}

