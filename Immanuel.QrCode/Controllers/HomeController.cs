using Immanuel.QrCode.Models;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Svg;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Immanuel.QrCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("qrcode")]
        public ContentResult qrcode([FromForm] string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCoder.SvgQRCode qrCode = new SvgQRCode(qrCodeData);
            string qrCodeAsSvg = qrCode.GetGraphic(20);
            return Content(qrCodeAsSvg, "image/svg+xml");
        }
        [Route("qrcode")]
        public ActionResult qrcode()
        {
            //string content = "Although once a fruitful land, the Tigris–Euphrates river system is now drying up at a startling rate. A government report by the Iraqi Ministry of Water Resources in 2021 warned that the rivers could run dry. (https://bible-study.immanuel.co/rev_16_12/)";
            string content = "Orion’s constellation is found in the North-Eastern side of the sky, next to which you will find Aldebaran or Alpha Tauri which is in the bigger constellation Taurus. When you move further in the north- eastern side you will find Pleiades.The Pleiades is a open cluster of stars, which means they are 100s of stars but are in the same cosmic cloud and have roughly the same chemical composition and are bound together by a force which we call mutual gravitational attraction.To our eyes they are always in one place, but in reality they are steadily marching together as a group. Loosening Orion’s Belt, Orion is made up of 2 stars (Alnilam and Mintaka) and 1 star cluster-Alnitak. These aren’t bound with a gravitational force like Pleiades, instead they are all heading in different directions.\r\nThey only seem to be in shape because of the great distance between them. So according to predictions based on the past, Alnilam and Mintaka will move closer while Alnitak will move Eastward and the “Belt” will no longer exist. (Just as God told Job!). (https://bible-study.immanuel.co/job_38_31/)";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCoder.SvgQRCode qrCode = new SvgQRCode(qrCodeData);
            string qrCodeAsSvg = qrCode.GetGraphic(new Size(300, 300));
            return Content(qrCodeAsSvg, "image/svg+xml");
            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            ////QRCode qrCode = new QRCode(qrCodeData);
            //QRCoder.PngByteQRCode qrCode = new QRCoder.PngByteQRCode(qrCodeData);
            //var bts = qrCode.GetGraphic(20);
            //Bitmap bitmap = null;
            //using (MemoryStream ms = new MemoryStream(bts))
            //{
            //    bitmap = new Bitmap(ms);
            //}
            //SvgDocument svgDoc = ConvertBitmapToSvg(bitmap);

            //return svgDoc.GetXML();

            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("Although once a fruitful land, the Tigris–Euphrates river system is now drying up at a startling rate. A government report by the Iraqi Ministry of Water Resources in 2021 warned that the rivers could run dry. (https://bible-study.immanuel.co/rev_16_12/)", QRCodeGenerator.ECCLevel.Q);
            //AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
            //string qrCodeAsAsciiArt = qrCode.GetGraphic(1);
            //return qrCodeAsAsciiArt;
        }
        static SvgDocument ConvertBitmapToSvg(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            SvgDocument svg = new SvgDocument
            {
                Width = width,
                Height = height
            };

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    if (pixelColor.A != 0) // If pixel is not transparent
                    {
                        SvgRectangle rect = new SvgRectangle
                        {
                            X = x,
                            Y = y,
                            Width = 1,
                            Height = 1,
                            Fill = new SvgColourServer(pixelColor)
                        };
                        svg.Children.Add(rect);
                    }
                }
            }

            return svg;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}