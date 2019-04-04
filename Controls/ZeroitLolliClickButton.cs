// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLolliClickButton.cs" company="Zeroit Dev Technologies">
//    This program is for creating Lollipop controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{

    #region Control    
    /// <summary>
    /// A class collection for rendering lollipop click animated button.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitLolliClickButtonDesigner))]
    public class ZeroitLollipopClickButton : Control
    {

        #region  Variables

        /// <summary>
        /// The animation timer
        /// </summary>
        Timer AnimationTimer = new Timer { Interval = 1 };

        /// <summary>
        /// The click timer
        /// </summary>
        Timer ClickTimer = new Timer();


        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();
        /// <summary>
        /// The sf
        /// </summary>
        StringFormat SF = new StringFormat();
        /// <summary>
        /// The r
        /// </summary>
        Rectangle R;

        /// <summary>
        /// The focus
        /// </summary>
        bool Focus = false;

        /// <summary>
        /// The xx
        /// </summary>
        int xx;
        /// <summary>
        /// The yy
        /// </summary>
        int yy;

        /// <summary>
        /// The size animation
        /// </summary>
        float SizeAnimation = 0;
        /// <summary>
        /// The size inc number
        /// </summary>
        float SizeIncNum;

        /// <summary>
        /// The fontcolor
        /// </summary>
        string fontcolor = "#ffffff";
        /// <summary>
        /// The backcolor
        /// </summary>
        string Backcolor = "#508ef5";

        /// <summary>
        /// The enabled bg color
        /// </summary>
        Color enabledBGColor = Color.FromArgb(80, 142, 245);
        /// <summary>
        /// The string color
        /// </summary>
        Color StringColor;

        /// <summary>
        /// The disabled bg color
        /// </summary>
        Color disabledBGColor = Color.FromArgb(176, 178, 181);
        /// <summary>
        /// The ripple effect color
        /// </summary>
        Color rippleEffectColor = Color.Black;

        /// <summary>
        /// The ripple opacity
        /// </summary>
        private int rippleOpacity = 25;
        /// <summary>
        /// The interval
        /// </summary>
        private int interval = 1;
        /// <summary>
        /// The clickinterval
        /// </summary>
        private int clickinterval = 1;
        /// <summary>
        /// The radius
        /// </summary>
        private float radius = 1;

        /// <summary>
        /// The double ripple
        /// </summary>
        private bool doubleRipple = false;
        /// <summary>
        /// The allow click animation
        /// </summary>
        private bool allowClickAnimation = true;

        /// <summary>
        /// The clicked
        /// </summary>
        private bool clicked = false;

        /// <summary>
        /// The locate
        /// </summary>
        private Point locate;

        /// <summary>
        /// The click limit
        /// </summary>
        private int clickLimit = 10;

        /// <summary>
        /// The border color
        /// </summary>
        Color borderColor = Color.FromArgb(80, 142, 245);
        /// <summary>
        /// The border width
        /// </summary>
        private float borderWidth = 1;


        #endregion

        #region  Properties


        #region Smoothing Mode

        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.HighQuality;

        /// <summary>
        /// Gets or sets the smoothing.
        /// </summary>
        /// <value>The smoothing.</value>
        public SmoothingMode Smoothing
        {
            get { return smoothing; }
            set
            {
                smoothing = value;
                Invalidate();
            }
        }

        #endregion

        #region TextRenderingHint

        #region Add it to OnPaint / Graphics Rendering component

        //e.Graphics.TextRenderingHint = textrendering;
        #endregion

        /// <summary>
        /// The textrendering
        /// </summary>
        TextRenderingHint textrendering = TextRenderingHint.AntiAlias;

        /// <summary>
        /// Gets or sets the text rendering.
        /// </summary>
        /// <value>The text rendering.</value>
        public TextRenderingHint TextRendering
        {
            get { return textrendering; }
            set
            {
                textrendering = value;
                Invalidate();
            }
        }
        #endregion

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the click speed.
        /// </summary>
        /// <value>The click speed.</value>
        public int ClickSpeed
        {
            get { return clickinterval; }
            set
            {
                clickinterval = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable or disable double ripple.
        /// </summary>
        /// <value><c>true</c> if enable double ripple; otherwise, <c>false</c>.</value>
        public bool DoubleRipple
        {
            get { return doubleRipple; }
            set
            {
                doubleRipple = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the ripple opacity.
        /// </summary>
        /// <value>The ripple opacity.</value>
        public int RippleOpacity
        {
            get { return rippleOpacity; }
            set
            {
                rippleOpacity = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the corners.
        /// </summary>
        /// <value>The corners.</value>
        public float Corners
        {
            get { return radius; }
            set
            {
                radius = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the animation interval.
        /// </summary>
        /// <value>The animation interval.</value>
        public int AnimationInterval
        {
            get { return interval; }
            set
            {
                if (value < 1)
                {
                    value = 1;

                    MessageBox.Show("Value must be more than 1. Default value of 1 will be set");
                }
                interval = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the ripple effect.
        /// </summary>
        /// <value>The color of the ripple effect.</value>
        public Color RippleEffectColor
        {
            get { return rippleEffectColor; }
            set
            {
                rippleEffectColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the enabled bg.
        /// </summary>
        /// <value>The color of the enabled bg.</value>
        public Color EnabledBGColor
        {
            get { return enabledBGColor; }
            set
            {
                enabledBGColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled background.
        /// </summary>
        /// <value>The color of the disabled background.</value>
        public Color DisabledBGColor
        {
            get { return disabledBGColor; }
            set
            {
                disabledBGColor = value;
                Invalidate();
            }
        }

        ///// <summary>
        ///// Gets or sets the color of the background.
        ///// </summary>
        ///// <value>The color of the background.</value>
        //[Category("Appearance")]
        //public string BGColor
        //{
        //    get { return Backcolor; }
        //    set
        //    {
        //        Backcolor = value;
        //        Invalidate();
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the color of the font.
        ///// </summary>
        ///// <value>The color of the font.</value>
        //[Category("Appearance")]
        //public string FontColor
        //{
        //    get { return fontcolor; }
        //    set
        //    {
        //        fontcolor = value;
        //        Invalidate();
        //    }
        //}

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(true)]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the click limit.
        /// </summary>
        /// <value>The click limit.</value>
        public int ClickLimit
        {
            get { return clickLimit; }
            set
            {
                clickLimit = value;
                Invalidate();
            }
        }

        //[Browsable(false)]
        //public Color ForeColor
        //{
        //    get { return base.ForeColor; }
        //    set { base.ForeColor = value; }
        //}

        #endregion

        #region  Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            locate = new Point(Location.X, Location.Y);
            clicked = true;

            xx = e.X;
            yy = e.Y;
            Focus = true;
            AnimationTimer.Start();

            ClickTimer.Start();

            Invalidate();
            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            clicked = false;

            Focus = false;
            AnimationTimer.Start();
            if (allowClickAnimation)
            {
                ClickTimer.Start();
            }

            Invalidate();
            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Mirror)
            {
                R = new Rectangle(0, 0, Width, Height / 2);
            }
            else
            {
                R = new Rectangle(0, 0, Width, Height);
            }
            
        }

        #endregion

        #region Image Designer

        #region Include in paint method

        ///////////////////////////////////////////////////////////////////////////////////////////////// 
        //                                                                                             //                                                                     
        //         ------------------------Add this to the Paint Method ------------------------       //
        //                                                                                             //
        // Rectangle R1 = new Rectangle(0, 0, Width, Height);                                          //
        //                                                                                             //
        // PointF ipt = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize);                   //
        //                                                                                             //
        // if ((Image == null))                                                                        //
        //     {                                                                                       //
        //         G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat           //
        //             {                                                                               //
        //                 Alignment = _TextAlignment,                                                 //
        //                 LineAlignment = StringAlignment.Center                                      //
        //             });                                                                             //
        //      }                                                                                      //
        // else                                                                                        //
        //      {                                                                                      //
        //         G.DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);              //
        //          G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat          //
        //             {                                                                               //
        //                 Alignment = _TextAlignment,                                                 //
        //                 LineAlignment = StringAlignment.Center                                      //
        //             });                                                                             //
        //      }                                                                                      //
        //                                                                                             //
        /////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion

        #region Include in Private Fields
        /// <summary>
        /// The image
        /// </summary>
        private Image _Image;
        /// <summary>
        /// The image size
        /// </summary>
        private Size _ImageSize;
        /// <summary>
        /// The image align
        /// </summary>
        private ContentAlignment _ImageAlign = ContentAlignment.MiddleCenter;
        /// <summary>
        /// The text alignment
        /// </summary>
        private StringAlignment _TextAlignment = StringAlignment.Center;
        /// <summary>
        /// The show text
        /// </summary>
        private bool showText = true;
        #endregion

        #region Include in Public Properties
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get { return _Image; }
            set
            {
                if (value == null)
                {
                    _ImageSize = Size.Empty;
                }
                else
                {
                    _ImageSize = value.Size;
                }

                _Image = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the image.
        /// </summary>
        /// <value>The size of the image.</value>
        public Size ImageSize
        {
            get { return _ImageSize; }
            set
            {
                _ImageSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the image align.
        /// </summary>
        /// <value>The image align.</value>
        public ContentAlignment ImageAlign
        {
            get { return _ImageAlign; }
            set
            {
                _ImageAlign = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show text].
        /// </summary>
        /// <value><c>true</c> if [show text]; otherwise, <c>false</c>.</value>
        public bool ShowText
        {
            get { return showText; }
            set
            {
                showText = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text align.
        /// </summary>
        /// <value>The text align.</value>
        public StringAlignment TextAlign
        {
            get { return _TextAlignment; }
            set
            {
                _TextAlignment = value;
                Invalidate();
            }
        }


        #endregion

        #region Include in Private Methods
        /// <summary>
        /// Images the location.
        /// </summary>
        /// <param name="SF">The sf.</param>
        /// <param name="Area">The area.</param>
        /// <param name="ImageArea">The image area.</param>
        /// <returns>PointF.</returns>
        private static PointF ImageLocation(StringFormat SF, SizeF Area, SizeF ImageArea)
        {
            PointF MyPoint = default(PointF);
            switch (SF.Alignment)
            {
                case StringAlignment.Center:
                    MyPoint.X = Convert.ToSingle((Area.Width - ImageArea.Width) / 2);
                    break;
                case StringAlignment.Near:
                    MyPoint.X = 2;
                    break;
                case StringAlignment.Far:
                    MyPoint.X = Area.Width - ImageArea.Width - 2;
                    break;
            }

            switch (SF.LineAlignment)
            {
                case StringAlignment.Center:
                    MyPoint.Y = Convert.ToSingle((Area.Height - ImageArea.Height) / 2);
                    break;
                case StringAlignment.Near:
                    MyPoint.Y = 2;
                    break;
                case StringAlignment.Far:
                    MyPoint.Y = Area.Height - ImageArea.Height - 2;
                    break;
            }
            return MyPoint;
        }

        /// <summary>
        /// Gets the string format.
        /// </summary>
        /// <param name="_ContentAlignment">The content alignment.</param>
        /// <returns>StringFormat.</returns>
        private StringFormat GetStringFormat(ContentAlignment _ContentAlignment)
        {
            StringFormat SF = new StringFormat();
            switch (_ContentAlignment)
            {
                case ContentAlignment.MiddleCenter:
                    SF.LineAlignment = StringAlignment.Center;
                    SF.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    SF.LineAlignment = StringAlignment.Center;
                    SF.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    SF.LineAlignment = StringAlignment.Center;
                    SF.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.TopCenter:
                    SF.LineAlignment = StringAlignment.Near;
                    SF.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    SF.LineAlignment = StringAlignment.Near;
                    SF.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    SF.LineAlignment = StringAlignment.Near;
                    SF.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    SF.LineAlignment = StringAlignment.Far;
                    SF.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    SF.LineAlignment = StringAlignment.Far;
                    SF.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomRight:
                    SF.LineAlignment = StringAlignment.Far;
                    SF.Alignment = StringAlignment.Far;
                    break;
            }
            return SF;
        }

        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="G">The g.</param>
        /// <param name="R1">The r1.</param>
        private void DrawImage(Graphics G, Rectangle R1)
        {
            //Rectangle R1 = new Rectangle(0, 0, Width, Height);                                          
            G.SmoothingMode = SmoothingMode.HighQuality;

            PointF ipt = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize);

            if ((Image == null))
            {
                if (ShowText)
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                    {
                        Alignment = _TextAlignment,
                        LineAlignment = StringAlignment.Center

                    });
            }
            else
            {
                G.DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);

                if (ShowText)
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                    {
                        Alignment = _TextAlignment,
                        LineAlignment = StringAlignment.Center
                    });
            }

        }


        #endregion
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




        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopClickButton" /> class.
        /// </summary>
        public ZeroitLollipopClickButton()
        {
            SetStyle(
                (ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint), true);
            
            //DoubleBuffered = true;

            Size = new Size(143, 41);
            //BackColor = Color.Transparent;

            locate = new Point(Location.X, Location.Y);

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            AnimationTimer.Tick += new EventHandler(AnimationTick);
            
            ClickTimer.Tick += ClickTimer_Tick;

            ForeColor = Color.White;

        }

        /// <summary>
        /// Handles the Tick event of the ClickTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClickTimer_Tick(object sender, EventArgs e)
        {

            if (clicked)
            {
                this.Location = new Point(Location.X, Location.Y + 1);
                //this.Location = new Point(Location.X, Location.Y - 10);
            }
            else
            {
                this.Location = locate;
            }

            if (Location.Y > locate.Y + ClickLimit)
            {
                this.Location = locate;
                ClickTimer.Stop();
            }

            Invalidate();

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            SizeIncNum = Width / 34;
        }


        #region Mirror

        /// <summary>
        /// The bitmap
        /// </summary>
        private Bitmap bitmap;
        /// <summary>
        /// The mirror
        /// </summary>
        private bool mirror = true;

        /// <summary>
        /// The length
        /// </summary>
        private int length = 100;

        /// <summary>
        /// The mirror rotation
        /// </summary>
        private RotateFlipType mirrorRotation = RotateFlipType.Rotate180FlipX;

        /// <summary>
        /// The mirror gradient
        /// </summary>
        private LinearGradientMode mirrorGradient = LinearGradientMode.Vertical;

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length
        {
            get { return length; }
            set { length = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLollipopClickButton"/> is mirror.
        /// </summary>
        /// <value><c>true</c> if mirror; otherwise, <c>false</c>.</value>
        public bool Mirror
        {
            get { return mirror; }
            set { mirror = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the mirror rotation.
        /// </summary>
        /// <value>The mirror rotation.</value>
        public RotateFlipType MirrorRotation
        {
            get { return mirrorRotation; }
            set
            {
                mirrorRotation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the mirror gradient.
        /// </summary>
        /// <value>The mirror gradient.</value>
        public LinearGradientMode MirrorGradient
        {
            get { return mirrorGradient; }
            set
            {
                mirrorGradient = value;
                Invalidate();
            }
        }

        #endregion

        /// <summary>
        /// The bg
        /// </summary>
        GraphicsPath BG;
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            base.OnPaint(e);

            
            AnimationTimer.Interval = interval;
            ClickTimer.Interval = clickinterval;

            int internalWidth = Width;
            int internalHeight = Height / 2;

            if (mirror)
            {
                bitmap = new Bitmap(internalWidth, internalHeight);
            }
            else
            {
                bitmap = new Bitmap(Width, Height);
            }

            Graphics G = Graphics.FromImage(bitmap);
            G.SmoothingMode = smoothing;
            //G.TextRenderingHint = textrendering;
            G.Clear(Color.Transparent);

            //StringColor = ColorTranslator.FromHtml(fontcolor);
            //enabledBGColor = Backcolor;

            if (Mirror)
            {
                BG = DrawHelper.CreateRoundRect(1, 1, internalWidth - 3, internalHeight - 3, radius);

            }
            else
            {
                BG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 3, radius);

            }

            Region region = new Region(BG);

            G.FillPath(new SolidBrush(Enabled ? enabledBGColor : disabledBGColor), BG);


            //G.DrawPath(new Pen(Enabled ? enabledBGColor : disabledBGColor), BG);

            G.DrawPath(new Pen(BorderColor, BorderWidth), BG);

            G.SetClip(region, CombineMode.Replace);

            if (doubleRipple)
            {
                //The Ripple Effect
                G.FillEllipse(new SolidBrush(Color.FromArgb(rippleOpacity, rippleEffectColor)), xx - (SizeAnimation / 2), yy - (SizeAnimation / 2), SizeAnimation, SizeAnimation);

                G.FillEllipse(new SolidBrush(Color.FromArgb(rippleOpacity, rippleEffectColor)), xx - (SizeAnimation / 2), yy - (SizeAnimation / 4), SizeAnimation, SizeAnimation);

            }
            else
            {
                //The Ripple Effect
                G.FillEllipse(new SolidBrush(Color.FromArgb(rippleOpacity, rippleEffectColor)), xx - (SizeAnimation / 2), yy - (SizeAnimation / 2), SizeAnimation, SizeAnimation);

            }

            if (Mirror)
            {

                DrawImage(G, new Rectangle(0, 0, Width, Height / 2));

                //G.DrawString(Text, font.Roboto_Medium9, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height / 2), SF);

            }
            else
            {
                DrawImage(G, new Rectangle(0, 0, Width, Height));

                //G.DrawString(Text, font.Roboto_Medium9, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);

            }

            if (Mirror)
            {
                e.Graphics.DrawImage(
                    DrawReflection(bitmap, BackColor, MirrorRotation, MirrorGradient,
                        Length), 0, 0);

            }
            else
            {

                e.Graphics.DrawImage(bitmap, 0, 0);

            }

            bitmap.Dispose();
            G.Dispose();
            if(!DesignMode)
                GC.Collect();
        }

        /// <summary>
        /// Animations the tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void AnimationTick(object sender, EventArgs e)
        {
            if (Focus)
            {
                if (SizeAnimation < Width + 250)
                {
                    SizeAnimation += SizeIncNum;
                    this.Invalidate();
                }
            }
            else
            {
                if (SizeAnimation > 0)
                {
                    SizeAnimation = 0;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Draws the reflection.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="toBG">To bg.</param>
        /// <param name="RotateFlipType">Type of the rotate flip.</param>
        /// <param name="LinearGradientMode">The linear gradient mode.</param>
        /// <param name="Length">The length.</param>
        /// <returns>Image.</returns>
        private static Image DrawReflection(Image img, Color toBG,
            RotateFlipType RotateFlipType = RotateFlipType.Rotate180FlipX,
            LinearGradientMode LinearGradientMode = LinearGradientMode.Vertical,
            int Length = 100) // img is the original image.
        {
            //This is the static function that generates the reflection...
            int height = img.Height + Length; //Added height from the original height of the image.
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img.Width, height, PixelFormat.Format64bppPArgb); //A new bitmap.
            Brush brsh = new LinearGradientBrush(new Rectangle(0, 0, img.Width + 10, height), Color.Transparent, toBG, LinearGradientMode);//The Brush that generates the fading effect to a specific color of your background.
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution); //Sets the new bitmap's resolution.
            using (System.Drawing.Graphics grfx = System.Drawing.Graphics.FromImage(bmp)) //A graphics to be generated from an image (here, the new Bitmap we've created (bmp)).
            {
                System.Drawing.Bitmap bm = (System.Drawing.Bitmap)img; //Generates a bitmap from the original image (img).
                grfx.DrawImage(bm, 0, 0, img.Width, img.Height); //Draws the generated bitmap (bm) to the new bitmap (bmp).
                System.Drawing.Bitmap bm1 = (System.Drawing.Bitmap)img; //Generates a bitmap again from the original image (img).
                bm1.RotateFlip(RotateFlipType); //Flips and rotates the image (bm1).
                grfx.DrawImage(bm1, 0, img.Height); //Draws (bm1) below (bm) so it serves as the reflection image.
                Rectangle rt = new Rectangle(0, img.Height, img.Width, Length); //A new rectangle to paint our gradient effect.
                grfx.FillRectangle(brsh, rt); //Brushes the gradient on (rt).
            }

            return bmp; //Returns the (bmp) with the generated image.
        }


    }
    #endregion

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitLolliClickButtonDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitLolliClickButtonDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitLolliClickButtonDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitLolliClickButtonSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitLolliClickButtonSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitLolliClickButtonSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopClickButton colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLolliClickButtonSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitLolliClickButtonSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopClickButton;

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
        /// Gets or sets the color of the ripple effect.
        /// </summary>
        /// <value>The color of the ripple effect.</value>
        public Color RippleEffectColor
        {
            get
            {
                return colUserControl.RippleEffectColor;
            }
            set
            {
                GetPropertyByName("RippleEffectColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the enabled bg.
        /// </summary>
        /// <value>The color of the enabled bg.</value>
        public Color EnabledBGColor
        {
            get
            {
                return colUserControl.EnabledBGColor;
            }
            set
            {
                GetPropertyByName("EnabledBGColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled bg.
        /// </summary>
        /// <value>The color of the disabled bg.</value>
        public Color DisabledBGColor
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
        /// Gets or sets a value indicating whether [double ripple].
        /// </summary>
        /// <value><c>true</c> if [double ripple]; otherwise, <c>false</c>.</value>
        public bool DoubleRipple
        {
            get
            {
                return colUserControl.DoubleRipple;
            }
            set
            {
                GetPropertyByName("DoubleRipple").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the click speed.
        /// </summary>
        /// <value>The click speed.</value>
        public int ClickSpeed
        {
            get
            {
                return colUserControl.ClickSpeed;
            }
            set
            {
                GetPropertyByName("ClickSpeed").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the ripple opacity.
        /// </summary>
        /// <value>The ripple opacity.</value>
        public int RippleOpacity
        {
            get
            {
                return colUserControl.RippleOpacity;
            }
            set
            {
                GetPropertyByName("RippleOpacity").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the corners.
        /// </summary>
        /// <value>The corners.</value>
        public float Corners
        {
            get
            {
                return colUserControl.Corners;
            }
            set
            {
                GetPropertyByName("Corners").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the animation interval.
        /// </summary>
        /// <value>The animation interval.</value>
        public int AnimationInterval
        {
            get
            {
                return colUserControl.AnimationInterval;
            }
            set
            {
                GetPropertyByName("AnimationInterval").SetValue(colUserControl, value);
            }
        }

        //public string BGColor
        //{
        //    get
        //    {
        //        return colUserControl.BGColor;
        //    }
        //    set
        //    {
        //        GetPropertyByName("BGColor").SetValue(colUserControl, value);
        //    }
        //}

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font
        {
            get
            {
                return colUserControl.Font;
            }
            set
            {
                GetPropertyByName("Font").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text rendering.
        /// </summary>
        /// <value>The text rendering.</value>
        public TextRenderingHint TextRendering
        {
            get
            {
                return colUserControl.TextRendering;
            }
            set
            {
                GetPropertyByName("TextRendering").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the smoothing.
        /// </summary>
        /// <value>The smoothing.</value>
        public SmoothingMode Smoothing
        {
            get
            {
                return colUserControl.Smoothing;
            }
            set
            {
                GetPropertyByName("Smoothing").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLolliClickButtonSmartTagActionList"/> is mirror.
        /// </summary>
        /// <value><c>true</c> if mirror; otherwise, <c>false</c>.</value>
        public bool Mirror
        {
            get
            {
                return colUserControl.Mirror;
            }
            set
            {
                GetPropertyByName("Mirror").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length
        {
            get
            {
                return colUserControl.Length;
            }
            set
            {
                GetPropertyByName("Length").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get
            {
                return colUserControl.Image;
            }
            set
            {
                GetPropertyByName("Image").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("DoubleRipple",
                                 "Double Ripple", "Appearance",
                                 "Set to enable double ripple."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));
            
            items.Add(new DesignerActionPropertyItem("RippleEffectColor",
                                 "Ripple Effect Color", "Appearance",
                                 "Sets the ripple effect color."));


            items.Add(new DesignerActionPropertyItem("EnabledBGColor",
                                 "Enabled BG Color", "Appearance",
                                 "Sets the background enabled color."));

            items.Add(new DesignerActionPropertyItem("DisabledBGColor",
                                 "Disabled BG Color", "Appearance",
                                 "Sets the disabled color."));

            items.Add(new DesignerActionPropertyItem("Image",
                "Image", "Appearance",
                "Sets the Image."));


            items.Add(new DesignerActionPropertyItem("ClickSpeed",
                                 "Click Speed", "Appearance",
                                 "Sets the click speed."));

            items.Add(new DesignerActionPropertyItem("RippleOpacity",
                                 "Ripple Opacity", "Appearance",
                                 "Sets the ripple opacity."));

            items.Add(new DesignerActionPropertyItem("Corners",
                                 "Corners", "Appearance",
                                 "Sets the corners."));

            items.Add(new DesignerActionPropertyItem("AnimationInterval",
                                 "Animation Interval", "Appearance",
                                 "Sets the animation interval."));
            
            items.Add(new DesignerActionPropertyItem("Font",
                                 "Font", "Appearance",
                                 "Sets the font."));

            //items.Add(new DesignerActionPropertyItem("Smoothing",
            //                     "Smoothing", "Appearance",
            //                     "Sets the smoothing mode."));

            //items.Add(new DesignerActionPropertyItem("TextRendering",
            //                     "Text Rendering", "Appearance",
            //                     "Sets the text rendering mode."));

            items.Add(new DesignerActionHeaderItem("Mirror Effect"));

            items.Add(new DesignerActionPropertyItem("Mirror",
                "Mirror", "Mirror",
                "Set to enable mirror effect."));

            items.Add(new DesignerActionPropertyItem("Length",
                "Length", "Mirror",
                "Set to enable mirror length."));

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
