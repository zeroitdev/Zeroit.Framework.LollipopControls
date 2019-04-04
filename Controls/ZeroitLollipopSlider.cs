// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopSlider.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.LollipopControls.Controls
{
    /// <summary>
    /// Class ZeroitLollipopSlider.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="Zeroit.Framework.LollipopControls.Controls.IMaterialControl" />
    [Designer(typeof(LollipopSliderDesigner))]
    public partial class ZeroitLollipopSlider : Control, IMaterialControl
    {

        #region Private Fields
        /// <summary>
        /// The line color
        /// </summary>
        Color lineColor = Color.FromArgb((int)(2.55 * 30), 255, 255, 255);
        /// <summary>
        /// The circle color
        /// </summary>
        Color circleColor = Color.DeepPink;
        /// <summary>
        /// The minimum value color
        /// </summary>
        private Color minValColor = Color.Gray;
        /// <summary>
        /// The fill color
        /// </summary>
        Color fillColor = Color.RoyalBlue;
        /// <summary>
        /// The outer circle alpha
        /// </summary>
        private int outerCircleAlpha = 100;
        /// <summary>
        /// The value
        /// </summary>
        private float _Value = 50;
        /// <summary>
        /// The maximum value
        /// </summary>
        private float _MaxValue = 100;
        /// <summary>
        /// The minimum value
        /// </summary>
        private float _MinValue = 0;
        /// <summary>
        /// The mouse pressed
        /// </summary>
        private bool MousePressed;
        /// <summary>
        /// The mouse x
        /// </summary>
        private int MouseX;
        /// <summary>
        /// The indicator size
        /// </summary>
        private int IndicatorSize;
        /// <summary>
        /// The hovered
        /// </summary>
        private bool hovered = false;
        /// <summary>
        /// The indicator rectangle
        /// </summary>
        private Rectangle IndicatorRectangle;
        /// <summary>
        /// The indicator rectangle normal
        /// </summary>
        private Rectangle IndicatorRectangleNormal;
        /// <summary>
        /// The indicator rectangle pressed
        /// </summary>
        private Rectangle IndicatorRectanglePressed;
        /// <summary>
        /// The indicator rectangle disabled
        /// </summary>
        private Rectangle IndicatorRectangleDisabled;

        /// <summary>
        /// The show maximum minimum
        /// </summary>
        private bool showMaxMin = false;
        /// <summary>
        /// The show percentage
        /// </summary>
        private bool showPercentage = true;

        /// <summary>
        /// The post fix
        /// </summary>
        private string postFix = "%";
        /// <summary>
        /// The line size
        /// </summary>
        private float lineSize = 1;
        /// <summary>
        /// The circle reduction
        /// </summary>
        private int circleReduction = 6;

        private Orientation orientation = Orientation.Horizontal;

        /// <summary>
        /// The spacing
        /// </summary>
        private int spacing = 10;
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                orientation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
        {
            get { return lineColor; }
            set
            {
                lineColor = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        [Browsable(false)]
        public int Depth { get; set; }

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>The state of the mouse.</value>
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        /// <summary>
        /// Delegate ValueChanged
        /// </summary>
        /// <param name="newValue">The new value.</param>
        [Browsable(false)]
        public delegate void ValueChanged(int newValue);
        /// <summary>
        /// Occurs when [on value changed].
        /// </summary>
        public event ValueChanged onValueChanged;

        /// <summary>
        /// Gets or sets a value indicating whether [show maximum minimum].
        /// </summary>
        /// <value><c>true</c> if [show maximum minimum]; otherwise, <c>false</c>.</value>
        public bool ShowMaxMin
        {
            get { return showMaxMin; }
            set
            {
                showMaxMin = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show percentage].
        /// </summary>
        /// <value><c>true</c> if [show percentage]; otherwise, <c>false</c>.</value>
        public bool ShowPercentage
        {
            get { return showPercentage; }
            set
            {
                showPercentage = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public float Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        MouseX = (int)((double)_Value / (double)(MaxValue - MinValue) * (double)(Width - IndicatorSize));
                        break;
                    case Orientation.Vertical:
                        MouseX = (int)((double)_Value / (double)(MaxValue - MinValue) * (double)(Height - IndicatorSize));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                RecalcutlateIndicator();
                Invalidate();

            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public float MaxValue
        {
            get { return _MaxValue; }
            set
            {
                _MaxValue = value;
                MouseX = (int)((double)_Value / (double)(MaxValue - MinValue) * (double)(Width - IndicatorSize));
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public float MinValue
        {
            get { return _MinValue; }
            set
            {
                _MinValue = value;
                MouseX = (int)((double)_Value / (double)(MaxValue - MinValue) * (double)(Width - IndicatorSize));
            }
        }

        /// <summary>
        /// Gets or sets the color of the circle.
        /// </summary>
        /// <value>The color of the circle.</value>
        public Color CircleColor
        {
            get { return circleColor; }
            set
            {
                circleColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the outer circle alpha.
        /// </summary>
        /// <value>The outer circle alpha.</value>
        public int OuterCircleAlpha
        {
            get { return outerCircleAlpha; }
            set
            {
                outerCircleAlpha = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public Color FillColor
        {
            get { return fillColor; }
            set
            {
                fillColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the minimum color of the value.
        /// </summary>
        /// <value>The minimum color of the value.</value>
        public Color MinValColor
        {
            get => minValColor;
            set { minValColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the post fix.
        /// </summary>
        /// <value>The post fix.</value>
        public string PostFix
        {
            get { return postFix; }
            set { postFix = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the line.
        /// </summary>
        /// <value>The size of the line.</value>
        public float LineSize
        {
            get { return lineSize; }
            set { lineSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the circle reduction.
        /// </summary>
        /// <value>The circle reduction.</value>
        public int CircleReduction
        {
            get { return circleReduction; }
            set { circleReduction = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the spacing.
        /// </summary>
        /// <value>The spacing.</value>
        public int Spacing
        {
            get { return spacing; }
            set
            {
                spacing = value;
                RecalcutlateIndicator();
                Invalidate();
            }
        }

        
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopSlider"/> class.
        /// </summary>
        public ZeroitLollipopSlider()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            
            SetStyle(ControlStyles.Selectable, true);
            IndicatorSize = 30;
            //MaxValue = 100;
            Width = 80;
            //MinValue = 0;
            Height = IndicatorSize + 10;

            //Value = 50;

            IndicatorRectangle = new Rectangle(0, 10, IndicatorSize, IndicatorSize);
            IndicatorRectangleNormal = new Rectangle();
            IndicatorRectanglePressed = new Rectangle();

            EnabledChanged += MaterialSlider_EnabledChanged;

            DoubleBuffered = true;

            ForeColor = Color.Black;

        }

        #endregion

        #region Overrides and Private Methods

        /// <summary>
        /// Handles the EnabledChanged event of the MaterialSlider control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void MaterialSlider_EnabledChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    Height = IndicatorSize + Spacing;
                    MouseX = (int)((double)_Value / (double)(MaxValue - MinValue) * (double)(Width - IndicatorSize));
                    RecalcutlateIndicator();
                    break;
                case Orientation.Vertical:
                    Width = IndicatorSize + Spacing + 10;
                    MouseX = (int)((double)_Value / (double)(MaxValue - MinValue) * (double)(Height - IndicatorSize));
                    RecalcutlateIndicator();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            hovered = true;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            hovered = false;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            switch (Orientation)
            {
                case Orientation.Horizontal:

                    if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Y > IndicatorRectanglePressed.Top && e.Y < IndicatorRectanglePressed.Bottom)
                    {
                        MousePressed = true;
                        if (e.X >= IndicatorSize / 2 && e.X <= Width - IndicatorSize / 2)
                        {
                            MouseX = e.X - IndicatorSize / 2;
                            double ValuePerPx = ((double)(MaxValue - MinValue)) / (Width - IndicatorSize);
                            int v = (int)(ValuePerPx * MouseX);
                            if (v != _Value)
                            {
                                _Value = v;
                                if (onValueChanged != null) onValueChanged((int)_Value);
                            }
                            RecalcutlateIndicator();
                        }
                    }


                    break;
                case Orientation.Vertical:

                    if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Y > IndicatorRectanglePressed.Top && e.Y < IndicatorRectanglePressed.Bottom)
                    {
                        MousePressed = true;
                        if (e.Y >= IndicatorSize / 2 && e.Y <= Height - IndicatorSize / 2)
                        {
                            MouseX = e.Y - IndicatorSize / 2;
                            double ValuePerPx = ((double)(MaxValue - MinValue)) / (Height - IndicatorSize);
                            int v = (int)(ValuePerPx * MouseX);
                            if (v != _Value)
                            {
                                _Value = v;
                                if (onValueChanged != null) onValueChanged((int)_Value);
                            }
                            RecalcutlateIndicator();
                        }
                    }


                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            hovered = true;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hovered = false;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MousePressed = false;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    if (MousePressed)
                    {
                        if (e.X >= IndicatorSize / 2 && e.X <= Width - IndicatorSize / 2)
                        {
                            MouseX = e.X - IndicatorSize / 2;
                            double ValuePerPx = ((double)(MaxValue - MinValue)) / (Width - IndicatorSize);
                            int v = (int)(ValuePerPx * MouseX);
                            if (v != _Value)
                            {
                                _Value = v;
                                if (onValueChanged != null) onValueChanged((int)_Value);
                            }
                            RecalcutlateIndicator();
                        }
                    }
                    break;
                case Orientation.Vertical:
                    if (MousePressed)
                    {
                        
                        if (e.Y >= IndicatorSize / 2 && e.Y <= Height - IndicatorSize / 2)
                        {
                            MouseX = e.Y - IndicatorSize / 2;
                            double ValuePerPx = ((double)(MaxValue - MinValue)) / (Height - IndicatorSize);
                            int v = (int)(ValuePerPx * MouseX);
                            if (v != _Value)
                            {
                                _Value = v;
                                if (onValueChanged != null) onValueChanged((int)_Value);
                            }
                            RecalcutlateIndicator();
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
        }

        /// <summary>
        /// Recalcutlates the indicator.
        /// </summary>
        private void RecalcutlateIndicator()
        {
            int iWidht = Width - IndicatorSize;
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    IndicatorRectangle = new Rectangle(MouseX, Height - IndicatorSize, IndicatorSize, IndicatorSize);
                    IndicatorRectangleNormal = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.25), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.25), (int)(IndicatorRectangle.Width * 0.5), (int)(IndicatorRectangle.Height * 0.5));
                    IndicatorRectanglePressed = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.165), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.165), (int)(IndicatorRectangle.Width * 0.66), (int)(IndicatorRectangle.Height * 0.66));
                    IndicatorRectangleDisabled = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.34), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.34), (int)(IndicatorRectangle.Width * 0.33), (int)(IndicatorRectangle.Height * 0.33));
                    Invalidate();
                    break;
                case Orientation.Vertical:
                    IndicatorRectangle = new Rectangle((Width/2 - IndicatorSize/2), MouseX, IndicatorSize, IndicatorSize);
                    IndicatorRectangleNormal = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.25), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.25), (int)(IndicatorRectangle.Width * 0.5), (int)(IndicatorRectangle.Height * 0.5));
                    IndicatorRectanglePressed = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.17), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.165), (int)(IndicatorRectangle.Width * 0.66), (int)(IndicatorRectangle.Height * 0.66));
                    IndicatorRectangleDisabled = new Rectangle(IndicatorRectangle.X + (int)(IndicatorRectangle.Width * 0.34), IndicatorRectangle.Y + (int)(IndicatorRectangle.Height * 0.34), (int)(IndicatorRectangle.Width * 0.33), (int)(IndicatorRectangle.Height * 0.33));
                    Invalidate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            //Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            float percentage = (Value / (MaxValue - MinValue)) * 100;

            //g.Clear(BackColor);
            
            SolidBrush DisabledBrush = new SolidBrush(LineColor);
            
            Pen LinePen = new Pen(LineColor, LineSize+1);

            SizeF fS = g.MeasureString(Convert.ToInt32(percentage).ToString(), Font);

            switch (Orientation)
            {
                case Orientation.Horizontal:

                    #region Horizontal Orientation
                    g.DrawLine(LinePen, IndicatorSize / 2, Height / 2 + (Height - IndicatorSize) / 2, Width - IndicatorSize / 2, Height / 2 + (Height - IndicatorSize) / 2);

                    if (Enabled)
                    {
                        g.DrawLine(new Pen(FillColor, LineSize), IndicatorSize / 2, Height / 2 + (Height - IndicatorSize) / 2, IndicatorRectangleNormal.X, Height / 2 + (Height - IndicatorSize) / 2);

                        if (MousePressed)
                        {
                            if (Value > MinValue)
                            {
                                g.FillEllipse(new SolidBrush(CircleColor), IndicatorRectanglePressed);
                            }
                            else
                            {
                                g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectanglePressed);
                                g.DrawEllipse(LinePen, IndicatorRectanglePressed);
                            }
                        }
                        else
                        {
                            if (Value > MinValue)
                            {
                                g.FillEllipse(new SolidBrush(CircleColor), IndicatorRectangleNormal);
                            }
                            else
                            {
                                g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectangleNormal);
                                g.DrawEllipse(LinePen, IndicatorRectangleNormal);
                            }


                            if (hovered)
                            {
                                g.FillEllipse(new SolidBrush(Color.FromArgb(OuterCircleAlpha, CircleColor)), new Rectangle(IndicatorRectangle.X + CircleReduction / 2, IndicatorRectangle.Y + CircleReduction / 2, IndicatorRectangle.Width - CircleReduction, IndicatorRectangle.Height - CircleReduction));
                            }
                        }
                    }
                    else
                    {
                        if (Value > MinValue)
                        {
                            g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectangleNormal);
                            g.FillEllipse(DisabledBrush, IndicatorRectangleDisabled);
                        }
                        else
                        {
                            g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectangleNormal);
                            g.DrawEllipse(LinePen, IndicatorRectangleDisabled);
                        }
                    }


                    if (ShowMaxMin)
                    {
                        if (percentage != MinValue)
                        {
                            g.DrawString(MinValue.ToString(), Font, new SolidBrush(ForeColor), new PointF(0, 0));

                        }

                        if (percentage != MaxValue)
                        {
                            g.DrawString(MaxValue.ToString(), Font, new SolidBrush(ForeColor), new PointF(Width - g.MeasureString(MaxValue.ToString(), Font).Width, 0f));

                        }

                    }

                    if (ShowPercentage)
                    {
                        g.DrawString(Convert.ToInt32(percentage).ToString() + PostFix, Font, new SolidBrush(ForeColor), new Rectangle(IndicatorRectangle.X, 0, IndicatorRectangle.Width + (int)g.MeasureString(Convert.ToInt32(percentage).ToString() + PostFix, Font).Width, IndicatorRectangle.Height));

                    }

                    #endregion

                    break;
                case Orientation.Vertical:

                    #region Vertical Orientation

                    //g.DrawLine(LinePen, IndicatorSize / 2, Height / 2 + (Height - IndicatorSize) / 2,
                    //    Width - IndicatorSize / 2, Height / 2 + (Height - IndicatorSize) / 2);

                    g.DrawLine(LinePen, new Point(Width/2, 0), new Point(Width/2, Height));

                    if (Enabled)
                    {
                        //g.DrawLine(new Pen(FillColor, LineSize), IndicatorSize / 2, Height / 2 + (Height - IndicatorSize) / 2, IndicatorRectangleNormal.X, Height / 2 + (Height - IndicatorSize) / 2);
                        g.DrawLine(new Pen(FillColor, LineSize), new Point(Width / 2, 0), new Point(Width / 2, IndicatorRectangleNormal.Y));
                        if (MousePressed)
                        {
                            if (Value > MinValue)
                            {
                                g.FillEllipse(new SolidBrush(CircleColor), IndicatorRectanglePressed);
                            }
                            else
                            {
                                g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectanglePressed);
                                g.DrawEllipse(LinePen, IndicatorRectanglePressed);
                            }
                        }
                        else
                        {
                            if (Value > MinValue)
                            {
                                g.FillEllipse(new SolidBrush(CircleColor), IndicatorRectangleNormal);
                            }
                            else
                            {
                                g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectangleNormal);
                                g.DrawEllipse(LinePen, IndicatorRectangleNormal);
                            }


                            if (hovered)
                            {
                                g.FillEllipse(new SolidBrush(Color.FromArgb(OuterCircleAlpha, CircleColor)), new Rectangle(IndicatorRectangle.X + CircleReduction / 2, IndicatorRectangle.Y + CircleReduction / 2, IndicatorRectangle.Width - CircleReduction, IndicatorRectangle.Height - CircleReduction));
                            }
                        }
                    }
                    else
                    {
                        if (Value > MinValue)
                        {
                            g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectangleNormal);
                            g.FillEllipse(DisabledBrush, IndicatorRectangleDisabled);
                        }
                        else
                        {
                            g.FillEllipse(new SolidBrush(MinValColor), IndicatorRectangleNormal);
                            g.DrawEllipse(LinePen, IndicatorRectangleDisabled);
                        }
                    }


                    if (ShowMaxMin)
                    {
                        if (percentage != MinValue)
                        {
                            g.DrawString(MinValue.ToString(), Font, new SolidBrush(ForeColor), new PointF(0, 0));

                        }

                        if (percentage != MaxValue)
                        {
                            g.DrawString(MaxValue.ToString(), Font, new SolidBrush(ForeColor), new PointF(0, Height - g.MeasureString(MaxValue.ToString(), Font).Height));

                        }
                        
                        
                    }

                    if (ShowPercentage)
                    {
                        g.DrawString(Convert.ToInt32(percentage).ToString() + PostFix, Font, new SolidBrush(ForeColor), new Rectangle(IndicatorRectangle.X - (int)(fS.Width/2)-IndicatorSize/8, IndicatorRectangle.Y, IndicatorRectangle.Width + (int)g.MeasureString(Convert.ToInt32(percentage).ToString() + PostFix, Font).Width, IndicatorRectangle.Height));

                    }


                    #endregion

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        #endregion

        
        #region Transparency


        #region Include in Paint

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion

        #region Method

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion


        #endregion
        
    }



    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(LollipopSliderDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class LollipopSliderDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopSliderDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new LollipopSliderSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Zeroit Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }
        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class LollipopSliderSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopSliderSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopSlider colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopSliderSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopSliderSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopSlider;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color LineColor
        {
            get
            {
                return colUserControl.LineColor;
            }
            set
            {
                GetPropertyByName("LineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show maximum minimum].
        /// </summary>
        /// <value><c>true</c> if [show maximum minimum]; otherwise, <c>false</c>.</value>
        public bool ShowMaxMin
        {
            get
            {
                return colUserControl.ShowMaxMin;
            }
            set
            {
                GetPropertyByName("ShowMaxMin").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show percentage].
        /// </summary>
        /// <value><c>true</c> if [show percentage]; otherwise, <c>false</c>.</value>
        public bool ShowPercentage
        {
            get
            {
                return colUserControl.ShowPercentage;
            }
            set
            {
                GetPropertyByName("ShowPercentage").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public float Value
        {
            get
            {
                return colUserControl.Value;
            }
            set
            {
                GetPropertyByName("Value").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public float MaxValue
        {
            get
            {
                return colUserControl.MaxValue;
            }
            set
            {
                GetPropertyByName("MaxValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public float MinValue
        {
            get
            {
                return colUserControl.MinValue;
            }
            set
            {
                GetPropertyByName("MinValue").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the circle.
        /// </summary>
        /// <value>The color of the circle.</value>
        public Color CircleColor
        {
            get
            {
                return colUserControl.CircleColor;
            }
            set
            {
                GetPropertyByName("CircleColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the outer circle alpha.
        /// </summary>
        /// <value>The outer circle alpha.</value>
        public int OuterCircleAlpha
        {
            get
            {
                return colUserControl.OuterCircleAlpha;
            }
            set
            {
                GetPropertyByName("OuterCircleAlpha").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public Color FillColor
        {
            get
            {
                return colUserControl.FillColor;
            }
            set
            {
                GetPropertyByName("FillColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum color of the value.
        /// </summary>
        /// <value>The minimum color of the value.</value>
        public Color MinValColor
        {
            get
            {
                return colUserControl.MinValColor;
            }
            set
            {
                GetPropertyByName("MinValColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the post fix.
        /// </summary>
        /// <value>The post fix.</value>
        public string PostFix
        {
            get
            {
                return colUserControl.PostFix;
            }
            set
            {
                GetPropertyByName("PostFix").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the line.
        /// </summary>
        /// <value>The size of the line.</value>
        public float LineSize
        {
            get
            {
                return colUserControl.LineSize;
            }
            set
            {
                GetPropertyByName("LineSize").SetValue(colUserControl, value);
            }
        }

        public int Spacing
        {
            get
            {
                return colUserControl.Spacing;
            }
            set
            {
                GetPropertyByName("Spacing").SetValue(colUserControl, value);
            }
        }


        public Orientation Orientation
        {
            get
            {
                return colUserControl.Orientation;
            }
            set
            {
                GetPropertyByName("Orientation").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("ShowMaxMin",
                "Show Max & Min Value", "Appearance",
                "Set to show the maximum and minimum values."));

            items.Add(new DesignerActionPropertyItem("ShowPercentage",
                "Show Percentage", "Appearance",
                "Set to show the percentage."));

            items.Add(new DesignerActionPropertyItem("Orientation",
                "Orientation", "Appearance",
                "Sets orientation of the slider to either horizontal or vertical."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("CircleColor",
                "Circle Color", "Appearance",
                "Sets the color of the circle."));

            items.Add(new DesignerActionPropertyItem("FillColor",
                "FillColor", "Appearance",
                "Sets the fill color of the slider."));

            items.Add(new DesignerActionPropertyItem("MinValColor",
                "Minimum Value Color", "Appearance",
                "Sets the circle value when it reaches the minimum value."));

            items.Add(new DesignerActionPropertyItem("LineColor",
                "Line Color", "Appearance",
                "Sets the inactive line color."));

            items.Add(new DesignerActionPropertyItem("Value",
                "Value", "Appearance",
                "Sets the current value."));

            items.Add(new DesignerActionPropertyItem("MaxValue",
                "Maximum Value", "Appearance",
                "Sets the maximum value."));

            items.Add(new DesignerActionPropertyItem("MinValue",
                "Minimum Value", "Appearance",
                "Sets the minimum value."));
            
            items.Add(new DesignerActionPropertyItem("OuterCircleAlpha",
                "Outer Circle Alpha", "Appearance",
                "Sets the outer circle transparency."));

            items.Add(new DesignerActionPropertyItem("LineSize",
                "Line Size", "Appearance",
                "Sets the size of the slider."));

            items.Add(new DesignerActionPropertyItem("Spacing",
                "Spacing", "Appearance",
                "Sets the padding around the control."));

            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion

}
