using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using NTwain;
using NTwain.Data;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace KodakScannerE1040.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class nTwainValuesController : ControllerBase
    {
        //private TwainSession _twain;
        //public nTwainValuesController()
        //{
        //    _twain = new TwainSession(TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetExecutingAssembly()));
        //    _twain.Open();
        //}

        //[HttpGet("scan")]
        //public IActionResult Scan()
        //{
        //    try
        //    {
        //        // 1️⃣ Get the first available scanner
        //        var scanner = _twain.FirstOrDefault();
        //        if (scanner == null)
        //        {
        //            return BadRequest("No scanner found.");
        //        }

        //        // 2️⃣ Open the scanner
        //        scanner.Open();

        //        // 3️⃣ Enable Feeder (ADF) & Duplex Mode
        //        var feederCap = scanner.Capabilities.CapFeederEnabled;
        //        if (feederCap.CanSet) feederCap.SetValue(BoolType.True);

        //        var duplexCap = scanner.Capabilities.CapDuplexEnabled;
        //        if (duplexCap.CanSet) duplexCap.SetValue(BoolType.True);

        //        var images = new List<Bitmap>();
        //        AutoResetEvent scanCompleteEvent = new AutoResetEvent(false);

        //        // 4️⃣ Capture scanned images
        //        _twain.DataTransferred += (s, e) =>
        //        {
        //            if (e.NativeData != IntPtr.Zero)
        //            {
        //                using (var img = Image.FromHbitmap(e.NativeData))
        //                {
        //                    images.Add(new Bitmap(img));
        //                }
        //            }
        //        };

        //        // 🔹 Connect to scanner
        //        //scanner = scannerDevice.Connect(); // Ensure this line executes properly!

        //        // 🔹 Now enable scanner (No UI mode)
        //        scanner.Enable(SourceEnableMode.NoUI, false, IntPtr.Zero);

        //        // 6️⃣ Wait for all pages to be scanned
        //        while (true)
        //        {
        //            var feederLoadedCap = scanner.Capabilities.CapFeederLoaded;

        //            if (feederLoadedCap == null)
        //            {
        //                //Console.WriteLine("FeederLoaded capability not supported.");
        //                return BadRequest("FeederLoaded capability not supported.");
        //            }

        //            //object value;
        //            //feederLoadedCap.GetValue(out value);  // ✅ Correct if supported
        //            //bool isFeederLoaded = Convert.ToBoolean(value);

        //            //if (!isFeederLoaded)
        //            //{
        //            //    Console.WriteLine("Feeder is empty.");
        //            //}

        //        }


        //        // 7️⃣ Convert images to multi-page TIFF
        //        if (images.Count == 0)
        //        {
        //            return BadRequest("No images scanned.");
        //        }

        //        byte[] tiffBytes = ConvertToMultiPageTiff(images);
        //        return File(tiffBytes, "image/tiff", "ScannedFile.tiff");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Scanning failed.", error = ex.Message });
        //    }
        //    finally
        //    {
        //        // 8️⃣ Always close the scanner
        //        _twain.CurrentSource?.Close();
        //    }
        //}


        //private byte[] ConvertToMultiPageTiff(List<Bitmap> images)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        Encoder encoder = Encoder.SaveFlag;
        //        EncoderParameters encoderParams = new EncoderParameters(1);
        //        ImageCodecInfo tiffCodec = ImageCodecInfo.GetImageEncoders()
        //            .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);

        //        if (tiffCodec == null)
        //            throw new Exception("TIFF codec not found.");

        //        images[0].Save(ms, tiffCodec, null);
        //        encoderParams.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.MultiFrame);

        //        for (int i = 1; i < images.Count; i++)
        //        {
        //            images[0].SaveAdd(images[i], encoderParams);
        //        }

        //        encoderParams.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.Flush);
        //        images[0].SaveAdd(encoderParams);

        //        return ms.ToArray();
        //    }
        //}

    }
}
