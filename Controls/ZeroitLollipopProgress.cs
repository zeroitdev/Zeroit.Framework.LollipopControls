// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopProgress.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Text;
using Zeroit.Framework.LollipopControls.Animations;

namespace Zeroit.Framework.LollipopControls.Controls
{
    /// <summary>
    /// Material design-like progress bar
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="Zeroit.Framework.LollipopControls.Controls.IMaterialControl" />
    [Designer(typeof(ZeroitLollipopFlatProgressDesigner))]
    public class ZeroitLollipopFlatProgress : Control, IMaterialControl
    {

        #region Private Fields
        /// <summary>
        /// The progress transparency
        /// </summary>
        private int progressTransparency = 100;
        /// <summary>
        /// The orientation
        /// </summary>
        private Orientation _Orientation = Orientation.Horizontal;
        /// <summary>
        /// The inverted progress bar
        /// </summary>
        private bool _InvertedProgressBar;

        /// <summary>
        /// The animation manager start
        /// </summary>
        private Animations.AnimationManager AnimationManagerStart, AnimationManagerEnd;
        /// <summary>
        /// The maximum
        /// </summary>
        private int _Maximum;
        /// <summary>
        /// The minimum
        /// </summary>
        private int _Minimum;
        /// <summary>
        /// The step
        /// </summary>
        private int _Step;
        /// <summary>
        /// The value
        /// </summary>
        private int _Value;

        /// <summary>
        /// The style
        /// </summary>
        private ProgressStyle _Style;

        /// <summary>
        /// The background
        /// </summary>
        private Color background = Color.FromArgb(80, 80, 80);

        /// <summary>
        /// The progress color
        /// </summary>
        private Color progressColor = Color.RoyalBlue;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopProgress" /> class.
        /// </summary>
        public ZeroitLollipopFlatProgress()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Maximum = 100;
            AnimationManagerStart = new AnimationManager();
            AnimationManagerStart.AnimationType = Animations.AnimationType.EaseInOut;
            AnimationManagerStart.Increment = 0.025;
            AnimationManagerStart.SetProgress(0);
            AnimationManagerStart.OnAnimationFinished += AnimationManager_OnAnimationFinished;
            AnimationManagerStart.OnAnimationProgress += AnimationManager_OnAnimationProgress;
            AnimationManagerEnd = new Animations.AnimationManager();
            AnimationManagerEnd.AnimationType = Animations.AnimationType.EaseInOut;
            AnimationManagerEnd.Increment = 0.025;
            AnimationManagerEnd.OnAnimationProgress += AnimationManager_OnAnimationProgress;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        [Browsable(false)]
        public int Depth { get; set; }

        ///// <summary>
        ///// Gets the skin manager.
        ///// </summary>
        ///// <value>
        ///// The skin manager.
        ///// </value>
        //[Browsable(false)]
        //public MaterialSkinManager SkinManager
        //{
        //    get { return MaterialSkinManager.Instance; }
        //}

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>The state of the mouse.</value>
        [Browsable(false)]
        public MouseState MouseState { get; set; }


        /// <summary>
        /// Sets the Orientation of The Progessbar
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
        {
            get { return _Orientation; }
            set
            {
                if (value != _Orientation)
                {
                    _Orientation = value;

                    int tmp = _Orientation == System.Windows.Forms.Orientation.Horizontal ? Height : Width;

                    SetBoundsCore(Location.X, Location.Y, Height, Width, BoundsSpecified.All);

                    if (_Orientation == System.Windows.Forms.Orientation.Horizontal)
                    {
                        Width = tmp;
                    }
                    else
                    {
                        Height = tmp;
                    }

                }
            }
        }

        /// <summary>
        /// Inverts the Progressbar
        /// </summary>
        /// <value><c>true</c> if [inverted progress bar]; otherwise, <c>false</c>.</value>
        public bool InvertedProgressBar { get { return _InvertedProgressBar; } set { _InvertedProgressBar = value; Invalidate(); } }

        /// <summary>
        /// Enum ProgressStyle
        /// </summary>
        public enum ProgressStyle
        {
            /// <summary>
            /// The determinate
            /// </summary>
            Determinate,
            /// <summary>
            /// The indeterminate
            /// </summary>
            Indeterminate
        }

