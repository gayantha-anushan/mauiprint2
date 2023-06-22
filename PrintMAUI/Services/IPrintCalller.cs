using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintMAUI.Services
{
    public interface IPrintCalller
    {
        Task<bool> printImage(byte[] bitmapBytes);
        Task<bool> printPDF(byte[] pdfBytes);
    }
}
