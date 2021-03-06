﻿using CoreLib.Effects;
using Microsoft.UI.Composition.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace X.Viewer.SketchFlow.Controls
{
    public sealed partial class PageLayout : UserControl
    {
        public event EventHandler PerformAction;

        private ContainerVisual _shadowContainer;
        private Compositor _compositor;
        private DropShadow _shadow;
        private SpriteVisual _spriteVisual;

        public PageLayout()
        {
            this.InitializeComponent();
        }

        private void pgLayer_LayerChanged(object sender, EventArgs e)
        {
            var plea = e as PageLayerEventArgs;
            var page = this.DataContext as SketchPage;
            page.ExternalPC("Layers");

            PerformAction?.Invoke(null, plea);
        }
        

        private void grdPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var nm = (string)((Grid)sender).Tag;
            PerformAction?.Invoke(this, new PageLayoutEventArgs() { ActionType = nm + "PageLayoutStarted", StartPoint = e.GetCurrentPoint(null) });
            PerformAction?.Invoke(this, new PageLayoutEventArgs() { ActionType = "PageSelected", StartPoint = e.GetCurrentPoint(null) });
        }

        private void grdPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var nm = (string)((Grid)sender).Tag;
            PerformAction?.Invoke(this, new PageLayoutEventArgs() { ActionType = nm + "PageLayoutFinished", StartPoint = e.GetCurrentPoint(null) });
            
        }

        private void grdPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ((Grid)sender).Opacity = 1;
        }

        private void grdPointerExited(object sender, PointerRoutedEventArgs e)
        {
            ((Grid)sender).Opacity = 0.3;
        }

        private void layoutRoot_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            PerformAction?.Invoke(this, new PageLayoutEventArgs() { ActionType = "PageSelected", StartPoint = e.GetCurrentPoint(null) });
        }

        public object FindContentElementByName(string name) {
            return pc.FindContentElementByName(name);
        }

        private void layoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //Currently there's no true "mixing" where you could directly host XAML content within a Composition Visual.
            //https://github.com/Microsoft/WindowsUIDevLabs/issues/68
            //InitShadow();

            this.UpdateLayout();
            DrawGridLines(this.ActualWidth, this.ActualHeight, 50, 50);
        }

        private void DrawGridLines(double width, double height, double overhangX, double overhangY, double spacingX = 10, double spacingY = 10) {
            if (width == 0 || height == 0) return;
            
            PathFigureCollection myPathFigureCollection = new PathFigureCollection();

            //horizontal
            var max = ((height + overhangY) / spacingY);
            for (int y = 0; y < max; y++) {
                
                var yPos = y * spacingY;
                LineSegment myLineSegment = new LineSegment();
                PathFigure myPathFigure = new PathFigure();
                PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
                myPathFigure.StartPoint = new Point(0, yPos);
                myLineSegment.Point = new Point(width + overhangX, yPos);
                myPathSegmentCollection.Add(myLineSegment);
                myPathFigure.Segments = myPathSegmentCollection;
                
                if (y > 0 && y < max-1) myPathFigureCollection.Add(myPathFigure);
                
            }

            //vertical
            max = ((width + overhangX) / spacingX);
            for (int x = 0; x < max; x++)
            {
                var xPos = x * spacingX;
                LineSegment myLineSegment = new LineSegment();
                PathFigure myPathFigure = new PathFigure();
                PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
                myPathFigure.StartPoint = new Point(xPos, 0);
                myLineSegment.Point = new Point(xPos, height + overhangY);
                myPathSegmentCollection.Add(myLineSegment);
                myPathFigure.Segments = myPathSegmentCollection;
                
                if (x > 0 && x < max-1) myPathFigureCollection.Add(myPathFigure);
            }



            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;
            
            pthGridLines.Stroke = new SolidColorBrush(Colors.CornflowerBlue);
            pthGridLines.StrokeThickness = 1;
            pthGridLines.Data = myPathGeometry;
        }

        private void InitShadow()
        {
            grdLowerLayer.UpdateLayout();

            _shadowContainer = CompositionManager.GetVisual(grdLowerLayer);
            _shadowContainer.Size = new Vector2((float)grdLowerLayer.ActualWidth, (float)grdLowerLayer.ActualHeight);
            _shadowContainer.Offset = new Vector3(0, 0, 0);
            
            _compositor = CompositionManager.GetCompositor(_shadowContainer);

            _spriteVisual = _compositor.CreateSpriteVisual();
            _spriteVisual.Size = new Vector2((float)grdLowerLayer.ActualWidth + 10, (float)grdLowerLayer.ActualHeight + 10);
            _spriteVisual.Offset = new Vector3(-5, -5, 0);

            // Add drop shadow to image visual
            _shadow = _compositor.CreateDropShadow();
            _shadow.Offset = new System.Numerics.Vector3(0, 0, 0);
            _shadow.Color = Windows.UI.Colors.Black;
            _shadow.Opacity = 0.3f;
            _spriteVisual.Shadow = _shadow;

            _shadowContainer.Children.InsertAtBottom(_spriteVisual);
        }

        private void layoutRoot_Unloaded(object sender, RoutedEventArgs e)
        {

            _compositor = null;

            _shadow?.Dispose();
            _shadow = null;

            _shadowContainer?.Dispose();
            _shadowContainer = null;


        }
    }

    public class PageLayoutEventArgs: EventArgs
    {
        public string ActionType;
        public PointerPoint StartPoint;
    }
}


//https://github.com/Microsoft/WindowsUIDevLabs/issues/27
//http://www.slideshare.net/WindowsDev/build-2016-p490-beyond-the-effectbrush-with-windows-ui
//http://blog.robmikh.com/xaml/uwp/composition/2016/04/14/introduction-to-composition.html