        /// <summary>
        /// Gets or sets the maxium Progress Value
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value <= Minimum)
                {
                    _Maximum = Minimum + 1;
                }
                else
                {
                    _Maximum = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Minimum Progress Value;
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                if (value >= Maximum)
                {
                    _Minimum = Maximum - 1;
                }
                else
                {
                    _Minimum = value;
                }
            }
        }

        /// <summary>
        /// Gets or Sets The Amount of Progress on Step consists off
        /// </summary>
        /// <value>The step.</value>
        public int Step
        {
            get
            {
                return _Step;
            }
            set
            {
                if (value >= Maximum - Minimum)
                {
                    _Step = Maximum - Minimum;
                }
                else if (value <= Minimum - Maximum)
                {
                    _Step = Minimum - Maximum;
                }
                else
                {
                    _Step = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Current Value
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                if (_Value > Maximum)
                {
                    _Value = Maximum;
                }
                else if (_Value < Minimum)
                {
                    _Value = Minimum;
                }
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public ProgressStyle Style
        {
            get
            {
                return _Style;
            }
            set
            {
                if (_Style == ProgressStyle.Determinate && value == ProgressStyle.Indeterminate)
                {
                    _Style = value;
                    AnimationManagerEnd.StartNewAnimation(Animations.AnimationDirection.In);
                }
                else if (_Style == ProgressStyle.Indeterminate && value == ProgressStyle.Determinate)
                {
                    _Style = value;
                    if (AnimationManagerEnd.IsAnimating() || AnimationManagerStart.IsAnimating())
                    {
                        AnimationManagerEnd.SetProgress(1);
                        AnimationManagerStart.SetProgress(1);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Color Background
        {
            get { return background; }
            set
            {

                background = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the progress.
        /// </summary>
        /// <value>The color of the progress.</value>
        public Color ProgressColor
        {
            get { return progressColor; }
            set
            {
                progressColor = Color.FromArgb(ProgressTransparency, value.R, value.G, value.B);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the progress transparency.
        /// </summary>
        /// <value>The progress transparency.</value>
        public int ProgressTransparency
        {
            get { return progressTransparency; }
            set
            {
                ProgressColor = Color.FromArgb(value, progressColor);

                progressTransparency = value;
                Invalidate();
            }
        }


        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
        /// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
        /// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
        /// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (Orientation == System.Windows.Forms.Orientation.Horizontal)
            {
                base.SetBoundsCore(x, y, width, 5, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, 5, height, specified);
            }

        }

        /// <summary>
        /// Performs the step.
        /// </summary>
        public void PerformStep()
        {
            Value += Step;
            Invalidate();
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            if (_Style == ProgressStyle.Determinate)
            {
                if (Orientation == System.Windows.Forms.Orientation.Horizontal)
                {
                    var doneProgress = (int)(e.ClipRectangle.Width * ((double)Value / (Maximum - Minimum)));
                    if (InvertedProgressBar)
                    {
                        doneProgress = Width - doneProgress;
                        e.Graphics.FillRectangle(new SolidBrush(Background), 0, 0, doneProgress, e.ClipRectangle.Height);
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), doneProgress, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), 0, 0, doneProgress, e.ClipRectangle.Height);
                        e.Graphics.FillRectangle(new SolidBrush(Background), doneProgress, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                    }
                }
                else
                {
                    var doneProgress = (int)(e.ClipRectangle.Height * ((double)Value / Maximum));
                    if (InvertedProgressBar)
                    {
                        doneProgress = Height - doneProgress;

                        e.Graphics.FillRectangle(new SolidBrush(Background), 0, 0, e.ClipRectangle.Width, doneProgress);
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), 0, doneProgress, e.ClipRectangle.Height, e.ClipRectangle.Height);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), 0, 0, e.ClipRectangle.Height, doneProgress);
                        e.Graphics.FillRectangle(new SolidBrush(Background), 0, doneProgress, e.ClipRectangle.Width, e.ClipRectangle.Height);
                    }
                }
            }
            else
            {
                double startProgress = AnimationManagerStart.GetProgress();
                double EndProgress = AnimationManagerEnd.GetProgress();
                if (Orientation == System.Windows.Forms.Orientation.Horizontal)
                {
                    var doneLocation = (int)(e.ClipRectangle.Width * EndProgress);
                    var StartLocation = (int)(e.ClipRectangle.Width * startProgress);

                    if (InvertedProgressBar)
                    {
                        doneLocation = Width - doneLocation;
                        StartLocation = Width - StartLocation;
                        e.Graphics.FillRectangle(new SolidBrush(Background), 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), doneLocation, 0, StartLocation - doneLocation, e.ClipRectangle.Height);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Background), 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), StartLocation, 0, doneLocation - StartLocation, e.ClipRectangle.Height);

                    }
                }
                else
                {
                    var doneLocation = (int)(e.ClipRectangle.Height * EndProgress);
                    var StartLocation = (int)(e.ClipRectangle.Height * startProgress);

                    if (InvertedProgressBar)
                    {
                        doneLocation = Height - doneLocation;
                        StartLocation = Height - StartLocation;

                        e.Graphics.FillRectangle(new SolidBrush(Background), 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), 0, doneLocation, e.ClipRectangle.Height, StartLocation - doneLocation);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Background), 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                        e.Graphics.FillRectangle(new SolidBrush(ProgressColor), 0, StartLocation, e.ClipRectangle.Width, doneLocation - StartLocation);
                    }
                }
            }
        }



        /// <summary>
        /// Animations the manager on animation progress.
        /// </summary>
        /// <param name="sender">The sender.</param>
        void AnimationManager_OnAnimationProgress(object sender)
        {
            if (AnimationManagerEnd.GetProgress() >= 0.4 && !AnimationManagerStart.IsAnimating())
            {
                AnimationManagerStart.StartNewAnimation(Animations.AnimationDirection.In);
            }
            Invalidate();
        }

        /// <summary>
        /// Animations the manager on animation finished.
        /// </summary>
        /// <param name="sender">The sender.</param>
        void AnimationManager_OnAnimationFinished(object sender)
        {
            Invalidate();
            if (AnimationManagerStart.Increment == 0.02)
            {
                AnimationManagerStart.Increment = 0.025;
                AnimationManagerEnd.Increment = 0.025;
            }
            else
            {
                AnimationManagerStart.Increment = 0.02;
                AnimationManagerEnd.Increment = 0.02;
            }

            AnimationManagerStart.SetProgress(0);
            AnimationManagerEnd.SetProgress(0);
            AnimationManagerEnd.StartNewAnimation(Animations.AnimationDirection.In);
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

    //--------------- [Designer(typeof(ZeroitLollipopFlatProgressDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitLollipopFlatProgressDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitLollipopFlatProgressDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitLollipopFlatProgressSmartTagActionList(this.Component));
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
    /// Class ZeroitLollipopFlatProgressSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitLollipopFlatProgressSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopFlatProgress colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopFlatProgressSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitLollipopFlatProgressSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopFlatProgress;

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
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
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

        /// <summary>
        /// Gets or sets a value indicating whether [inverted progress bar].
        /// </summary>
        /// <value><c>true</c> if [inverted progress bar]; otherwise, <c>false</c>.</value>
        public bool InvertedProgressBar
        {
            get
            {
                return colUserControl.InvertedProgressBar;
            }
            set
            {
                GetPropertyByName("InvertedProgressBar").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public ZeroitLollipopFlatProgress.ProgressStyle Style
        {
            get
            {
                return colUserControl.Style;
            }
            set
            {
                GetPropertyByName("Style").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Color Background
        {
            get
            {
                return colUserControl.Background;
            }
            set
            {
                GetPropertyByName("Background").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the progress.
        /// </summary>
        /// <value>The color of the progress.</value>
        public Color ProgressColor
        {
            get
            {
                return colUserControl.ProgressColor;
            }
            set
            {
                GetPropertyByName("ProgressColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
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
        public int Minimum
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
        /// Gets or sets the step.
        /// </summary>
        /// <value>The step.</value>
        public int Step
        {
            get
            {
                return colUserControl.Step;
            }
            set
            {
                GetPropertyByName("Step").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the progress transparency.
        /// </summary>
        /// <value>The progress transparency.</value>
        public int ProgressTransparency
        {
            get
            {
                return colUserControl.ProgressTransparency;
            }
            set
            {
                GetPropertyByName("ProgressTransparency").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
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
            items.Add(new DesignerActionHeaderItem("Behaviour"));

            items.Add(new DesignerActionPropertyItem("InvertedProgressBar",
                "Inverted ProgressBar", "Behaviour",
                "Set to enable inversion."));

            items.Add(new DesignerActionPropertyItem("Orientation",
                "Orientation", "Behaviour",
                "Sets the orientation."));

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            //items.Add(new DesignerActionPropertyItem("BackColor",
            //                     "Back Color", "Appearance",
            //                     "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("Background",
                "Background", "Appearance",
                "Sets the background color."));


            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));


            items.Add(new DesignerActionPropertyItem("ProgressColor",
                "Progress Color", "Appearance",
                "Sets the progress color."));

            items.Add(new DesignerActionPropertyItem("Style",
                "Style", "Appearance",
                "Sets the progress style."));

            
            items.Add(new DesignerActionPropertyItem("Maximum",
                "Maximum", "Appearance",
                "Sets the maximum value."));


            items.Add(new DesignerActionPropertyItem("Minimum",
                "Minimum", "Appearance",
                "Sets the minimum value."));

            items.Add(new DesignerActionPropertyItem("Step",
                "Step", "Appearance",
                "Sets the step interval."));


            items.Add(new DesignerActionPropertyItem("ProgressTransparency",
                "Transparency", "Appearance",
                "Sets the progress transparency."));

            items.Add(new DesignerActionPropertyItem("Value",
                "Value", "Appearance",
                "Sets the progress value."));

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
