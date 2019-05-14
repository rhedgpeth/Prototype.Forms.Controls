using Android.Content;
using Android.Widget;
using PrototypeControlsSample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PrototypeControlsSample.Controls.RadioButton), typeof(RadioButtonRenderer))]
namespace PrototypeControlsSample.Droid.Renderers
{
    public class RadioButtonRenderer : ViewRenderer<Controls.RadioButton, RadioButton>
    {
        public RadioButtonRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.RadioButton> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged += ElementOnPropertyChanged;
            }

            if (Control == null)
            {
                var radButton = new RadioButton(Context);

                radButton.CheckedChange += RadioButton_CheckedChange;

                SetNativeControl(radButton);
            }

            Control.Text = e.NewElement.Text;
            Control.Checked = e.NewElement.Checked;

            Element.PropertyChanged += ElementOnPropertyChanged;
        }

        void RadioButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e) => Element.Checked = e.IsChecked;

        void ElementOnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;
            }
        }
    }
}
