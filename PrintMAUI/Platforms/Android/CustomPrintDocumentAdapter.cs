using Android.Graphics.Pdf;
using Android.OS;
using Android.Print;
using Android.Print.Pdf;
using Android.Runtime;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Print.PrintAttributes;

namespace PrintMAUI.Platforms
{
    public class CustomPrintDocumentAdapter : PrintDocumentAdapter
    {
        private byte[] data;
        public CustomPrintDocumentAdapter(byte[] data)
        {
            this.data = data;
        }
        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();
                return;
            }

            int pages = 1;

            if(pages > 0)
            {
                PrintDocumentInfo pdi = new PrintDocumentInfo.Builder("PDF.pdf")
                    .SetContentType(Android.Print.PrintContentType.Document)
                    .SetPageCount(pages)
                    .Build();
                callback.OnLayoutFinished(pdi, true);
            }
            else
            {
                callback.OnLayoutFailed("Calculations Failed!");
            }
        }
        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            // Write PDF document to file
            try
            {
                FileOutputStream stream = new FileOutputStream(destination.FileDescriptor);
                stream.Write(data);
            }
            catch (Exception e)
            {
                callback.OnWriteFailed(e.ToString());
                return;
            }
            callback.OnWriteFinished(pages);

        }
    }
}
