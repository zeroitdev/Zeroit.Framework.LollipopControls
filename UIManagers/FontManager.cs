﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="FontManager.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Zeroit.Framework.LollipopControls.Properties;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;

namespace Zeroit.Framework.LollipopControls.Helpers
{
    /// <summary>
    /// Class FontManager.
    /// </summary>
    public class FontManager
    {

        /// <summary>
        /// The roboto medium15
        /// </summary>
        public Font Roboto_Medium15;
        /// <summary>
        /// The roboto medium10
        /// </summary>
        public Font Roboto_Medium10;
        /// <summary>
        /// The roboto regular10
        /// </summary>
        public Font Roboto_Regular10;


        /// <summary>
        /// The roboto medium9
        /// </summary>
        public Font Roboto_Medium9;
        /// <summary>
        /// The roboto regular9
        /// </summary>
        public Font Roboto_Regular9;


        /// <summary>
        /// Initializes a new instance of the <see cref="FontManager"/> class.
        /// </summary>
        public FontManager()
        {
            Roboto_Medium15 = new Font(LoadFont(Resources.Roboto_Medium), 15f);
            Roboto_Medium10 = new Font(LoadFont(Resources.Roboto_Medium), 10f);
            Roboto_Regular10 = new Font(LoadFont(Resources.Roboto_Regular), 10f);

            Roboto_Medium9 = new Font(LoadFont(Resources.Roboto_Medium), 9f);
            Roboto_Regular9 = new Font(LoadFont(Resources.Roboto_Regular), 9f);
        }

        /// <summary>
        /// The private font collection
        /// </summary>
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        /// <summary>
        /// Adds the font memory resource ex.
        /// </summary>
        /// <param name="pbFont">The pb font.</param>
        /// <param name="cbFont">The cb font.</param>
        /// <param name="pvd">The PVD.</param>
        /// <param name="pcFonts">The pc fonts.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);

        /// <summary>
        /// Loads the font.
        /// </summary>
        /// <param name="fontResource">The font resource.</param>
        /// <returns>FontFamily.</returns>
        private FontFamily LoadFont(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);

            return privateFontCollection.Families.Last();
        }
    }

}
