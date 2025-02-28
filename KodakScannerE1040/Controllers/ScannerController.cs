using Microsoft.AspNetCore.Mvc;
using System.Management;
using TwainDotNet;
using WIA;
using CommonDialog = WIA.CommonDialog;


namespace KodakScannerE1040.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScannerController : ControllerBase
    {
        //private bool _isSessionInitialized = false;
        //private readonly TwainSession _session;
        private readonly IConfiguration _configuration;
        private Twain _twain;
        private readonly string query;

        private readonly string FormatType;
        private readonly string FileName;
        private readonly string OutPutFormat;
        private readonly int Pixel;
        private readonly int Dpi;
        private readonly int ColorMode;

        public ScannerController(IConfiguration configuration)
        {
            _configuration = configuration;
            query = _configuration.GetValue<string>("QueryToGetScannerStatus");

            FormatType = _configuration.GetValue<string>("FormatType");
            OutPutFormat = _configuration.GetValue<string>("OutPutFormat");
            Pixel = _configuration.GetValue<int>("Pixel");
            Dpi = _configuration.GetValue<int>("Dpi");
            ColorMode = _configuration.GetValue<int>("ColorMode");
            FileName = _configuration.GetValue<string>("FileName");
        }


        [HttpPost("status")]
        public IActionResult Status()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject obj in searcher.Get())
                {
                    return Ok(new { message = $"Status: {obj["Status"]} ; DeviceID: {obj["DeviceID"]} ; Manufacturer: {obj["Manufacturer"]} ; Device: {obj["Description"]}" });
                }
                return BadRequest("Scanner is not connected");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during getting information of device.", error = ex.Message });
            }
        }


        [HttpGet("tiff_scan")]
        public IActionResult tiff_Scan()
        {
            try
            {
                // ✅ Step 1: Get the scanner device
                DeviceManager deviceManager = new DeviceManager();
                DeviceInfo scannerDevice = null;

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
                {
                    if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                    {
                        scannerDevice = deviceManager.DeviceInfos[i];
                        break;
                    }
                }
                if (scannerDevice == null)
                {
                    return BadRequest("No scanner found!");
                }

                // ✅ Step 2: Connect to the scanner
                Device scanner = scannerDevice.Connect();

                foreach (Property item in scanner.Properties)
                {
                    if (item.Name == "Document Handling Status")
                    {
                        int i = 0;
                    }
                    Console.WriteLine($"Prop name {item.Name} : {item.get_Value}");
                }

                // ✅ Step 3: Start scanning
                Item scanItem = scanner.Items[1]; // The first item is usually the scanner
                SetScannerProperty(scanItem.Properties, 6146, ColorMode); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
                SetScannerProperty(scanItem.Properties, 6147, Dpi); // DPI (Resolution)
                SetScannerProperty(scanItem.Properties, 6148, Pixel); // Bits per pixel

                CommonDialog dialog = new CommonDialog();
                ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, FormatType, false);

                byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();
                //return File(imageBytes, "image/jpeg", "ScannedImage.jpg");
                return File(imageBytes, OutPutFormat, FileName);
            }
            catch (Exception ex)
            {
                if (ex.Message == "0x80210003")
                {
                    return StatusCode(500, new { message = "Page not found in scanner." });
                }
                else
                {
                    return StatusCode(500, new { message = "An error occurred during scanning", error = ex.Message });
                }
            }
        }

        [HttpGet("base64_scan")]
        public IActionResult Base64Scan()
        {
            var response = new
            {
                stage = "",
                status = "",
                message = "",
                scannedContentBase64 = ""
            };

            try
            {
                // ✅ Step 1: Get the scanner device
                response = new { stage = "GET_SCANNER", status = "", message = "", scannedContentBase64 = "" };

                DeviceManager deviceManager = new DeviceManager();
                DeviceInfo scannerDevice = null;

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
                {
                    if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                    {
                        scannerDevice = deviceManager.DeviceInfos[i];
                        break;
                    }
                }
                if (scannerDevice == null)
                {
                    return BadRequest(new { stage = "GET_SCANNER", status = "ERROR", message = "No scanner found!", scannedContentBase64 = "" });
                }

                // ✅ Step 2: Connect to the scanner
                response = new { stage = "CONNECT_TO_SCANNER", status = "", message = "", scannedContentBase64 = "" };

                Device scanner = scannerDevice.Connect();

                // ✅ Step 3: Start scanning
                response = new { stage = "GET_SCANNED_IMAGE", status = "", message = "", scannedContentBase64 = "" };

                Item scanItem = scanner.Items[1]; // The first item is usually the scanner

                SetScannerProperty(scanItem.Properties, 6146, ColorMode); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
                SetScannerProperty(scanItem.Properties, 6147, Dpi); // DPI (Resolution)
                SetScannerProperty(scanItem.Properties, 6148, Pixel); // Bits per pixel

                CommonDialog dialog = new CommonDialog();
                ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, FormatType, false);

                byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();

                // ✅ Convert byte array to Base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                return Ok(new
                {
                    stage = "GET_SCANNED_IMAGE",
                    status = "SUCCESS",
                    message = "Scanning completed successfully.",
                    scannedContentBase64 = base64String
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    stage = response.stage,
                    status = "ERROR",
                    message = ex.Message,
                    scannedContentBase64 = ""
                });
            }
        }

        //Binary_scan
        [HttpGet("binary_scan")]
        public IActionResult BinaryScan()
        {
            var response = new
            {
                stage = "",
                status = "",
                message = "",
                scannedContentBinary = new byte[0] // Placeholder for binary data
            };

            try
            {
                // ✅ Step 1: Get the scanner device
                response = new { stage = "GET_SCANNER", status = "", message = "", scannedContentBinary = new byte[0] };

                DeviceManager deviceManager = new DeviceManager();
                DeviceInfo scannerDevice = null;

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
                {
                    if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                    {
                        scannerDevice = deviceManager.DeviceInfos[i];
                        break;
                    }
                }
                if (scannerDevice == null)
                {
                    return BadRequest(new { stage = "GET_SCANNER", status = "ERROR", message = "No scanner found!", scannedContentBinary = new byte[0] });
                }

                // ✅ Step 2: Connect to the scanner
                response = new { stage = "CONNECT_TO_SCANNER", status = "", message = "", scannedContentBinary = new byte[0] };

                Device scanner = scannerDevice.Connect();

                // ✅ Step 3: Start scanning
                response = new { stage = "GET_SCANNED_IMAGE", status = "", message = "", scannedContentBinary = new byte[0] };

                Item scanItem = scanner.Items[1]; // The first item is usually the scanner

                SetScannerProperty(scanItem.Properties, 6146, ColorMode); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
                SetScannerProperty(scanItem.Properties, 6147, Dpi); // DPI (Resolution)
                SetScannerProperty(scanItem.Properties, 6148, Pixel); // Bits per pixel

                CommonDialog dialog = new CommonDialog();
                ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, FormatType, false);

                byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();

                return Ok(new
                {
                    stage = "GET_SCANNED_IMAGE",
                    status = "SUCCESS",
                    message = "Scanning completed successfully.",
                    scannedContentBinary = imageBytes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    stage = response.stage,
                    status = "ERROR",
                    message = ex.Message,
                    scannedContentBinary = new byte[0]
                });
            }
        }


        /// ✅ Helper Method: Set Scanner to Color Mode
        private void SetScannerProperties(Item scanItem)
        {
            const int WIA_IPS_CUR_INTENT = 6146; // WIA property for intent (color mode)
            const int WIA_IPS_DATA_TYPE = 4103;  // WIA property for data type (color)

            try
            {
                // ✅ Set color mode
                SetWIAProperty(scanItem.Properties, WIA_IPS_CUR_INTENT, 1); // 1 = Color

                // ✅ Ensure the scanner is set to color
                SetWIAProperty(scanItem.Properties, WIA_IPS_DATA_TYPE, 2); // 2 = RGB Color
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting scanner properties: {ex.Message}");
            }
        }

        /// ✅ Helper Method: Set WIA Property
        /// ✅ Helper Method: Set WIA Property Safely
        private void SetWIAProperty(IProperties properties, int propertyID, int value)
        {
            try
            {
                foreach (Property property in properties)
                {
                    if (property.PropertyID == propertyID)
                    {
                        property.set_Value(value);
                        return; // Exit after setting the property
                    }
                }
                Console.WriteLine($"Warning: Property {propertyID} not found on this scanner.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set property {propertyID}: {ex.Message}");
            }
        }


        private void SetScannerProperty(IProperties properties, int propertyID, int value)
        {
            try
            {
                foreach (Property property in properties)
                {
                    Console.WriteLine($"Property ID: {property.PropertyID}, Name: {property.Name}");
                    if (property.PropertyID == propertyID)
                    {
                        property.set_Value(value);
                        return;
                    }
                }
                Console.WriteLine($"Warning: Property {propertyID} not found on this scanner.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set property {propertyID}: {ex.Message}");
            }
        }


        //reference
        //🔹 Option 2: Add COM Reference Manually
        //Right-click on your project in Solution Explorer.
        //Select "Add Reference".
        //Go to "COM".
        //Search for "Microsoft Windows Image Acquisition Library v2.0".
        //Check the box and click OK.


        //✅ Common WIA Formats & Their GUIDs
        //Format GUID(Globally Unique Identifier)
        //BMP(Bitmap) {B96B3CAB-0728-11D3-9D7B-0000F81EF32E}
        //PNG(Portable Network Graphics) {B96B3CAF-0728-11D3-9D7B-0000F81EF32E}
        //JPEG(Joint Photographic Experts Group) {B96B3CAE-0728-11D3-9D7B-0000F81EF32E}
        //TIFF(Tagged Image File Format) {B96B3CB1-0728-11D3-9D7B-0000F81EF32E}
        //GIF(Graphics Interchange Format) {B96B3CB0-0728-11D3-9D7B-0000F81EF32E}


        /* 📌 Explanation of Each Scanner Property
        Property ID	Property Name	Description & Values
        6146	WIA_IPS_CUR_INTENT	Color Mode → 1 = B/W, 2 = Grayscale, 3 = Color
        6147	WIA_IPS_XRES & WIA_IPS_YRES	Resolution (DPI) → Set both X & Y to 200 or 300 for high quality
        6148	WIA_IPS_BITDEPTH	Bits per pixel → 1 for B/W, 8 for Grayscale, 24 for Color
        3088	WIA_IPS_ORIENTATION	Page Orientation → 0 = Portrait, 1 = Landscape
        4103	WIA_IPS_DATATYPE	Data Type → 0 = BW, 1 = Grayscale, 2 = RGB (Color)
        6151	WIA_IPS_DOCUMENT_HANDLING_SELECT	Enable Feeder & Duplex → 1 = Flatbed, 2 = Feeder, 4 = Duplex
        6154	WIA_IPS_PAGES	Number of pages to scan
        3096	WIA_IPS_TYMED	Image Format Type → 2 = File, 4 = Memory
        */


        //[HttpPost("status")]
        //public IActionResult Status()
        //{
        //    try
        //    {
        //        string query = "SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%Fax%' OR Description LIKE '%Scanner%'";
        //        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
        //        ManagementObjectCollection results = searcher.Get();

        //        // Debug: Check how many devices are found
        //        int count = results.Count;
        //        Console.WriteLine($"🔍 Total Devices Found: {count}");

        //        if (count == 0)
        //        {
        //            return NotFound(new { message = "No scanner or fax device found." });
        //        }

        //        var deviceList = new List<object>();

        //        foreach (ManagementObject obj in results)
        //        {
        //            Console.WriteLine("\n🔹 **Device Found:**");
        //            Console.WriteLine("-----------------------------------------------------");

        //            var properties = new Dictionary<string, object>();

        //            foreach (PropertyData property in obj.Properties)
        //            {
        //                try
        //                {
        //                    properties[property.Name] = property.Value ?? "NULL";
        //                    Console.WriteLine($"📌 {property.Name} = {property.Value}");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"⚠️ Error reading {property.Name}: {ex.Message}");
        //                }
        //            }

        //            deviceList.Add(properties);
        //        }

        //        return Ok(new { message = "Scanner details retrieved.", devices = deviceList });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while getting device information.", error = ex.Message });
        //    }
        //}



    }
}
