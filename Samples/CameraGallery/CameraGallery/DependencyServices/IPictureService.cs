using System;
namespace CameraGallery
{
    public interface IPictureService
    {
        
        void CapturePicture();

        void SelectImage();

        void CropImage();

        event EventHandler<IPictureActionArgs> PictureActionCompleted;

    }

    public interface IPictureActionArgs
    {
        string LocalPictureURL { get; set; }
    }
}
