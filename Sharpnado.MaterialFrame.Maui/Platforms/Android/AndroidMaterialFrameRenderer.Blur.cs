﻿using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace Sharpnado.MaterialFrame.Maui.Droid
{
    public partial class AndroidMaterialFrameRenderer
    {
        private const double StyledBlurRadius = 64;

        private static readonly Color DarkBlurOverlayColor = Color.FromHex("#80000000");

        private static readonly Color LightBlurOverlayColor = Color.FromHex("#40FFFFFF");

        private static readonly Color ExtraLightBlurOverlayColor = Color.FromHex("#B0FFFFFF");

        private static int blurAutoUpdateDelayMilliseconds = 100;
        private static int blurProcessingDelayMilliseconds = 10;

        private RealtimeBlurView _realtimeBlurView;

        private View _blurRootView;

        /// <summary>
        /// When a page visibility changes we activate or deactivate blur updates.
        /// Setting a bigger delay could improve performance and rendering.
        /// </summary>
        public static int BlurAutoUpdateDelayMilliseconds
        {
            get => blurAutoUpdateDelayMilliseconds;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        "The blur processing delay cannot be negative",
                        nameof(BlurAutoUpdateDelayMilliseconds));
                }

                blurAutoUpdateDelayMilliseconds = value;
            }
        }

        /// <summary>
        /// Sometimes the computation of the background can take some times (svg images for example).
        /// Setting a bigger delay to be sure that the background is rendered first can fix some glitches.
        /// </summary>
        public static int BlurProcessingDelayMilliseconds
        {
            get => blurProcessingDelayMilliseconds;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        "The blur processing delay cannot be negative",
                        nameof(BlurProcessingDelayMilliseconds));
                }

                blurProcessingDelayMilliseconds = value;
            }
        }

        /// <summary>
        /// If set to <see langword="true"/>, the rendering result could be better (clearer blur not mixing front elements).
        /// However due to a bug in the Xamarin framework https://github.com/xamarin/xamarin-android/issues/4548, debugging is impossible with this mode (causes SIGSEGV).
        /// A suggestion would be to set it to false for debug, and to true for releases.
        /// </summary>
        public static bool ThrowStopExceptionOnDraw { get; set; } = false;

        private bool IsAndroidBlurPropertySet => MaterialFrame.AndroidBlurRadius > 0;

        private double CurrentBlurRadius =>
            IsAndroidBlurPropertySet ? MaterialFrame.AndroidBlurRadius : StyledBlurRadius;

        private string FormsId => MaterialFrame.StyleId;

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (MaterialFrame.AndroidBlurRootElement != null && _blurRootView == null)
            {
                UpdateAndroidBlurRootElement();
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            InternalLogger.Info(FormsId, $"Renderer::OnSizeChanged(w: {w}, h: {h}, oldw: {oldw}, oldh: {oldh})");
            LayoutBlurView();
        }

        private void LayoutBlurView()
        {
            if (Width == 0 || Height == 0 || _realtimeBlurView == null)
            {
                return;
            }
      
            int width = Width;
            int height = Height;

            InternalLogger.Info(FormsId, $"Renderer::LayoutBlurView(element: {MaterialFrame.StyleId} | {width}x{height} | {MeasuredWidth}x{MeasuredHeight})");

            _realtimeBlurView.Measure(width,height);
            _realtimeBlurView.Layout(0, 0, width, height);
        }

        private void DestroyBlur()
        {
            if (_realtimeBlurView.IsNullOrDisposed())
            {
                return;
            }

            RemoveView(_realtimeBlurView);

            _realtimeBlurView.Destroy();
            _realtimeBlurView = null;
        }

        private void UpdateAndroidBlurRootElement()
        {
            if (MaterialFrame.AndroidBlurRootElement == null)
            {
                return;
            }

            var formsView = MaterialFrame.AndroidBlurRootElement;
            var renderer = Microsoft.Maui.Controls.Compatibility.Platform.Android.Platform.GetRenderer(formsView);
            if (renderer == null)
            {
                return;
            }

            bool IsAncestor(Element child, Layout parent)
            {

                if (child.Parent == null)
                {
                    return false;
                }

                if (child.Parent == parent)
                {
                    return true;
                }

                return IsAncestor(child.Parent, parent);
            }

            if (!IsAncestor(MaterialFrame, MaterialFrame.AndroidBlurRootElement))
            {
                throw new InvalidOperationException(
                    "The AndroidBlurRootElement of the MaterialFrame should be an ancestor of the MaterialFrame.");
            }

            Microsoft.Maui.Controls.Compatibility.Platform.Android.Platform.SetRenderer(formsView, renderer);
            _blurRootView = renderer.View;

            _realtimeBlurView?.SetRootView(_blurRootView);
        }

        private void UpdateAndroidBlurOverlayColor(bool invalidate = true)
        {
            if (IsAndroidBlurPropertySet)
            {
                InternalLogger.Info(FormsId, "UpdateAndroidBlurOverlayColor()");
                _realtimeBlurView?.SetOverlayColor(MaterialFrame.AndroidBlurOverlayColor.ToAndroid(), invalidate);
            }
        }

        private void UpdateAndroidBlurRadius(bool invalidate = true)
        {
            if (IsAndroidBlurPropertySet)
            {
                InternalLogger.Info(FormsId, "Renderer::UpdateAndroidBlurRadius()");
                _realtimeBlurView?.SetBlurRadius(_context.ToPixels(MaterialFrame.AndroidBlurRadius), invalidate);
            }
        }

        private void UpdateMaterialBlurStyle(bool invalidate = true)
        {
            if (_realtimeBlurView == null || IsAndroidBlurPropertySet)
            {
                return;
            }

            InternalLogger.Info(FormsId, $"Renderer::UpdateMaterialBlurStyle({MaterialFrame.MaterialBlurStyle})");

            _realtimeBlurView.SetBlurRadius(_context.ToPixels(StyledBlurRadius), invalidate);

            switch (MaterialFrame.MaterialBlurStyle)
            {
                case MaterialFrame.BlurStyle.ExtraLight:
                    _realtimeBlurView.SetOverlayColor(ExtraLightBlurOverlayColor.ToAndroid(), invalidate);
                    break;
                case MaterialFrame.BlurStyle.Dark:
                    _realtimeBlurView.SetOverlayColor(DarkBlurOverlayColor.ToAndroid(), invalidate);
                    break;

                default:
                    _realtimeBlurView.SetOverlayColor(LightBlurOverlayColor.ToAndroid(), invalidate);
                    break;
            }
        }

        private void EnableBlur()
        {
            InternalLogger.Info(FormsId, "Renderer::EnableBlur()");

            if (_realtimeBlurView == null)
            {
                _realtimeBlurView = new RealtimeBlurView(_context, MaterialFrame.StyleId);
            }

            UpdateAndroidBlurRadius();
            UpdateAndroidBlurOverlayColor();
            UpdateMaterialBlurStyle();
            UpdateAndroidBlurRootElement();

            _realtimeBlurView.SetDownsampleFactor(CurrentBlurRadius <= 10 ? 1 : 2);

            UpdateCornerRadius();

            if (ChildCount > 0 && ReferenceEquals(GetChildAt(0), _realtimeBlurView))
            {
                // Already added
                return;
            }

            InternalLogger.Info(FormsId, "Renderer::EnableBlur() => adding pre draw listener");
            AddView(
                _realtimeBlurView,
                0,
                new FrameLayout.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent,
                    GravityFlags.NoGravity));

            LayoutBlurView();
        }

        private void DisableBlur()
        {
            if (ChildCount == 0 || !ReferenceEquals(GetChildAt(0), _realtimeBlurView))
            {
                return;
            }

            InternalLogger.Info(FormsId, "Renderer::DisableBlur() => removing pre draw listener");
            RemoveView(_realtimeBlurView);
        }
    }
}