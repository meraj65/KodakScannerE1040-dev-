using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WIA;

namespace KodakScannerE1040.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuplexScannerController : ControllerBase
    {

        //[HttpGet("scan")]
        //public IActionResult Scan()
        //{
        //    try
        //    {
        //        var deviceManager = new DeviceManager();
        //        DeviceInfo deviceInfo = null;

        //        // Find Kodak E1040 scanner
        //        for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
        //        {
        //            if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
        //            {
        //                deviceInfo = deviceManager.DeviceInfos[i];
        //                break;
        //            }
        //        }

        //        if (deviceInfo == null)
        //        {
        //            return BadRequest("No scanner found.");
        //        }

        //        // Connect to the scanner
        //        Device device = deviceInfo.Connect();
        //        Item scannerItem = device.Items[1];

        //        // Set scanner source to Feeder
        //        SetFeederSource(scannerItem);

        //        // Enable Duplex mode
        //        SetDuplexMode(scannerItem);

        //        // Allow multiple pages
        //        SetPageCount(scannerItem);

        //        // Scan both sides in one pass
        //        List<byte[]> scannedImages = ScanBothSides(scannerItem);

        //        // Merge into a multi-page TIFF
        //        byte[] multiPageTiff = MergeTIFFImages(scannedImages);

        //        return File(multiPageTiff, "image/tiff", "ScannedFile.tiff");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Scanning failed.", error = ex.Message });
        //    }
        //}

        //private void SetPageCount(Item scannerItem)
        //{
        //    try
        //    {
        //        // Allow scanning multiple pages (WIA_DPS_PAGES, property 3096)
        //        Property pageProperty = scannerItem.Properties["3096"];
        //        if (pageProperty != null)
        //        {
        //            object allPages = -1; // -1 means scan all available pages
        //            pageProperty.set_Value(ref allPages);
        //        }

        //        Console.WriteLine("Page count set to scan all available pages.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error setting page count: " + ex.Message);
        //    }
        //}

        //private void SetDuplexMode(Item scannerItem)
        //{
        //    try
        //    {
        //        // Enable Duplex Scanning (WIA_DPS_DOCUMENT_HANDLING_SELECT, property 3088)
        //        Property duplexProperty = scannerItem.Properties["3088"];
        //        if (duplexProperty != null)
        //        {
        //            object duplexMode = 1; // Enable Duplex
        //            duplexProperty.set_Value(ref duplexMode);
        //        }

        //        Console.WriteLine("Duplex mode enabled.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error enabling duplex mode: " + ex.Message);
        //    }
        //}

        //private void SetFeederSource(Item scannerItem)
        //{
        //    try
        //    {
        //        // Set scanner to Feeder mode (WIA_DPS_DOCUMENT_HANDLING_SELECT, property 3087)
        //        Property feederProperty = scannerItem.Properties["3087"];
        //        if (feederProperty != null)
        //        {
        //            object feederMode = 1; // 1 = Feeder Mode
        //            feederProperty.set_Value(ref feederMode);
        //        }

        //        Console.WriteLine("Scanner source set to Feeder.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error setting scanner source: " + ex.Message);
        //    }
        //}

        //private List<byte[]> ScanBothSides(Item scannerItem)
        //{
        //    List<byte[]> scannedImages = new List<byte[]>();

        //    try
        //    {
        //        while (true) // Keep scanning until an error occurs
        //        {
        //            try
        //            {
        //                //ImageFile imageFile = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatTIFF);
        //               // ImageFile imageFile = (ImageFile)scannerItem.Transfer("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}");
        //                ImageFile imageFile = (ImageFile)scannerItem.Transfer("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}");

        //                if (imageFile != null)
        //                {
        //                    scannedImages.Add((byte[])imageFile.FileData.get_BinaryData());
        //                }
        //            }
        //            catch
        //            {
        //                break; // Stop when no more pages are available
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error scanning both sides: " + ex.Message);
        //    }

        //    return scannedImages;
        //}


        //private byte[] MergeTIFFImages(List<byte[]> images)
        //{
        //    using (MemoryStream output = new MemoryStream())
        //    {
        //        EncoderParameters encoderParams = new EncoderParameters(1);
        //        encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.MultiFrame);

        //        ImageCodecInfo tiffCodec = ImageCodecInfo.GetImageEncoders().First(c => c.MimeType == "image/tiff");

        //        using (Image firstImage = Image.FromStream(new MemoryStream(images[0])))
        //        {
        //            firstImage.Save(output, tiffCodec, encoderParams);

        //            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);

        //            for (int i = 1; i < images.Count; i++)
        //            {
        //                using (Image nextImage = Image.FromStream(new MemoryStream(images[i])))
        //                {
        //                    firstImage.SaveAdd(nextImage, encoderParams);
        //                }
        //            }

        //            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);
        //            firstImage.SaveAdd(encoderParams);
        //        }

        //        return output.ToArray();
        //    }
        //}



    }
}
