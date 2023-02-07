using CommunityToolkit.Maui.Behaviors;
using Sharpnado.MaterialFrame.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpnado.MaterialFrame.Maui
{
    public class TintColorBehavior : Behavior<Image>
    {
        public static readonly BindableProperty AttachBehaviorProperty =
       BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(TintColorBehavior), false, propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            var image = view as Image;
            if (image == null)
                return;

            bool attachBehavior = (bool)newValue;

            if (attachBehavior)
            {
                image.Behaviors.Add(new IconTintColorBehavior() { TintColor = MaterialContent.Instance.TabIconColor });
            }
            else
            {
                var toRemove = image.Behaviors.FirstOrDefault(b => b is IconTintColorBehavior);
                if (toRemove != null)
                {
                    image.Behaviors.Remove(toRemove);
                }
            }
        }
    }
}
