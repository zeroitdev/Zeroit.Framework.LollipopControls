// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopProgressBar.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{

    /// <summary>
    /// A class collection for rendering Lollipop progress bar.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(LollipopProgressBarDesigner))]
    public class ZeroitLollipopBarProgress : Control
    {
        #region variables

        /// <summary>
        /// The progress number
        /// </summary>
        float ProgressNum = 0;
        /// <summary>
        /// The progress color
        /// </summary>
        Color ProgressColor = ColorTranslator.FromHtml("#508ef5");

        /// <summary>
        /// The back ground color
        /// </summary>
        Color backGroundColor;

        /// <summary>
        /// The maximum
        /// </summary>
        private float maximum = 100;
        /// <summary>
        /// The minimum
        /// </summary>
        private float minimum = 0;

        /// <summary>
        /// The show text
        /// </summary>
        private bool showText = true;
        /// <summary>
        /// The post fix
        /// </summary>
        private string postFix = "%";

        /// <summary>
        /// The background alpha
        /// </summary>
        private int backgroundAlpha = 68;
        /// <summary>
        /// The rounding
        /// </summary>
        private bool rounding = false;
        /// <summary>
        /// The radius
        /// </summary>
        private int radius = 2;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        [Category("Appearance")]
        public Color BackgroundColor
        {
            get { return ProgressColor; }
            set
            {
                ProgressColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Category("Behavior")]
        public float Value
        {
            get { return ProgressNum; }
            set
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }
                ProgressNum = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        [Category("Behavior")]
        public float Maximum
        {
            get { return maximum; }
            set { maximum = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        [Category("Behavior")]
        public float Minimum
        {
            get { return minimum; }
            set { minimum = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show text.
        /// </summary>
        /// <value><c>true</c> if show text; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
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
        /// Gets or sets the post fix text.
        /// </summary>
        /// <value>The post fix text.</value>
        [Category("Behavior")]
        public string PostFix
        {
            get { return postFix; }
            set
            {
                postFix = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the speed of the progress animation.
        /// </summary>
        /// <value>The speed.</value>
        public int Speed
        {
            get { return timer.Interval; }
            set
            {
                timer.Interval = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background transparency.
        /// </summary>
        /// <value>The background transparency.</value>
        public int BackTransparency
        {
            get { return backgroundAlpha; }
            set
            {
                backgroundAlpha = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="ZeroitLollipopBarProgress" /> is rounded.
        /// </summary>
        /// <value><c>true</c> if rounding; otherwise, <c>false</c>.</value>
        public bool Rounding
        {
            get { return rounding; }
            set
            {
                rounding = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the radius for the rounding.
        /// </summary>
        /// <value>The radius.</value>
        public int Radius
        {
            get { return radius; }
            set
            {
                if (value < 1)
                {
                    value = 1;
                    MessageBox.Show("Value must be more than 1");
                    Invalidate();
                }
                radius = value;
                Invalidate();
            }
        }

        #endregion


        #region Timer Event


        #region Include in Private Field

        /// <summary>
        /// The automatic animate
        /// </summary>
        private bool autoAnimate = false;
        /// <summary>
        /// The timer
        /// </summary>
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        /// <summary>
        /// The timer decrement
        /// </summary>
        private System.Windows.Forms.Timer timerDecrement = new System.Windows.Forms.Timer();
        /// <summary>
        /// The speed multiplier
        /// </summary>
        private float speedMultiplier = 1;
        /// <summary>
        /// The change
        /// </summary>
        private float change = 0.1f;
        /// <summary>
        /// The reverse
        /// </summary>
        private bool reverse = true;

        /// <summary>
        /// The sluggish
        /// </summary>
        private bool sluggish = false;
        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [automatic animate].
        /// </summary>
        /// <value><c>true</c> if [automatic animate]; otherwise, <c>false</c>.</value>
        public bool AutoAnimate
        {
            get { return autoAnimate; }
            set
            {
                autoAnimate = value;

                if (value == true)
                {
                    timer.Enabled = true;
                }

                else
                {
                    timer.Enabled = false;
                    timerDecrement.Enabled = false;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLollipopBarProgress"/> is reverse.
        /// </summary>
        /// <value><c>true</c> if reverse; otherwise, <c>false</c>.</value>
        public bool Reverse
        {
            get { return reverse; }
            set
            {

                reverse = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the change.
        /// </summary>
        /// <value>The change.</value>
        public float Change
        {
            get { return change; }
            set
            {
                change = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the speed multiplier.
        /// </summary>
        /// <value>The speed multiplier.</value>
        public float SpeedMultiplier
        {
            get { return speedMultiplier; }
            set
            {
                speedMultiplier = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the timer interval.
        /// </summary>
        /// <value>The timer interval.</value>
        public int TimerInterval
        {
            get { return timer.Interval; }
            set
            {
                timer.Interval = value;
                timerDecrement.Interval = value;
                Invalidate();
            }
        }



        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLollipopBarProgress"/> is sluggish.
        /// </summary>
        /// <value><c>true</c> if sluggish; otherwise, <c>false</c>.</value>
        public bool Sluggish
        {
            get { return sluggish; }
            set
            {
                sluggish = value;
                Invalidate();
            }
        }

        #endregion

        #region Event

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (Reverse)
            {

                if (this.Value + (Change * SpeedMultiplier) > Maximum)
                {
                    timer.Stop();
                    timer.Enabled = false;
                    timerDecrement.Enabled = true;
                    timerDecrement.Start();
                    //timerDecrement.Tick += TimerDecrement_Tick;
                    Invalidate();
                }

                else
                {
                    this.Value += (Change * SpeedMultiplier);
                    Invalidate();
                }


            }
            else
            {

                if (Sluggish)
                {
                    if (this.Value + (Change * SpeedMultiplier) > Maximum)
                    {
                        timer.Stop();
                        timer.Enabled = false;
                        timerDecrement.Enabled = true;
                        timerDecrement.Start();
                        
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        Invalidate();
                    }

                    else
                    {
                        this.Value += (Change * SpeedMultiplier);
                        Invalidate();
                    }
                }
                else
                {
                    if (this.Value + (Change * SpeedMultiplier) > Maximum)
                    {
                        timerDecrement.Enabled = false;
                        timerDecrement.Stop();
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        Value = 0;
                        Invalidate();
                    }

                    else
                    {
                        this.Value += (Change * SpeedMultiplier);
                        Invalidate();
                    }
                }

            }
        }


        private void TimerDecrement_Tick(object sender, EventArgs e)
        {
            if (this.Value < this.Minimum)
            {
                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                timer.Enabled = true;
                timer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                this.Value -= (Change * SpeedMultiplier);
                Invalidate();
            }


        }


        #endregion

        #region Constructor

        private void IncludeInConstructor()
        {

            if (DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 100;
                    timer.Interval = 100;
                    timer.Start();
                }
            }

            if (!DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 100;
                    timer.Interval = 100;
                    timer.Start();
                }
            }

        }

        #endregion


        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopBarProgress" /> class.
        /// </summary>
        public ZeroitLollipopBarProgress()
        {
            Width = 300;
            Height = 4;
            DoubleBuffered = true;

            IncludeInConstructor();
        }
        /// <summary>
        /// Centers the string.
        /// </summary>
        /// <param name="G">The g.</param>
        /// <param name="T">The t.</param>
        /// <param name="F">The f.</param>
        /// <param name="C">The c.</param>
        /// <param name="R">The r.</param>
        public static void CenterString(Graphics G, string T, Font F, Color C, Rectangle R)
        {
            SizeF TS = G.MeasureString(T, F);

            using (SolidBrush B = new SolidBrush(C))
            {
                G.DrawString(T, F, B, new Point(R.Width / 2 - (int)(TS.Width / 2), R.Height / 2 - (int)(TS.Height / 2)));
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            base.OnPaint(e);

            Graphics G = e.Graphics;
            //G.Clear(BackColor);

            backGroundColor = ProgressColor;

            var rectangle = DrawHelper.CreateRoundRect(0, 0, Width, Height, radius);

            var rectangleFill = DrawHelper.CreateRoundRect(0, 0, (Width * Value) / ((int)Maximum - (int)Minimum), Height, radius);

            if (Rounding)
            {
                G.FillPath(new SolidBrush(Color.FromArgb(backgroundAlpha, BackColor)), rectangle);

                G.FillPath(new SolidBrush(backGroundColor), rectangleFill);

            }
            else
            {
                G.FillRectangle(new SolidBrush(Color.FromArgb(backgroundAlpha, BackColor)), 0, 0, Width, Height);

                G.FillRectangle(new SolidBrush(backGroundColor), 0, 0, (Width * Value) / ((int)Maximum - (int)Minimum), Height);

            }



            if (ShowText)
            {
                CenterString(e.Graphics,Convert.ToInt32((Value/(Maximum-Minimum)) * 100).ToString() + PostFix, Font, ForeColor,
                    ClientRectangle);
            }
            
        }




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

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class LollipopProgressBarDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopProgressBarDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopProgressBarSmartTagActionList(this.Component));
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
    /// Class LollipopProgressBarSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopProgressBarSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopBarProgress colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopProgressBarSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopProgressBarSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopBarProgress;

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
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        [Category("Appearance")]
        public Color BackgroundColor
        {
            get
            {
                return colUserControl.BackgroundColor;
            }
            set
            {
                GetPropertyByName("BackgroundColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Category("Behavior")]
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
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [Category("Behavior")]
        public float Maximum
        {
            get
            {
                return colUserControl.Maximum;
            }
            set
            {
                GetPropertyByName("Maximum").SetValue(colUserControl, value);
            }
        }
        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [Category("Behavior")]
        public float Minimum
        {
            get
            {
                return colUserControl.Minimum;
            }
            set
            {
                GetPropertyByName("Minimum").SetValue(colUserControl, value);
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [show text].
        /// </summary>
        /// <value><c>true</c> if [show text]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool ShowText
        {
            get
            {
                return colUserControl.ShowText;
            }
            set
            {
                GetPropertyByName("ShowText").SetValue(colUserControl, value);
            }
        }
        /// <summary>
        /// Gets or sets the post fix.
        /// </summary>
        /// <value>The post fix.</value>
        [Category("Behavior")]
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
        /// Gets or sets a value indicating whether [automatic animate].
        /// </summary>
        /// <value><c>true</c> if [automatic animate]; otherwise, <c>false</c>.</value>
        public bool AutoAnimate
        {
            get
            {
                return colUserControl.AutoAnimate;
            }
            set
            {
                GetPropertyByName("AutoAnimate").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public int Speed
        {
            get
            {
                return colUserControl.Speed;
            }
            set
            {
                GetPropertyByName("Speed").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the back transparency.
        /// </summary>
        /// <value>The back transparency.</value>
        public int BackTransparency
        {
            get
            {
                return colUserControl.BackTransparency;
            }
            set
            {
                GetPropertyByName("BackTransparency").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LollipopProgressBarSmartTagActionList"/> is rounding.
        /// </summary>
        /// <value><c>true</c> if rounding; otherwise, <c>false</c>.</value>
        public bool Rounding
        {
            get
            {
                return colUserControl.Rounding;
            }
            set
            {
                GetPropertyByName("Rounding").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public int Radius
        {
            get
            {
                return colUserControl.Radius;
            }
            set
            {
                GetPropertyByName("Radius").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("ShowText",
                "Show Text", "Appearance",
                "Set to show the Progress."));

            items.Add(new DesignerActionPropertyItem("Rounding",
                "Rounding", "Appearance",
                "Sets the progress to have a rounding effect."));


            items.Add(new DesignerActionPropertyItem("AutoAnimate",
                "Auto Animate", "Appearance",
                "Sets the progress to automatically animate."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("BackgroundColor",
                                 "Background Color", "Appearance",
                                 "Sets the background color."));

            items.Add(new DesignerActionPropertyItem("BackTransparency",
                "Background Transparency", "Appearance",
                "Sets the background transparency."));

            items.Add(new DesignerActionPropertyItem("Value",
                                 "Value", "Appearance",
                                 "Sets the progress value."));

            items.Add(new DesignerActionPropertyItem("Maximum",
                "Maximum", "Appearance",
                "Sets the maximum value."));

            items.Add(new DesignerActionPropertyItem("Minimum",
                "Minimum", "Appearance",
                "Sets the minimum value."));

            items.Add(new DesignerActionPropertyItem("Radius",
                "Radius", "Appearance",
                "Sets the border radius."));
            
            items.Add(new DesignerActionPropertyItem("Speed",
                "Speed", "Appearance",
                "Sets the animation speed."));

            items.Add(new DesignerActionPropertyItem("PostFix",
                "Post Fix", "Appearance",
                "Sets the post fix text."));

            

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
