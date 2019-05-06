using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrototypeControlsSample.Controls
{
    public class TappableLabel : Label
    {
        public EventHandler Tapped;

        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(ListView));

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public TappableLabel()
        {
            var tappedGesture = new TapGestureRecognizer
            {
                Command = new Command(OnLabelTapped)
            };

            tappedGesture.Tapped += TappedGesture_Tapped;

            GestureRecognizers.Add(tappedGesture);
        }

        void OnLabelTapped()
        {
            if (TappedCommand != null && TappedCommand.CanExecute(null))
            {
                TappedCommand.Execute(null);
            }
        }

        void TappedGesture_Tapped(object sender, EventArgs e) => Tapped?.Invoke(sender, e);
    }
}
