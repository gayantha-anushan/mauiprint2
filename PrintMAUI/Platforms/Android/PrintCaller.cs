using Android.App;
using Android.Graphics;
using Android.Print;
using Android.PrintServices;
using Android.Runtime;
using AndroidX.Print;
using PrintMAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintMAUI.Platforms
{
    public class PrintCaller : MauiAppCompatActivity, IPrintCalller
    {
        public async Task<bool> printImage(byte[] bitmapBytes)
        {
            try
            {
                //Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.Context
               
                PrintHelper photoPrinter = new PrintHelper(Platform.CurrentActivity);
                photoPrinter.ScaleMode = PrintHelper.ScaleModeFill;

                Bitmap bitmap = await BitmapFactory.DecodeByteArrayAsync(bitmapBytes, 0, bitmapBytes.Length);

                photoPrinter.PrintBitmap("Testing Print for BL", bitmap);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> printPDF(byte[] pdfBytes)
        {
            try
            {
                
                PrintManager printManager = Platform.CurrentActivity.GetSystemService(Android.Content.Context.PrintService).JavaCast<PrintManager>();
                string jobName = "simple Print Job";
                printManager.Print(jobName, new CustomPrintDocumentAdapter(pdfBytes), null);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
