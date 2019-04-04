// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopTextBox.cs" company="Zeroit Dev Technologies">
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
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{

    /// <summary>
    /// A class collection for rendering a Lollipop textbox.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("TextChanged")]
    [Designer(typeof(LollipopTextBoxDesigner))]
    public class ZeroitLollipopTextBox : Control
    {

        #region  Variables

        /// <summary>
        /// The lollipop tb
        /// </summary>
        TextBox LollipopTB = new TextBox();
        /// <summary>
        /// The aln type
        /// </summary>
        HorizontalAlignment ALNType;

        /// <summary>
        /// The maxchars
        /// </summary>
        int maxchars = 32767;
        /// <summary>
        /// The read only
        /// </summary>
        bool readOnly;
        /// <summary>
        /// The previous read only
        /// </summary>
        bool previousReadOnly;
        /// <summary>
        /// The multiline
        /// </summary>
        bool multiline;
        /// <summary>
        /// The is password masked
        /// </summary>
        bool isPasswordMasked = false;
        /// <summary>
        /// The enable
        /// </summary>
        bool Enable = true;

        /// <summary>
        /// The animation timer
        /// </summary>
        Timer AnimationTimer = new Timer { Interval = 1 };
        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();

        /// <summary>
        /// The focus
        /// </summary>
        bool Focus = false;

        /// <summary>
        /// The size animation
        /// </summary>
        float SizeAnimation = 0;
        /// <summary>
        /// The size inc decimal
        /// </summary>
        float SizeInc_Dec;

        /// <summary>
        /// The point animation
        /// </summary>
        float PointAnimation;
        /// <summary>
        /// The point inc decimal
        /// </summary>
        float PointInc_Dec;

        //Color fontColor = ColorTranslator.FromHtml("#999999");
        /// <summary>
        /// The focus color
        /// </summary>
        Color focusColor = Color.FromArgb(80, 142, 245);

        /// <summary>
        /// The enabled focused color
        /// </summary>
        Color EnabledFocusedColor;
        /// <summary>
        /// The enabled string color
        /// </summary>
        Color EnabledStringColor;

        /// <summary>
        /// The enabled un focused color
        /// </summary>
        Color enabledUnFocusedColor = Color.FromArgb(219, 219, 219);

        /// <summary>
        /// The disabled un focused color
        /// </summary>
        Color disabledUnFocusedColor = Color.FromArgb(233, 236, 238);
        /// <summary>
        /// The disabled string color
        /// </summary>
        Color disabledStringColor = Color.FromArgb(186, 187, 189);
        /// <summary>
        /// The text background color
        /// </summary>
        private Color textBackgroundColor;
        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        public HorizontalAlignment TextAlignment
        {
            get
            {
                return ALNType;
            }
            set
            {
                ALNType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        [Category("Behavior")]
        public int MaxLength
        {
            get
            {
                return maxchars;
            }
            set
            {
                maxchars = value;
                LollipopTB.MaxLength = MaxLength;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLollipopTextBox" /> supports multiline.
        /// </summary>
        /// <value><c>true</c> if multiline; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool Multiline
        {
            get
            {
                return multiline;
            }
            set
            {
                multiline = value;
                if (LollipopTB != null)
                {
                    LollipopTB.Multiline = value;

                    if (value)
                    {
                        LollipopTB.Location = new Point(-3, 1);
                        LollipopTB.Width = Width + 3;
                        LollipopTB.Height = Height - 6;
                    }
                    else
                    {
                        LollipopTB.Location = new Point(0, 1);
                        LollipopTB.Width = Width;
                        Height = 24;
                    }
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use system password character.
        /// </summary>
        /// <value><c>true</c> if use system password character; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return isPasswordMasked;
            }
            set
            {
                LollipopTB.UseSystemPasswordChar = UseSystemPasswordChar;
                isPasswordMasked = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether read only attribute is enabled.
        /// </summary>
        /// <value><c>true</c> if read only; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
                if (LollipopTB != null)
                {
                    LollipopTB.ReadOnly = value;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is enabled.
        /// </summary>
        /// <value><c>true</c> if the control should be enabled; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool IsEnabled
        {
            get { return Enable; }
            set
            {
                Enable = value;

                if (IsEnabled)
                {
                    readOnly = previousReadOnly;
                    LollipopTB.ReadOnly = previousReadOnly;
                    LollipopTB.ForeColor = EnabledStringColor;
                }
                else
                {
                    previousReadOnly = ReadOnly;
                    ReadOnly = true;
                    LollipopTB.ForeColor = disabledStringColor;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the control when focused.
        /// </summary>
        /// <value>The color of the control when focused.</value>
        [Category("Appearance")]
        public Color FocusedColor
        {
            get { return focusColor; }
            set
            {
                focusColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        public bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(true)]
        public Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                LollipopTB.Font = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        [Browsable(true)]
        public Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the background text.
        /// </summary>
        /// <value>The color of the background text.</value>
        public Color TextBackgroundColor
        {
            get { return textBackgroundColor; }
            set
            {
                textBackgroundColor = value;
                LollipopTB.BackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the password character.
        /// </summary>
        /// <value>The password character.</value>
        public char PasswordChar
        {
            get { return LollipopTB.PasswordChar; }
            set
            {
                
                LollipopTB.PasswordChar = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether should be word wrapped.
        /// </summary>
        /// <value><c>true</c> if word wrap; otherwise, <c>false</c>.</value>
        public bool WordWrap
        {
            get { return LollipopTB.WordWrap; }
            set
            {
                LollipopTB.WordWrap = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the focused color when enabled.
        /// </summary>
        /// <value>The focused color when enabled.</value>
        public Color EnabledUnFocusedColor
        {
            get { return enabledUnFocusedColor; }
            set { enabledUnFocusedColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the focused color when disabled.
        /// </summary>
        /// <value>The focused color when disabled.</value>
        public Color DisabledUnFocusedColor
        {
            get { return disabledUnFocusedColor; }
            set { disabledUnFocusedColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the text color when disabled.
        /// </summary>
        /// <value>The text color when disabled.</value>
        public Color DisabledStringColor
        {
            get { return disabledStringColor; }
            set { disabledStringColor = value; Invalidate(); }
        }

        #endregion

        #region  Events

        /// <summary>
        /// Handles the <see cref="E:KeyDown" /> event.
        /// </summary>
        /// <param name="Obj">The object.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        protected void OnKeyDown(object Obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                LollipopTB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                LollipopTB.Copy();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                LollipopTB.Cut();
                e.SuppressKeyPress = true;
            }
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
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            LollipopTB.Focus();
            LollipopTB.SelectionLength = 0;
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            PointAnimation = Width / 2;
            SizeInc_Dec = Width / 18;
            PointInc_Dec = Width / 36;

            LollipopTB.Width = Width;


            if (multiline)
            {
                LollipopTB.Location = new Point(-3, 1);
                LollipopTB.Width = Width + 3;
                LollipopTB.Height = Height - 6;
            }
            else
            {
                LollipopTB.Location = new Point(0, 1);
                LollipopTB.Width = Width;
                Height = 24;
            }
        }

        #endregion


        /// <summary>
        /// Adds the text box.
        /// </summary>
        private void AddTextBox()
        {
            textBackgroundColor = BackColor;

            LollipopTB.Location = new Point(0, 1);
            LollipopTB.Size = new Size(Width, 20);
            LollipopTB.Text = Text;

            LollipopTB.BorderStyle = BorderStyle.None;
            LollipopTB.TextAlign = HorizontalAlignment.Left;
            LollipopTB.Font = font.Roboto_Regular10;
            LollipopTB.UseSystemPasswordChar = UseSystemPasswordChar;
            LollipopTB.Multiline = false;
            LollipopTB.BackColor = textBackgroundColor;
            LollipopTB.ScrollBars = ScrollBars.None;
            LollipopTB.KeyDown += OnKeyDown;

            LollipopTB.GotFocus += (sender, args) => Focus = true; AnimationTimer.Start();
            LollipopTB.LostFocus += (sender, args) => Focus = false; AnimationTimer.Start();

            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopTextBox" /> class.
        /// </summary>
        public ZeroitLollipopTextBox()
        {
            Width = 300;
            DoubleBuffered = true;
            previousReadOnly = ReadOnly;

            AddTextBox();
            Controls.Add(LollipopTB);

            LollipopTB.TextChanged += (sender, args) => Text = LollipopTB.Text;
            base.TextChanged += (sender, args) => LollipopTB.Text = Text;

            AnimationTimer.Tick += new EventHandler(AnimationTick);

            ForeColor = Color.FromArgb(153, 153, 153);

            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            //Bitmap B = new Bitmap(Width, Height);
            Graphics G = e.Graphics;
            G.Clear(BackColor);

            EnabledStringColor = ForeColor;
            EnabledFocusedColor = focusColor;

            LollipopTB.TextAlign = TextAlignment;
            LollipopTB.ForeColor = IsEnabled ? EnabledStringColor : disabledStringColor;
            LollipopTB.UseSystemPasswordChar = UseSystemPasswordChar;

            G.DrawLine(new Pen(new SolidBrush(IsEnabled ? enabledUnFocusedColor : disabledUnFocusedColor)), new Point(0, Height - 2), new Point(Width, Height - 2));

            if (IsEnabled)
            {
                G.FillRectangle(new SolidBrush(EnabledFocusedColor), PointAnimation, (float)Height - 3, SizeAnimation, 2);
                
            }

            
            
            G.DrawString("PlaceHolder Text", Font, new SolidBrush(Color.White), new Point(0, 2));
            
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
                if (SizeAnimation < Width)
                {
                    SizeAnimation += SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation > 0)
                {
                    PointAnimation -= PointInc_Dec;
                    this.Invalidate();
                }
            }
            else
            {
                if (SizeAnimation > 0)
                {
                    SizeAnimation -= SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation < Width / 2)
                {
                    PointAnimation += PointInc_Dec;
                    this.Invalidate();
                }
            }
        }


        

    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class LollipopTextBoxDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopTextBoxDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopTextBoxSmartTagActionList(this.Component));
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
    /// Class LollipopTextBoxSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopTextBoxSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopTextBox colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopTextBoxSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopTextBoxSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopTextBox;

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
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public new string Text
        {
            get { return colUserControl.Text; }
            set
            {
                GetPropertyByName("Text").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));
            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("Text",
                                 "Text", "Appearance",
                                 "Sets the text."));
            

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
