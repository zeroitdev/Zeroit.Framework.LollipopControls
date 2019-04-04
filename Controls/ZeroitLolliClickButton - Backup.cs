using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class LolliClickButton : Control
    {

        #region  Variables

        Timer AnimationTimer = new Timer { Interval = 1 };

        Timer ClickTimer = new Timer();


        FontManager font = new FontManager();
        StringFormat SF = new StringFormat();
        Rectangle R;

        bool Focus = false;

        int xx;
        int yy;

        float SizeAnimation = 0;
        float SizeIncNum;

        string fontcolor = "#ffffff";
        string Backcolor = "#508ef5";

        Color enabledBGColor = Color.FromArgb(80, 142, 245);
        Color StringColor;

        Color disabledBGColor = Color.FromArgb(176, 178, 181);
        Color rippleEffectColor = Color.Black;

        private int rippleOpacity = 25;
        private int interval = 1;
        private int clickinterval = 1;
        private float radius = 1;

        private bool doubleRipple = false;
        private bool allowClickAnimation = true;

        private bool clicked = false;

        private Point locate;

        private int clickLimit = 10;


        #endregion

        #region  Properties


        #region Smoothing Mode

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

        TextRenderingHint textrendering = TextRenderingHint.AntiAlias;

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
        /// Gets or sets the color of the font.
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

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LolliClickButton"/> class.
        /// </summary>
        public LolliClickButton()
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

        }

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
        
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            SizeIncNum = Width / 34;
        }


        #region Mirror

        private Bitmap bitmap;
        private bool mirror = true;

        private int length = 100;
        public int Length
        {
            get { return length; }
            set { length = value; Invalidate(); }
        }

        public bool Mirror
        {
            get { return mirror; }
            set { mirror = value; Invalidate(); }
        }

        #endregion

        GraphicsPath BG;
        protected override void OnPaint(PaintEventArgs e)
        {
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

            StringColor = ColorTranslator.FromHtml(fontcolor);
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

            if (Mirror)
            {
                G.DrawString(Text, font.Roboto_Medium9, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height / 2), SF);

            }
            else
            {
                G.DrawString(Text, font.Roboto_Medium9, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);

            }

            if (Mirror)
            {
                e.Graphics.DrawImage(
                    DrawReflection(bitmap, BackColor, RotateFlipType.Rotate180FlipX, LinearGradientMode.Vertical,
                        Length), 0, 0);

            }
            else
            {

                e.Graphics.DrawImage(bitmap, 0, 0);

            }

        }
        
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
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitLolliClickButtonDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
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
    public class ZeroitLolliClickButtonSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        private LolliClickButton colUserControl;


        private DesignerActionUIService designerActionUISvc = null;


        public ZeroitLolliClickButtonSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as LolliClickButton;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
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
