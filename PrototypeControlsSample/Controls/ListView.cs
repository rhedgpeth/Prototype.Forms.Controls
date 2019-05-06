using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace PrototypeControlsSample.Controls
{
    public class ListView : Xamarin.Forms.ListView
    {
        // Helper to keep track of what was last visible in the list
        int lastPosition;

        IList itemsSource;

        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(ListView));

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public static readonly BindableProperty LoadMoreItemsCommandProperty =
            BindableProperty.Create(nameof(LoadMoreItemsCommand), typeof(ICommand), typeof(ListView));

        public ICommand LoadMoreItemsCommand
        {
            get { return (ICommand)GetValue(LoadMoreItemsCommandProperty); }
            set { SetValue(LoadMoreItemsCommandProperty, value); }
        }

        public ListView()
        {
            ItemTapped += OnItemTapped;
            ItemAppearing += OnItemAppearing;
        }

        public ListView(ListViewCachingStrategy strategy) : base(Device.RuntimePlatform.Equals("iOS")
                                                                 ? ListViewCachingStrategy.RetainElement : strategy)
        {
            ItemTapped += OnItemTapped;
            ItemAppearing += OnItemAppearing;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                itemsSource = ItemsSource as IList;

                if (itemsSource == null)
                {
                    throw new Exception($"{nameof(ListView)} requires that {nameof(ItemsSource)} be of type IList");
                }
            }
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && ItemTappedCommand != null && ItemTappedCommand.CanExecute(e))
            {
                ItemTappedCommand.Execute(e.Item);
                SelectedItem = null;
            }
        }

        void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            int position = itemsSource?.IndexOf(e.Item) ?? 0;

            if (itemsSource != null)
            {
                // PreloadIndex should never end up to be equal to itemsSource. Count otherwise
                // LoadMoreItems would not be called
                if (PreloadCount <= 0)
                {
                    PreloadCount = 1;
                }

                int preloadIndex = Math.Max(itemsSource.Count - PreloadCount, 0);

                if ((position > lastPosition || (position == itemsSource.Count - 1)) && (position >= preloadIndex))
                {
                    lastPosition = position;
                    LoadMoreItems();
                }
            }
        }

        void LoadMoreItems()
        {
            if (LoadMoreItemsCommand != null && LoadMoreItemsCommand.CanExecute(null))
            {
                LoadMoreItemsCommand.Execute(null);
                SelectedItem = null;
            }
        }

        /// <summary>
        /// Identifies the <see cref="PreloadCount"/> bindable property.
        /// </summary>
        public static readonly BindableProperty PreloadCountProperty =
          BindableProperty.Create(nameof(PreloadCount), typeof(int), typeof(ListView), 0);

        /// <summary>
        /// How many cells before the end of the ListView before incremental loading should start. 
        /// Defaults to 0, meaning the end of the list has to be reached before it will try to load more. 
        /// This is a bindable property.
        /// </summary>
        public int PreloadCount
        {
            get { return (int)GetValue(PreloadCountProperty); }
            set { SetValue(PreloadCountProperty, value); }
        }
    }
}

