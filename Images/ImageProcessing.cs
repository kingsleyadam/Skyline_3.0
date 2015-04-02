using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Images
{
    public class ImageProcessing
    {
        private string _imgName;
        private string _origPath;
        private string _compressedPath;
        private string _thumbPath;
        private string _fileExtension;

        public ImageProcessing(string mImgName)
        {
            ImageName = System.IO.Path.GetFileNameWithoutExtension(mImgName);
            OriginalPath = HttpContext.Current.Server.MapPath("~/images/product_images/orig/");
            CompressedPath = HttpContext.Current.Server.MapPath("~/images/product_images/compressed/");
            ThumbnailPath = HttpContext.Current.Server.MapPath("~/images/product_images/thumb/");
            FileExtension = System.IO.Path.GetExtension(mImgName);
        }

        public ImageProcessing(string mImgName, string mOrigPath)
        {
            ImageName = System.IO.Path.GetFileNameWithoutExtension(mImgName);
            OriginalPath = HttpContext.Current.Server.MapPath(mOrigPath);
            CompressedPath = HttpContext.Current.Server.MapPath("~/images/product_images/compressed/");
            ThumbnailPath = HttpContext.Current.Server.MapPath("~/images/product_images/thumb/");
            FileExtension = System.IO.Path.GetExtension(mImgName);
        }

        public ImageProcessing(string mImgName, string mOrigPath, string mCompressedPath, string mThumbPath)
        {
            ImageName = System.IO.Path.GetFileNameWithoutExtension(mImgName);
            OriginalPath = HttpContext.Current.Server.MapPath(mOrigPath);
            CompressedPath = HttpContext.Current.Server.MapPath(mCompressedPath);
            ThumbnailPath = HttpContext.Current.Server.MapPath(mThumbPath);
            FileExtension = System.IO.Path.GetExtension(mImgName);
        }

        public string ImageName
        {
            get { return _imgName; }
            set { _imgName = value; }
        }

        public string OriginalPath
        {
            get { return _origPath; }
            set { _origPath = value; }
        }

        public string CompressedPath
        {
            get { return _compressedPath; }
            set { _compressedPath = value; }
        }

        public string ThumbnailPath
        {
            get { return _thumbPath; }
            set { _thumbPath = value; }
        }

        public string FileExtension
        {
            get { return _fileExtension; }
            set { _fileExtension = value; }
        }

        public void ProcessImage(out string statusMessage)
        {
            statusMessage = "";
            string fullFilePath = OriginalPath + ImageName + FileExtension;

            try
            {
                Image img = new Bitmap(fullFilePath);
                Bitmap bmp = new Bitmap(fullFilePath);

                //Fix Orientation
                FixOrientation(img, fullFilePath);

                //Adjust Quality and Save
                AdjustQualityLevel(bmp);

                //Dispose these so we can re-use the images later
                img.Dispose();
                bmp.Dispose();
            }
            catch (Exception ex)
            {
                statusMessage = ThumbnailPath + ImageName + FileExtension.ToLower() + "<br />" + ex.Message + ex.StackTrace;
            }

        }

        private void AdjustQualityLevel(Bitmap bmp)
        {
            EncoderParameters myEncoderParameters;
            EncoderParameter myEncoderParameter;

            //Create The bitmap
            ImageCodecInfo jpgEncoder = GetImageEncoder(ImageFormat.Jpeg);

            //Create an Encoder object based on the guid for quality parameter category
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            //Create an EncoderParameters object. It has an array of EncoderParameters object.
            myEncoderParameters = new EncoderParameters(1);

            if (!System.IO.File.Exists(CompressedPath + ImageName + FileExtension.ToLower()))
            {
                myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp.Save(CompressedPath + ImageName + FileExtension.ToLower(), jpgEncoder, myEncoderParameters);
            }


            if (!System.IO.File.Exists(ThumbnailPath + ImageName + FileExtension.ToLower()))
            {
                myEncoderParameter = new EncoderParameter(myEncoder, 25L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp.Save(ThumbnailPath + ImageName + FileExtension.ToLower(), jpgEncoder, myEncoderParameters);
            }

            bmp.Dispose();
        }

        private ImageCodecInfo GetImageEncoder(ImageFormat format)
        {
            ImageCodecInfo[] allCodecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in allCodecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void FixOrientation(Image originalImage, string filePath)
        {
            if (originalImage.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = originalImage.GetPropertyItem(0x0112).Value[0];
                switch (rotationValue)
                {
                    case 1: // landscape, do nothing
                        break;
                    case 8: // rotated 90 right
                        // de-rotate:
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                        originalImage.Save(filePath);
                        break;

                    case 3: // bottoms up
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                        originalImage.Save(filePath);
                        break;

                    case 6: // rotated 90 left
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                        originalImage.Save(filePath);
                        break;
                }
            }
            originalImage.Dispose();
        }
    }
}
