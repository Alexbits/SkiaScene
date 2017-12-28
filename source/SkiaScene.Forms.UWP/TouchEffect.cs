﻿using SkiaScene.UWP;
using System.Linq;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("SkiaScene")]
[assembly: ExportEffect(typeof(SkiaScene.Forms.UWP.TouchEffect), "TouchEffect")]
namespace SkiaScene.Forms.UWP
{
    public class TouchEffect : PlatformEffect
    {
        private TouchHandler _touchHandler;
        private FrameworkElement _frameworkElement;
        private NetStandard.TouchEffect _touchEffect;

        protected override void OnAttached()
        {
            _frameworkElement = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the PCL
            _touchEffect =
                (SkiaScene.Forms.NetStandard.TouchEffect)Element.Effects.FirstOrDefault(e => e is SkiaScene.Forms.NetStandard.TouchEffect);

            if (_touchEffect == null)
            {
                return;
            }

            _touchHandler = new TouchHandler();
            _touchHandler.TouchAction += TouchHandlerOnTouch;
            _touchHandler.Capture = _touchEffect.Capture;
            _touchHandler.RegisterEvents(_frameworkElement);

        }

        private void TouchHandlerOnTouch(object sender, TouchTracking.TouchActionEventArgs args)
        {
            _touchEffect.OnTouchAction(sender, args);
        }

        protected override void OnDetached()
        {
            if (_touchHandler == null)
            {
                return;
            }
            _touchHandler.TouchAction -= TouchHandlerOnTouch;
            _touchHandler.UnregisterEvents(_frameworkElement);
        }
    }
}
