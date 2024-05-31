function readURL(input, imageBoxId, previewId) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#' + imageBoxId).attr('src', e.target.result);
            $('#' + previewId).attr('class', 'dz-preview dz-processing dz-image-preview dz-complete dz-success ');
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function clearFileInput(id, imageId, previewId) {
    // Assuming there's an <input type="file" id="fileInput"> in your HTML
    document.getElementById(id).value = "";
    $('#'+imageId).removeAttr('src');
    $('#'+previewId).attr('class', 'dz-preview dz-processing dz-image-preview dz-complete');
}

$("#image").change(function () {
    readURL(this, 'imageBox', 'preview');
});

$("#newsSecondImage").change(function () {
    readURL(this, 'secondImageBox', 'secondPreview');
});

$("#newsThirdImage").change(function () {
    readURL(this, 'thirdImageBox', 'thirdPreview');
});