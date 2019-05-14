using CoreGraphics;
using Foundation;
using UIKit;

namespace PrototypeControlsSample.iOS.Controls
{
    [Register("RadioButtonView")]
    public class RadioButtonView : UIButton
    {
        public RadioButtonView()
        {
            Initialize();
        }

        public RadioButtonView(CGRect bounds) : base(bounds)
        {
            Initialize();
        }

        public bool Checked
        {
            set { Selected = value; }
            get { return Selected; }
        }

        public string Text
        {
            set { SetTitle(value, UIControlState.Normal); }
        }

        void Initialize()
        {
            AdjustEdgeInsets();
            ApplyStyle();

            TouchUpInside += (sender, args) => Selected = !Selected;
        }

        void AdjustEdgeInsets()
        {
            const float inset = 8f;

            HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            ImageEdgeInsets = new UIEdgeInsets(0f, inset, 0f, 0f);
            TitleEdgeInsets = new UIEdgeInsets(0f, inset * 2, 0f, 0f);
        }

        void ApplyStyle()
        {
            SetImage(UIImage.FromBundle("radio_selected.png"), UIControlState.Selected);
            SetImage(UIImage.FromBundle("radio_unselected.png"), UIControlState.Normal);
        }
    }
}
