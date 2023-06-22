using PrintMAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintMAUI.Platforms
{
    public class PrintCaller : IPrintCalller
    {
        public Task<bool> printImage(byte[] bitmapBytes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> printPDF(byte[] pdfBytes)
        {
            throw new NotImplementedException();
        }
    }
}
