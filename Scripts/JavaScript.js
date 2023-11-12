function ChangeImage(UpLoadImage, previewImg) {
    if (UpLoadImage.files && UpLoadImage.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById(previewImg).setAttribute('src', e.target.result);
        }
        reader.readAsDataURL(UpLoadImage.file[0]);
    }
}