﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LuMei.TestForm
{
    public partial class MetroProgressbar : UserControl
    {
        #region 成员
        /// <summary>
        /// 动画
        /// </summary>
        private readonly Storyboard _storyboard;

        /// <summary>
        /// 标志动画执行完毕，下一次执行之前是否需要更新动画
        /// </summary>
        private bool _isNeedUpdateStoryboard;
        #endregion

        #region 构造函数
        public MetroProgressbar()
        {
            InitializeComponent();
            _storyboard = FindResource("sbKey") as Storyboard;
            Start();
        }
        #endregion

        #region 公开的方法
        /// <summary>
        /// 使进度条开始滚动
        /// </summary>
        public void Start()
        {
            Visibility = Visibility.Visible;
            UpdateStoryboard();
            _storyboard.Begin(this, true);
        }
        /// <summary>
        /// 使进度条停止滚动
        /// </summary>
        public void Stop()
        {
            Visibility = Visibility.Collapsed;
            _storyboard.Stop(this);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 更新动画部分关键帧
        /// </summary>
        private void UpdateStoryboard()
        {
            //获取控件的实际宽度，减去4个点占用的宽度
            double realWidth = ActualWidth - 36;
            //正向加速度占比47.5%
            double _0Width = realWidth * 0.475;
            //匀速部分占比5%
            double _1Width = _0Width + realWidth * 0.05;

            //根据控件的实际大小动态更改动画
            #region 右边第一个
            var _0 = _storyboard.Children[0] as DoubleAnimationUsingKeyFrames;
            var _0_1 = _0.KeyFrames[1] as EasingDoubleKeyFrame;
            var _0_2 = _0.KeyFrames[2] as EasingDoubleKeyFrame;
            var _0_3 = _0.KeyFrames[3] as EasingDoubleKeyFrame;
            var _0_4 = _0.KeyFrames[4] as EasingDoubleKeyFrame;

            _0_1.Value = _0Width;
            _0_2.Value = _1Width;
            _0_3.Value = realWidth;
            _0_4.Value = realWidth;
            #endregion

            #region 右边倒数第二个
            var _1 = _storyboard.Children[1] as DoubleAnimationUsingKeyFrames;
            var _1_1 = _1.KeyFrames[1] as EasingDoubleKeyFrame;
            var _1_2 = _1.KeyFrames[2] as EasingDoubleKeyFrame;
            var _1_3 = _1.KeyFrames[3] as EasingDoubleKeyFrame;
            var _1_4 = _1.KeyFrames[4] as EasingDoubleKeyFrame;

            _1_1.Value = _0Width;
            _1_2.Value = _1Width;
            _1_3.Value = realWidth;
            _1_4.Value = realWidth;
            #endregion

            #region 右边倒数第三个
            var _2 = _storyboard.Children[2] as DoubleAnimationUsingKeyFrames;
            var _2_1 = _2.KeyFrames[1] as EasingDoubleKeyFrame;
            var _2_2 = _2.KeyFrames[2] as EasingDoubleKeyFrame;
            var _2_3 = _2.KeyFrames[3] as EasingDoubleKeyFrame;
            var _2_4 = _2.KeyFrames[4] as EasingDoubleKeyFrame;

            _2_1.Value = _0Width;
            _2_2.Value = _1Width;
            _2_3.Value = realWidth;
            _2_4.Value = realWidth;
            #endregion

            #region 右边倒数第四个
            var _3 = _storyboard.Children[3] as DoubleAnimationUsingKeyFrames;
            var _3_1 = _3.KeyFrames[1] as EasingDoubleKeyFrame;
            var _3_2 = _3.KeyFrames[2] as EasingDoubleKeyFrame;
            var _3_3 = _3.KeyFrames[3] as EasingDoubleKeyFrame;
            var _3_4 = _3.KeyFrames[4] as EasingDoubleKeyFrame;

            _3_1.Value = _0Width;
            _3_2.Value = _1Width;
            _3_3.Value = realWidth;
            _3_4.Value = realWidth;
            #endregion
        }
        #endregion

        #region 事件处理
        /// <summary>
        /// 动画执行完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            //每次动画执行完毕后判断是否需要更新动画
            if(_isNeedUpdateStoryboard)
            {
                UpdateStoryboard();
            }

            _isNeedUpdateStoryboard = false;
            _storyboard.Begin(this);
        }

        /// <summary>
        /// 当控件的大小发生变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //当控件的宽度发生变化时
            if(e.WidthChanged)
            {
                _isNeedUpdateStoryboard = true;
            }
        }
        #endregion
    }
}
