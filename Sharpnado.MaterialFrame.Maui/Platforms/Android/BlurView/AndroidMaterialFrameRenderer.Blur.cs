using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Microsoft.Maui.Platform;
using Sharpnado.MaterialFrame.Maui.Droid.BlurView;

namespace Sharpnado.Presentation.Forms.Droid.Renderers.MaterialFrame.Maui
{
    public partial class AndroidMaterialFrameRenderer : FrameRenderer
    {
        private BlockingBlurController _blurController;
        Context _context;

        public AndroidMaterialFrameRenderer(Context context) : base(context)
        {
            _context = context;
        }

        private bool IsBlurInitialized => _blurController != null;

        public override void Draw(Canvas canvas)
        {
            if (!IsBlurInitialized)
            {
                base.Draw(canvas);
                return;
            }

            bool shouldDraw = _blurController.Draw(canvas);
            if (shouldDraw)
            {
                base.Draw(canvas);
            }
        }

        private void DestroyBlur()
        {
            _blurController?.Destroy();
            _blurController = null;

            ViewTreeObserver.RemoveOnPreDrawListener(null);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            if (!IsBlurInitialized)
            {
                return;
            }

            _blurController.UpdateBlurViewSize();
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();

            if (!IsBlurInitialized)
            {
                return;
            }

            _blurController.SetBlurAutoUpdate(false);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            if (!IsBlurInitialized)
            {
                return;
            }

            if (!IsHardwareAccelerated)
            {
                Console.WriteLine("Error: BlurView can't be used in not hardware-accelerated window!");
                return;
            }

            _blurController.SetBlurAutoUpdate(true);
        }

        private bool InitializeBlurIfNeeded()
        {
            if (_blurController != null)
            {
                return false;
            }

            var decorView = _context.GetActivity().Window.DecorView;
            var rootView = decorView.FindViewById<ViewGroup>(Android.Resource.Id.Content);

            var newController = new BlockingBlurController(_context, this, rootView);
            _blurController = newController;
            return true;
        }

        private void EnableBlur()
        {
            InitializeBlurIfNeeded();

            _blurController.SetBlurEnabled(true);
        }

        private void DisableBlur()
        {
            _blurController?.SetBlurEnabled(false);

            DestroyBlur();
        }
    }
}