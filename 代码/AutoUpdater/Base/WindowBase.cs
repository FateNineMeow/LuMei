﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ezhu.AutoUpdater.Base
{
    /// <summary>
    /// WindowBase.xaml 的交互逻辑
    /// </summary>
    public partial class WindowBase : Window
    {
        public WindowBase()
        {
            InitializeTheme();
            InitializeStyle();

            Loaded += delegate
            {
                InitializeEvent();
            };
        }

        protected virtual void MinWin()
        {
            WindowState = WindowState.Minimized;
        }

        public Button YesButton
        {
            get;
            set;
        }
        public Button NoButton
        {
            get;
            set;
        }

        private void InitializeEvent()
        {
            ControlTemplate baseWindowTemplate = (ControlTemplate)Application.Current.Resources["BaseWindowControlTemplate"];

            Border borderTitle = (Border)baseWindowTemplate.FindName("borderTitle", this);
            Button closeBtn = (Button)baseWindowTemplate.FindName("btnClose", this);
            Button minBtn = (Button)baseWindowTemplate.FindName("btnMin", this);
            YesButton = (Button)baseWindowTemplate.FindName("btnYes", this);
            NoButton = (Button)baseWindowTemplate.FindName("btnNo", this);

            minBtn.Click += delegate
            {
                MinWin();
            };

            closeBtn.Click += delegate
            {
                this.Close();
            };

            borderTitle.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };


        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        public Canvas GridContent
        {
            get;
            set;
        }


        private void InitializeStyle()
        {
            Style = (Style)Application.Current.Resources["BaseWindowStyle"];
        }

        private void InitializeTheme()
        {
            string themeName = ConfigManage.CurrentTheme;
            Application.Current.Resources.MergedDictionaries.Add(Application.LoadComponent(new Uri(string.Format("../Theme/{0}/WindowBaseStyle.xaml", themeName), UriKind.Relative)) as ResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(Application.LoadComponent(new Uri(string.Format("../Theme/{0}/Style.xaml", themeName), UriKind.Relative)) as ResourceDictionary);
            //App.Current.Resources = Application.LoadComponent(new Uri(string.Format("../Theme/{0}/WindowBaseStyle.xaml", themeName), UriKind.Relative)) as ResourceDictionary;
            //this.Resources = Application.LoadComponent(new Uri(string.Format("../Theme/{0}/Style.xaml", themeName), UriKind.Relative)) as ResourceDictionary;
        }

        private bool _allowSizeToContent = false;
        /// <summary>
        /// 自定义属性，用于标记该窗体是否允许按内容适应，设此属性是为了解决最大化按钮当SizeToContent属性为WidthAndHeight时不能最大化，从而最大、最小化必须变更SizeToContent的值的问题
        /// </summary>
        public bool AllowSizeToContent
        {
            get
            {
                return _allowSizeToContent;
            }
            set
            {
                SizeToContent = (value ? SizeToContent.WidthAndHeight : SizeToContent.Manual);
                _allowSizeToContent = value;
            }
        }
    }
}