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
        //private byte[] data;
        private int totalPages = 0;
        PrintedPdfDocument pdfDocument;
        public CustomPrintDocumentAdapter(byte[] data)
        {
            //this.data = data;
        }
        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            pdfDocument = new PrintedPdfDocument(Platform.CurrentActivity,newAttributes);

            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();
                return;
            }

            int pages = computePageCount(newAttributes);

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
        private bool containsPage(PageRange[] pages,int num)
        {
            //TODO: you have to do it
            return true;
        }
        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            for (int i = 0; i < totalPages; i++)
            {
                // Check to see if this page is in the output range.
                if (containsPage(pages, i))
                {
                    // If so, add it to writtenPagesArray. writtenPagesArray.size()
                    // is used to compute the next output page index.
                    //writtenPagesArray.append(writtenPagesArray.size(), i);
                    PdfDocument.Page page = pdfDocument.StartPage(i);

                    // check for cancellation
                    if (cancellationSignal.IsCanceled)
                    {
                        callback.OnWriteCancelled();
                        pdfDocument.Close();
                        pdfDocument = null;
                        return;
                    }

                    // Rendering is complete, so page can be finalized.
                    pdfDocument.FinishPage(page);
                }
            }

            // Write PDF document to file
            try
            {
                //FileOutputStream stream = new FileOutputStream(destination.FileDescriptor);
                FileStream stream = new FileStream((string)destination.FileDescriptor, FileMode.Create, FileAccess.Write);
                pdfDocument.WriteTo(stream);
            }
            catch (Exception e)
            {
                callback.OnWriteFailed(e.ToString());
                return;
            }
            finally
            {
                pdfDocument.Close();
                pdfDocument = null;
            }
            //PageRange[] writtenPages = computeWrittenPages();
           /// Signal the print framework the document is complete
            callback.OnWriteFinished(pages);

        }

        private int computePageCount(PrintAttributes printAttributes)
        {
            int itemsPerPage = 4;

            MediaSize pageSize = printAttributes.GetMediaSize();
            if (!pageSize.IsPortrait)
            {
                itemsPerPage = 6;
            }

            int printItemCount = 1;

            return (int)Math.Ceiling((double)printItemCount/itemsPerPage);
        }
    }
}
