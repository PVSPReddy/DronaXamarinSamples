using System;
namespace CameraGallery
{
    public interface IPictureService
    {
        
        void CapturePicture();

        void SelectImage();

        event EventHandler<IPictureActionArgs> PictureActionCompleted;

    }

    public interface IPictureActionArgs
    {
        string LocalPictureURL { get; set; }
    }
}
