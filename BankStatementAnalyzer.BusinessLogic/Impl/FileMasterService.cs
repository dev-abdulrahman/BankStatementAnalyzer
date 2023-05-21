using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using File = BankStatementAnalyzer.Models.File;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class FileMasterService : IFileMasterService
    {
        private readonly Lazy<IFileMasterRepository> fileMasterRepository;

        public FileMasterService(Lazy<IFileMasterRepository> fileMasterRepository)
        {
            this.fileMasterRepository = fileMasterRepository;
        }

        public void Add(File entity)
        {
            fileMasterRepository.Value.Add(entity);
        }

        public void Delete(File entity)
        {
            fileMasterRepository.Value.Delete(entity);
        }

        public void Edit(File entity)
        {
            fileMasterRepository.Value.Edit(entity);
        }

        public IQueryable<File> FindBy(Expression<Func<File, bool>> predicate)
        {
            return fileMasterRepository.Value.FindBy(predicate);
        }

        public IQueryable<File> GetAll()
        {
            return fileMasterRepository.Value.GetAll();
        }

        public void Save()
        {
            fileMasterRepository.Value.Save();
        }

        //public string Upload(HttpPostedFileBase file, string filePath, string fileName)
        //{
        //    bool exists = Directory.Exists(filePath);

        //    if (!exists)
        //        Directory.CreateDirectory(filePath);
        //    if (file.ContentLength > 0)
        //    {
        //        var path = Path.Combine(filePath, fileName);
        //        file.SaveAs(path);
        //        return path;
        //    }
        //    return null;

        //}

        public string Upload(HttpPostedFileBase file, string filePath, string fileName)
        {
            bool exists = Directory.Exists(filePath);

            if (!exists)
                Directory.CreateDirectory(filePath);
            if (file.ContentLength > 0)
            {
                var path = Path.Combine(filePath, fileName);

                Stream strm = file.InputStream;
               // GenerateThumbnails(0.5, strm, path);
                file.SaveAs(path);
                return path;
            }
            return null;
        }

        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = Image.FromStream(sourcePath))
            {
                // can given width of image as we want  
                var newWidth = (int)(image.Width * scaleFactor);
                // can given height of image as we want  
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }

        public string SaveFile(string inputFileName, string path, Stream stream)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(inputFileName));
            var fullPath = Path.Combine(path, fileName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
            return fileName;
        }

        public byte[] ConvertToZip(byte[] stream, string filename)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Update, false))
                {
                    var zipEntry = zipArchive.CreateEntry(filename);

                    //Get the stream of the attachment
                    using (var originalFileStream = new MemoryStream(stream))
                    {
                        using (var zipEntryStream = zipEntry.Open())
                        {
                            //Copy the attachment stream to the zip entry stream
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }

                return compressedFileStream.ToArray();
            }

        }
        public bool DeleteFile(string fullPath)
        {
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }
    }
}