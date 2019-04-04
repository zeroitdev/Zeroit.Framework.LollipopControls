// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopMaskedTextBox.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{

    /// <summary>
    /// A class collection for rendering Lollipop Masked TextBox
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("TextChanged")]
    [Designer(typeof(LollipopMaskedTextBoxDesigner))]
    public class ZeroitLollipopMaskedTextBox : Control
    {

        #region  Variables

        /// <summary>
        /// The lollipop tb
        /// </summary>
        MaskedTextBox LollipopTB = new MaskedTextBox();
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
        Color focusColor = ColorTranslator.FromHtml("#508ef5");

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
        Color enabledUnFocusedColor = ColorTranslator.FromHtml("#dbdbdb");

        /// <summary>
        /// The disabled un focused color
        /// </summary>
        Color disabledUnFocusedColor = ColorTranslator.FromHtml("#e9ecee");
        /// <summary>
        /// The disabled string color
        /// </summary>
        Color disabledStringColor = ColorTranslator.FromHtml("#babbbd");
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
        /// Gets or sets the maximum length of the characters.
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
        /// Gets or sets the color when focused.
        /// </summary>
        /// <value>The color of when focused.</value>
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
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(true)]
        public Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
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
        /// Gets or sets the text mask format.
        /// </summary>
        /// <value>The text mask format.</value>
        public MaskFormat TextMaskFormat
        {
            get { return LollipopTB.TextMaskFormat; }
            set
            {
                LollipopTB.TextMaskFormat = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLollipopMaskedTextBox" /> is multiline.
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
        /// Gets or sets a value indicating whether to set read only attribute.
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
        /// <value><c>true</c> if the control is enabled; otherwise, <c>false</c>.</value>
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
        /// Gets or sets a value indicating whether to skip literals.
        /// </summary>
        /// <value><c>true</c> if skip literals; otherwise, <c>false</c>.</value>
        public bool SkipLiterals
        {
            get { return LollipopTB.SkipLiterals; }
            set
            {
                LollipopTB.SkipLiterals = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether short cuts  is enabled.
        /// </summary>
        /// <value><c>true</c> if short cuts enabled; otherwise, <c>false</c>.</value>
        public bool ShortCutsEnabled
        {
            get { return LollipopTB.ShortcutsEnabled; }
            set
            {
                LollipopTB.ShortcutsEnabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to reset on space.
        /// </summary>
        /// <value><c>true</c> if reset on space; otherwise, <c>false</c>.</value>
        public bool ResetOnSpace
        {
            get { return LollipopTB.ResetOnSpace; }
            set
            {
                LollipopTB.ResetOnSpace = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to reset on prompt.
        /// </summary>
        /// <value><c>true</c> if reset on prompt; otherwise, <c>false</c>.</value>
        public bool ResetOnPrompt
        {
            get { return LollipopTB.ResetOnPrompt; }
            set
            {
                LollipopTB.ResetOnPrompt = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to reject input on first failure.
        /// </summary>
        /// <value><c>true</c> if reject input on first failure; otherwise, <c>false</c>.</value>
        public bool RejectInputOnFirstFailure
        {
            get { return LollipopTB.RejectInputOnFirstFailure; }
            set
            {
                LollipopTB.RejectInputOnFirstFailure = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether beep on error.
        /// </summary>
        /// <value><c>true</c> if beep on error; otherwise, <c>false</c>.</value>
        public bool BeepOnError
        {
            get { return LollipopTB.BeepOnError; }
            set
            {
                LollipopTB.BeepOnError = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable ASCII only.
        /// </summary>
        /// <value><c>true</c> if ASCII only; otherwise, <c>false</c>.</value>
        public bool AsciiOnly
        {
            get { return LollipopTB.AsciiOnly; }
            set
            {
                LollipopTB.AsciiOnly = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow prompt as input.
        /// </summary>
        /// <value><c>true</c> if allow prompt as input; otherwise, <c>false</c>.</value>
        public bool AllowPromptAsInput
        {
            get { return LollipopTB.AllowPromptAsInput; }
            set
            {
                LollipopTB.AllowPromptAsInput = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide selection.
        /// </summary>
        /// <value><c>true</c> if hide selection; otherwise, <c>false</c>.</value>
        public bool HideSelection
        {
            get { return LollipopTB.HideSelection; }
            set
            {
                LollipopTB.HideSelection = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide prompt on leave.
        /// </summary>
        /// <value><c>true</c> if hide prompt on leave; otherwise, <c>false</c>.</value>
        public bool HidePromptOnLeave
        {
            get { return LollipopTB.HidePromptOnLeave; }
            set
            {
                LollipopTB.HidePromptOnLeave = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the prompt character.
        /// </summary>
        /// <value>The prompt character.</value>
        public char PromptChar
        {
            get { return LollipopTB.PromptChar; }
            set
            {
                LollipopTB.PromptChar = value;
                Invalidate();
            }
        }



        /// <summary>
        /// Gets or sets the mask.
        /// </summary>
        /// <value>The mask.</value>
        //[Editor("System.Windows.Forms.Design.MaskPropertyEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Editor("System.Windows.Forms.Design.MaskPropertyEditor", typeof(UITypeEditor))]
        public string Mask
        {
            get { return LollipopTB.Mask; }
            set
            {

                LollipopTB.Mask = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the insert key mode.
        /// </summary>
        /// <value>The insert key mode.</value>
        public InsertKeyMode InsertKeyMode
        {
            get { return LollipopTB.InsertKeyMode; }
            set
            {
                LollipopTB.InsertKeyMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the cut and copy mask format.
        /// </summary>
        /// <value>The cut and copy mask format.</value>
        public MaskFormat CutCopyMaskFormat
        {
            get { return LollipopTB.CutCopyMaskFormat; }
            set
            {
                LollipopTB.CutCopyMaskFormat = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public CultureInfo Culture
        {
            get { return LollipopTB.Culture; }
            set
            {
                LollipopTB.Culture = value;
                Invalidate();
            }
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
            //LollipopTB.ScrollBars = ScrollBars.None;
            LollipopTB.KeyDown += OnKeyDown;

            LollipopTB.GotFocus += (sender, args) => Focus = true; AnimationTimer.Start();
            LollipopTB.LostFocus += (sender, args) => Focus = false; AnimationTimer.Start();

            LollipopTB.Mask = Text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopMaskedTextBox" /> class.
        /// </summary>
        public ZeroitLollipopMaskedTextBox()
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
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
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

            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            G.Dispose();
            B.Dispose();
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
    /// Class LollipopMaskedTextBoxDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopMaskedTextBoxDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopMaskedTextBoxSmartTagActionList(this.Component));
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
    /// Class LollipopMaskedTextBoxSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopMaskedTextBoxSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopMaskedTextBox colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopMaskedTextBoxSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopMaskedTextBoxSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopMaskedTextBox;

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

        //where Color1_inactive property exist. Replace with an existing property
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

        /// <summary>
        /// Gets or sets the color of the focused.
        /// </summary>
        /// <value>The color of the focused.</value>
        public Color FocusedColor
        {
            get { return colUserControl.FocusedColor; }
            set
            {
                GetPropertyByName("FocusedColor").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("FocusedColor",
                "Focused Color", "Appearance",
                "Sets the focused color."));

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
