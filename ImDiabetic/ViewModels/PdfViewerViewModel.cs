﻿using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace ImDiabetic.ViewModels
{
    class PdfViewerViewModel : INotifyPropertyChanged
    {
        private Stream m_pdfDocumentStream;

        /// <summary>
        /// An event to detect the change in the value of a property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The PDF document stream that is loaded into the instance of the PDF viewer. 
        /// </summary>
        public Stream PdfDocumentStream
        {
            get
            {
                return m_pdfDocumentStream;
            }
            set
            {
                m_pdfDocumentStream = value;
                NotifyPropertyChanged("PdfDocumentStream");
            }
        }

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public PdfViewerViewModel(String topic)
        {
            //Accessing the PDF document that is added as embedded resource as stream.
            m_pdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("ImDiabetic.EduContent." + topic + ".pdf");
        }

        public PdfViewerViewModel(Stream stream)
        {
            //Accessing the PDF document that is added as embedded resource as stream.
            m_pdfDocumentStream = stream;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}