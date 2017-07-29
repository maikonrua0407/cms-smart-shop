using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace SmartShop.Utilities
{
    public static class LFile
    {
        /// <summary>
        /// Lấy giá trị của property trong file xml theo tên của property
        /// </summary>
        /// <param name="fileXML">đường dẫn tới file xml</param>
        /// <param name="propertyName">tên property cần lấy giá trị</param>
        /// <returns>object chứa giá trị</returns>
        public static object GetPropertyInXml(string fileXmlPath, string propertyName)
        {
            XElement xml = XElement.Load(@fileXmlPath);
            XElement setup = (from p in xml.Descendants() select p).First();
            foreach (XElement xe in setup.Descendants())
            {
                if (xe.Name.ToString().Equals(propertyName))
                {
                    return xe.Value;
                }
            }
            return "";
        }

        /// <summary>
        /// Xóa file theo đường dẫn
        /// </summary>
        /// <param name="path">đường dẫn tới file cần xóa</param>
        public static void DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    //If its readonly set it back to normal
                    //Need to "AND" it as it can also be archive, hidden etc 
                    if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                    }
                    //Delete the file
                    File.Delete(path);
                }  
            }
            catch (System.Exception ex)
            {
                
            }
        }

        public static string GetFileType(string path)
        {
            if (File.Exists(path))
            {
                return Path.GetExtension(path);
            }
            return "";
        }

        /// <summary>
        /// Hàm chuyển dữ liệu file  sang dữ liệu binary
        /// </summary>
        /// <param name="filePath">File với đường dẫn tuyệt đối</param>
        /// <returns>Dữ liệu binary của file</returns>
        public static Stream GetStreamDataFromFile(string filePath)
        {
            Stream streamImage = null;
            try
            {
                streamImage = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return streamImage;
            }
            catch (Exception ex)
            {
                
            }
            return streamImage;
        }

        /// <summary>
        /// Hàm chuyển dữ liệu file sang dữ liệu byte[]
        /// </summary>
        /// <param name="filePath">File với đường dẫn tuyệt đối</param>
        /// <returns>Dữ liệu binary của file</returns>
        public static byte[] GetByteArrayFromFile(string filePath)
        {
            byte[] byteArray = null;
            try
            {
                MemoryStream ms = new MemoryStream();
                Stream streamImage = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                streamImage.CopyTo(ms);
                byteArray = ms.ToArray();
                return byteArray;
            }
            catch (Exception ex)
            {
                
            }
            return byteArray;
        }

        /// <summary>
        /// Chuyển từ Stream sang byte[]
        /// </summary>
        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            byte[] byteArray = null;
            try
            {
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                byteArray = ms.ToArray();
                return byteArray;
            }
            catch (Exception ex)
            {
                
            }
            return byteArray;
        }

        /// <summary>
        /// Chuyển từ byte[] sang Stream
        /// </summary>
        public static Stream ConvertByteArrayToStream(byte[] byteArray)
        {
            Stream stream = null;
            try
            {
                stream = new MemoryStream(byteArray);
                return stream;
            }
            catch (Exception ex)
            {
                
            }
            return stream;
        }

        /// <summary>
        /// Hàm chuyển dữ liệu binary sang một file
        /// </summary>
        /// <param name="streamImage">Dữ liệu binary</param>
        /// <param name="imagePath">Đường dẫn tuyệt đối lưu trữ file</param>
        /// <returns>True >> thành công, False >> Không thành công</returns>
        public static bool WriteFileFromStreamData(Stream streamFile, string filePath)
        {
            bool result = true;
            try
            {
                if (streamFile.Length == 0) return false;

                // Create a FileStream object to write a stream to a file
                using (FileStream fileStream = System.IO.File.Create(filePath, (int)streamFile.Length))
                {
                    // Fill the bytes[] array with the stream data
                    byte[] bytesInStream = new byte[streamFile.Length];
                    streamFile.Read(bytesInStream, 0, (int)bytesInStream.Length);

                    // Use FileStream object to write to the specified file
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                }

                return result;
            }
            catch (Exception ex)
            {
                result = false;
                
            }
            return result;
        }

        /// <summary>
        /// Hàm chuyển dữ liệu binary sang một file
        /// </summary>
        /// <param name="streamImage">Dữ liệu binary</param>
        /// <param name="imagePath">Đường dẫn tuyệt đối lưu trữ file</param>
        /// <returns>True >> thành công, False >> Không thành công</returns>
        public static bool WriteAbsFileFromStreamData(Stream streamFile, string filePath)
        {
            bool result = true;
            try
            {
                if (streamFile.Length == 0) return false;

                // Tạo các folder trong đường dẫn tuyệt đối của file
                if (true)
                {
                    // convert / to \\
                    string absFilePath = filePath.Replace("/", "\\");
                    List<string> listFileFolderItem = absFilePath.SplitByDelimiter("\\").ToList();
                    listFileFolderItem.RemoveAt(listFileFolderItem.Count - 1);
                    string path = "";
                    
                    foreach (string item in listFileFolderItem)
                    {
                        path += item + "\\";
                        if (!Directory.Exists(path))
                        {
                            //Directory.CreateDirectory(path);
                            try
                            {
                                DirectoryInfo dirInfo = Directory.CreateDirectory(path);
                            }
                            catch (System.Exception ex)
                            {                               
                                return false;
                            }
                        }
                    }
                }    

                // Create a FileStream object to write a stream to a file
                using (FileStream fileStream = System.IO.File.Create(filePath, (int)streamFile.Length))
                {
                    // Fill the bytes[] array with the stream data
                    byte[] bytesInStream = new byte[streamFile.Length];
                    streamFile.Read(bytesInStream, 0, (int)bytesInStream.Length);

                    // Use FileStream object to write to the specified file
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                }

                return result;
            }
            catch (Exception ex)
            {
                result = false;
                
            }
            return result;
        }

        /// <summary>
        /// Hàm chuyển dữ liệu byte[] sang một file
        /// </summary>
        /// <returns>True >> thành công, False >> Không thành công</returns>
        public static bool WriteFileFromByteArray(byte[] byteArray, string filePath)
        {
            bool result = true;
            try
            {
                if (byteArray.Length == 0) return false;

                Stream streamFile = ConvertByteArrayToStream(byteArray);

                result = WriteFileFromStreamData(streamFile, filePath);

                return result;
            }
            catch (Exception ex)
            {
                result = false;
                
            }
            return result;
        }

        /// <summary>
        /// Hàm chuyển dữ liệu byte[] sang một file
        /// </summary>
        /// <returns>True >> thành công, False >> Không thành công</returns>
        public static bool WriteAbsFileFromByteArray(byte[] byteArray, string filePath)
        {
            bool result = true;
            try
            {
                if (byteArray.Length == 0) return false;

                Stream streamFile = ConvertByteArrayToStream(byteArray);

                result = WriteAbsFileFromStreamData(streamFile, filePath);

                return result;
            }
            catch (Exception ex)
            {
                result = false;
                
            }
            return result;
        }

        /// <summary>
        /// WriteTextToFile
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <param name="bAppend"></param>
        /// <param name="sText"></param>
        /// <returns></returns>
        public static bool WriteTextToFile(string FilePath, bool bAppend, string Text)
        {
            StreamWriter swWriter;
            bool bRet = true;
            try
            {
                swWriter = new StreamWriter(FilePath, bAppend, System.Text.Encoding.Unicode);
                swWriter.Write(Text);
                swWriter.Close();
            }
            catch (Exception ex)
            {
                
                bRet = false;
            }
            return bRet;
        }

        ///
        /// Merges the Docs and renders the destinationFile
        ///
        public static void MergePdfDocs(List<string> listFileExport, string mergeFileExport)
        {

            //Step 1: Create a Docuement-Object
            Document document = new Document();
            try
            {
                //Step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(mergeFileExport, FileMode.Create));

                //Step 3: Open the document
                document.Open();

                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                int n = 0;
                int rotation = 0;

                //Loops for each file that has been listed
                foreach (string filename in listFileExport)
                {
                    //The current file path
                    string filePath = filename;

                    // we create a reader for the document
                    PdfReader reader = new PdfReader(filePath);

                    //Gets the number of pages to process
                    n = reader.NumberOfPages;

                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(1));
                        document.NewPage();

                        //Insert to Destination on the first page
                        if (i == 1)
                        {
                            Chunk fileRef = new Chunk(" ");
                            fileRef.SetLocalDestination(filename);
                            document.Add(fileRef);
                        }

                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally { document.Close(); GC.Collect(); }
        }

        public static void ExportToPdf(DataTable dt, string filePath)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            PdfPRow row = null;
            float[] widths = new float[] { 4f, 4f, 4f, 4f };

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            int iCol = 0;
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {

                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    table.AddCell(new Phrase(r[0].ToString(), font5));
                    table.AddCell(new Phrase(r[1].ToString(), font5));
                    table.AddCell(new Phrase(r[2].ToString(), font5));
                    table.AddCell(new Phrase(r[3].ToString(), font5));
                }
            } document.Add(table);
            document.Close();
        }

        /// <summary>
        /// truonglq xóa toàn bộ file và thư mục con của folder
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFolder(string path)
        {
            DirectoryInfo drInfo = new DirectoryInfo(path);
            DirectoryInfo[] folders = drInfo.GetDirectories(); // lay cac folder
            FileInfo[] files = drInfo.GetFiles(); //lay cac files

            // neu van con thu muc con thi phai xoa het cac thu muc con
            if (folders != null)
            {
                foreach (DirectoryInfo fol in folders)
                {
                    DeleteFolder(fol.FullName);  //xoa thu muc con va cac file trong thu muc con do
                }

            }

            //Neu van con file thi phai xoa het cac file
            if (files != null)
            {
                foreach (FileInfo f in files)
                {
                    File.Delete(f.FullName);
                }
            }
            //Cuoi cung xoa thu muc goc
            Directory.Delete(path);
        }
    }
}
