﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;

namespace FinancialPortal.Helpers
{
    public static class ImageUploadValidator
    {
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            if (file == null)
                return false;
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                           ImageFormat.Png.Equals(img.RawFormat) ||
                           ImageFormat.Gif.Equals(img.RawFormat);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
    