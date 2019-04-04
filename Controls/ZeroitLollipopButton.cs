// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopButton.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{
    #region Control    
    /// <summary>
    /// A class collection for rendering button.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitLollipopButtonButtonDesigner))]
    public class ZeroitLollipopButton : Control
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
        private int clickinterval = 100;
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



        #endregion

        #region  Properties

        //public int ClickSpeed
        //{
        //    get { return clickinterval; }
        //    set
        //    {
        //        clickinterval = value;
        //        Invalidate();
        //    }
        //}


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
        /// Gets or sets the color of the enabled background.
        /// </summary>
        /// <value>The color of the enabled background.</value>
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

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        [Category("Appearance")]
        public string BGColor
        {
            get { return Backcolor; }
            set
            {
                Backcolor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the font in hex.
        /// </summary>
        /// <value>The color of the font.</value>
        [Category("Appearance")]
        public string FontColor
        {
            get { return fontcolor; }
            set
            {
                fontcolor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(true)]
        public Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
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

            #region Not Needed
            //locate = new Point(Location.X, Location.Y);
            //clicked = true;
            //ClickTimer.Start(); 
            #endregion

            xx = e.X;
            yy = e.Y;
            Focus = true;
            AnimationTimer.Start();



            Invalidate();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            #region Not Needed
            //clicked = false;
            //if (allowClickAnimation)
            //{
            //    ClickTimer.Start();
            //}

            #endregion

            Focus = false;
            AnimationTimer.Start();

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
            R = new Rectangle(0, 0, Width, Height);
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




        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopButton" /> class.
        /// </summary>
        public ZeroitLollipopButton()
        {
            SetStyle((ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint), true);
            DoubleBuffered = true;

            Size = new Size(143, 41);
            BackColor = Color.Transparent;

            #region Not Needed

            //locate = new Point(Location.X, Location.Y);
            //ClickTimer.Tick += ClickTimer_Tick;
            #endregion

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            AnimationTimer.Tick += new EventHandler(AnimationTick);

        }

        #region Not Needed

        //private void ClickTimer_Tick(object sender, EventArgs e)
        //{

        //    if (clicked)
        //    {
        //        this.Location = new Point(Location.X, Location.Y + 1);
        //        //this.Location = new Point(Location.X, Location.Y - 10);
        //    }

        //    else
        //    {
        //        this.Location = locate;
        //    }

        //    Invalidate();

        //}


        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            SizeIncNum = Width / 34;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            base.OnPaint(e);

            AnimationTimer.Interval = interval;

            #region Not Needed

            //ClickTimer.Interval = clickinterval;

            #endregion

            var G = e.Graphics;
            G.SmoothingMode = smoothing;
            G.TextRenderingHint = textrendering;
            //G.Clear(Parent.BackColor);

            StringColor = ColorTranslator.FromHtml(fontcolor);
            //enabledBGColor = Backcolor;

            var BG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 3, radius);
            Region region = new Region(BG);

            G.FillPath(new SolidBrush(Enabled ? enabledBGColor : disabledBGColor), BG);
            G.DrawPath(new Pen(Enabled ? enabledBGColor : disabledBGColor), BG);

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

            G.DrawString(Text, font.Roboto_Medium9, new SolidBrush(ForeColor), R, SF);
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
    }

    #endregion

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitLollipopButtonButtonDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitLollipopButtonButtonDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitLollipopButtonButtonDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitLollipopButtonSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitLollipopButtonSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitLollipopButtonSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopButton colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopButtonSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitLollipopButtonSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopButton;

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

        /// <summary>
        /// Gets or sets the color of the bg.
        /// </summary>
        /// <value>The color of the bg.</value>
        public string BGColor
        {
            get
            {
                return colUserControl.BGColor;
            }
            set
            {
                GetPropertyByName("BGColor").SetValue(colUserControl, value);
            }
        }

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

            items.Add(new DesignerActionPropertyItem("Smoothing",
                                 "Smoothing", "Appearance",
                                 "Sets the smoothing mode."));

            items.Add(new DesignerActionPropertyItem("TextRendering",
                                 "Text Rendering", "Appearance",
                                 "Sets the text rendering mode."));

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
