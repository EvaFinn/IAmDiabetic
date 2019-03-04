﻿using System;
using System.IO;
using System.Threading.Tasks;
namespace ImDiabetic.Utils
{
    public interface ISave
    {
        //Method to save document as a file and view the saved document
        Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}
