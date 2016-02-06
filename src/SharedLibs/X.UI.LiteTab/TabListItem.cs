﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace X.UI.LiteTab
{
    public sealed class TabListItem : Control
    {
        Button _tb;


        



        public TabListItem()
        {
            this.DefaultStyleKey = typeof(TabListItem);
   
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _tb = GetTemplateChild("tabButton") as Button;
            

            _tb.Click += _tb_Click;

            var p1 = GetTabListParent(_tb);
            _tb.BorderBrush = p1.TabItemBorderColor;
            

        }

        private TabList GetTabListParent(DependencyObject ctrl) {

            var p1 = VisualTreeHelper.GetParent(ctrl);
            var p2 = VisualTreeHelper.GetParent(p1);
            var p3 = VisualTreeHelper.GetParent(p2);
            var p4 = VisualTreeHelper.GetParent(p3);
            var p5 = VisualTreeHelper.GetParent(p4);
            var p6 = VisualTreeHelper.GetParent(p5) as TabList;

            return p6;
        }


        private void _tb_Click(object sender, RoutedEventArgs e)
        {
            //removed attached property as this was causing issues when there are MULTIPLE TabLists on a page ... 
            //being static it affects ALL tablist's

            var dc = (Tab)((Button)sender).DataContext;
            dc.IsSelected = true;


            //get TabList parent and call SelectedTabChanged 
            var p1  = GetTabListParent((DependencyObject)sender);
            p1.ChangeSelectedTab(dc);

        }


        


    }



}


