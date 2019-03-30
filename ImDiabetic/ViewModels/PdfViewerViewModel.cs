using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace ImDiabetic.ViewModels
{
    class PdfViewerViewModel : INotifyPropertyChanged
    {
        private Stream m_pdfDocumentStream;
        public event PropertyChangedEventHandler PropertyChanged;

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

        public PdfViewerViewModel(String topic)
        {
            m_pdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("ImDiabetic.EduContent." + topic + ".pdf");
        }

        public PdfViewerViewModel(Stream stream)
        {
            m_pdfDocumentStream = stream;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}