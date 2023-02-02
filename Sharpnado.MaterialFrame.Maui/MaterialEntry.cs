using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpnado.MaterialFrame.Maui
{
    public class MaterialEntry : Entry
    {
        /// <summary>
        /// Color of bottom border.
        /// </summary>
        public static BindableProperty UnderlineColorProperty = BindableProperty.Create(
                nameof(UnderlineColor), typeof(Color), typeof(MaterialEntry), Colors.Black);
        public Color UnderlineColor
        {
            get => (Color)GetValue(UnderlineColorProperty);
            set => SetValue(UnderlineColorProperty, value);
        }

        /// <summary>
        /// Thickness of bottom border.
        /// </summary>
        public static BindableProperty UnderlineThicknessProperty = BindableProperty.Create(
                nameof(UnderlineThickness), typeof(int), typeof(MaterialEntry), 0);
        public int UnderlineThickness
        {
            get => (int)GetValue(UnderlineThicknessProperty);
            set => SetValue(UnderlineThicknessProperty, value);
        }

        public MaterialEntry()
        {
        }
    }
}
