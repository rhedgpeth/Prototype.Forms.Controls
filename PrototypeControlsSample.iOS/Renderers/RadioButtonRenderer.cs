using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using PrototypeControlsSample.Controls;
using PrototypeControlsSample.iOS.Controls;
using PrototypeControlsSample.iOS.Renderers;
using UIKit;
using Xamarin.Forms;

using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RadioButton), typeof(RadioButtonRenderer))]
namespace PrototypeControlsSample.iOS.Renderers
{
    /// <summary>
    /// The Radio button renderer for iOS.
    /// </summary>
    public class RadioButtonRenderer : ViewRenderer<RadioButton, RadioButtonView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<RadioButton> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                BackgroundColor = Element.BackgroundColor.ToUIColor();

                if (Control == null)
                {
                    var checkBox = new RadioButtonView(Bounds);

                    checkBox.TouchUpInside += (s, args) => Element.Checked = Control.Checked;

                    SetNativeControl(checkBox);
                }
            }

            if (e?.NewElement != null)
            {
                Control.VerticalAlignment = UIControlContentVerticalAlignment.Top;
                Control.Text = e.NewElement.Text;
                Control.Checked = e.NewElement.Checked;
                Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Normal);
                Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Selected);
            }
        }

        void ResizeText()
        {
            var text = Element.Text;

            var bounds = Control.Bounds;

            var width = Control.TitleLabel.Bounds.Width;

            var height = GetStringHeight(text, Control.Font, (float)width);

            var minHeight = GetStringHeight(string.Empty, Control.Font, (float)width);

            var requiredLines = Math.Round(height / minHeight, MidpointRounding.AwayFromZero);

            var supportedLines = Math.Round(bounds.Height / minHeight, MidpointRounding.ToEven);

            if (supportedLines != requiredLines)
            {
                bounds.Height += (float)(minHeight * (requiredLines - supportedLines));

                Control.Bounds = bounds;

                Element.HeightRequest = bounds.Height;
            }
        }

        float GetStringHeight(string text, UIFont font, float width)
        {
            var nativeString = new NSString(text);

            var rect = nativeString.GetBoundingRect(
                new System.Drawing.SizeF(width, float.MaxValue),
                NSStringDrawingOptions.UsesLineFragmentOrigin,
                new UIStringAttributes() { Font = font },
                null);

            return (float)rect.Height;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            ResizeText();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;
                case "TextColor":
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Normal);
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Selected);
                    break;
                case "Element":
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
                    return;
            }
        }
    }
}