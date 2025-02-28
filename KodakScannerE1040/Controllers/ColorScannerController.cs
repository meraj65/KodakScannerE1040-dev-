using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTwain;
using WIA;
using static System.Windows.Forms.DataFormats;
using CommonDialog = WIA.CommonDialog;

namespace KodakScannerE1040.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorScannerController : ControllerBase
    {
        //[HttpGet("tiff_scan_color")]
        //public IActionResult tiff_scan_color()
        //{
        //    try
        //    {
        //        // ✅ Step 1: Get the scanner device
        //        DeviceManager deviceManager = new DeviceManager();
        //        DeviceInfo scannerDevice = null;


        //        for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
        //        {
        //            if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
        //            {
        //                scannerDevice = deviceManager.DeviceInfos[i];
        //                break;
        //            }
        //        }
        //        if (scannerDevice == null)
        //        {
        //            return BadRequest("No scanner found!");
        //        }

        //        // ✅ Step 2: Connect to the scanner
        //        Device scanner = scannerDevice.Connect();
        //        //scanner.Properties["Document Handling Select"].set_Value();
        //        //scanner.Properties["Document Handling Capabilities"].set_Value(4);
        //        scanner.Properties["Pages"].set_Value(1);

        //        //scanner.Properties["Sheet Feeder Registration"].set_Value(1);
        //        Console.WriteLine("Document Handling Select " + scanner.Properties["Document Handling Select"].get_Value());
        //        Console.WriteLine("Document Handling Capabilities " + scanner.Properties["Document Handling Capabilities"].get_Value());
        //        Console.WriteLine("Pages " + scanner.Properties["Pages"].get_Value());


        //        // ✅ Step 3: Get the scanning item (first item is usually the scanner)
        //        Item scanItem = scanner.Items[1];
        //        //scanItem.Transfer("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}");

        //        // ✅ Step 4: Set scanner properties (Color Mode, DPI, Page Size, etc.)
        //        SetScannerProperty(scanItem.Properties, 6146, 1); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
        //        SetScannerProperty(scanItem.Properties, 6147, 200); // DPI (Resolution)
        //        SetScannerProperty(scanItem.Properties, 6148, 8); // Bits per pixel


        //        SetScannerProperty(scanItem.Properties, 6151, 2); // Feeder Mode (Single double)
        //        SetScannerProperty(scanItem.Properties, 6151, 4); // Duplex Mode (Try Separately)
        //        SetScannerProperty(scanItem.Properties, 6151, 6); // Feeder + Duplex (Some Scanners)

        //        //SetScannerProperty(scanItem.Properties, 4103, 6); // Enable Feeder
        //        //SetScannerProperty(scanItem.Properties, 6151, 5); // Enable Duplex Mode





        //        // ✅ Step 5: Start scanning
        //        CommonDialog dialog = new CommonDialog();
        //        //Thread.Sleep(3000);
        //        ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}", true);
        //        //ImageFile imageFile1 = (ImageFile)dialog.ShowTransfer(scanItem, "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}", true);


        //        byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();
        //        //byte[] imageBytes1 = (byte[])imageFile1.FileData.get_BinaryData();
        //        System.IO.File.WriteAllBytes("D:\\KodakResource\\abc.tiff", imageBytes);

        //        return File(imageBytes, "image/tiff", "ScannedFile.tiff");
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message == "0x80210003")
        //        {
        //            return StatusCode(500, new { message = "Page not found in scanner." });
        //        }
        //        else
        //        {
        //            return StatusCode(500, new { message = "An error occurred during scanning", error = ex.Message });
        //        }
        //    }
        //}

        ///// ✅ Helper Method: Set Scanner Property
        //private void SetScannerProperty(IProperties properties, int propertyID, int value)
        //{
        //    try
        //    {
        //        foreach (Property property in properties)
        //        {
        //            Console.WriteLine($"Property ID: {property.PropertyID}, Name: {property.Name}");
        //            if (property.PropertyID == propertyID)
        //            {
        //                property.set_Value(value);
        //                return;
        //            }
        //        }
        //        Console.WriteLine($"Warning: Property {propertyID} not found on this scanner.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Failed to set property {propertyID}: {ex.Message}");
        //    }
        //}


    }
}


////****Meraj****

//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using NTwain;
//using WIA;
//using static System.Windows.Forms.DataFormats;
//using CommonDialog = WIA.CommonDialog;

//namespace KodakScannerE1040.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ColorScannerController : ControllerBase
//    {
//        //[HttpGet("tiff_scan_color")]
//        //public IActionResult tiff_scan_color()
//        //{
//        //    try
//        //    {
//        //        // ✅ Step 1: Get the scanner device
//        //        DeviceManager deviceManager = new DeviceManager();
//        //        DeviceInfo scannerDevice = null;

//        //        for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
//        //        {
//        //            if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
//        //            {
//        //                scannerDevice = deviceManager.DeviceInfos[i];
//        //                break;
//        //            }
//        //        }
//        //        if (scannerDevice == null)
//        //        {
//        //            return BadRequest("No scanner found!");
//        //        }

//        //        // ✅ Step 2: Connect to the scanner
//        //        Device scanner = scannerDevice.Connect();



//        //        // ✅ Step 3: Get the scanning item (first item is usually the scanner)
//        //        Item scanItem = scanner.Items[1];

//        //        // ✅ Step 4: Set scanner properties (Color Mode, DPI, Page Size, etc.)
//        //        SetScannerProperty(scanItem.Properties, 6146, 1); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
//        //        SetScannerProperty(scanItem.Properties, 6147, 200); // DPI (Resolution)
//        //        SetScannerProperty(scanItem.Properties, 6148, 8); // Bits per pixel


//        //        //SetScannerProperty(scanItem.Properties, 6151, 2); // Feeder Mode (Single double)
//        //        //SetScannerProperty(scanItem.Properties, 6151, 4); // Duplex Mode (Try Separately)
//        //        //SetScannerProperty(scanItem.Properties, 6151, 6); // Feeder + Duplex (Some Scanners)

//        //        SetScannerProperty(scanItem.Properties, 6151, 4); // Enable Feeder
//        //        SetScannerProperty(scanItem.Properties, 6160, 1); // Enable Duplex Mode

//        //        SetScannerProperty(scanItem.Properties, 3088, 1);



//        //        // ✅ Step 5: Start scanning
//        //        CommonDialog dialog = new CommonDialog();
//        //        //Thread.Sleep(30000);
//        //        ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}", true);

//        //        byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();
//        //        return File(imageBytes, "image/tiff", "ScannedFile.tiff");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (ex.Message == "0x80210003")
//        //        {
//        //            return StatusCode(500, new { message = "Page not found in scanner." });
//        //        }
//        //        else
//        //        {
//        //            return StatusCode(500, new { message = "An error occurred during scanning", error = ex.Message });
//        //        }
//        //    }
//        //}

//        ///// ✅ Helper Method: Set Scanner Property
//        //private void SetScannerProperty(IProperties properties, int propertyID, int value)
//        //{
//        //    try
//        //    {
//        //        foreach (Property property in properties)
//        //        {
//        //            Console.WriteLine($"Property ID: {property.PropertyID}, Name: {property.Name}");
//        //            if (property.PropertyID == propertyID)
//        //            {
//        //                property.set_Value(value);
//        //                return;
//        //            }
//        //        }
//        //        Console.WriteLine($"Warning: Property {propertyID} not found on this scanner.");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine($"Failed to set property {propertyID}: {ex.Message}");
//        //    }
//        //}

//    }
//}