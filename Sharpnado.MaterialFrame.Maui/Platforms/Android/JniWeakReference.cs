﻿using System;

namespace Sharpnado.MaterialFrame.Maui.Droid
{
    internal class JniWeakReference<T>
        where T : Java.Lang.Object
    {
        private readonly WeakReference<T> _reference;

        public JniWeakReference(T target)
        {
            _reference = new WeakReference<T>(target);
        }

        public bool TryGetTarget(out T target)
        {
            target = null;
            if (_reference.TryGetTarget(out var innerTarget))
            {
                if (innerTarget.Handle != IntPtr.Zero)
                {
                    target = innerTarget;
                }
            }

            return target != null;
        }

        public override string ToString()
        {
            return $"[JniWeakReference] {_reference}";
        }
    }
}