function createPreviewTemplate(inputName,divName) {

    const uniqueId = generateUniqueId();
    const previewId = "p" + uniqueId ;
    const imageBoxId = "im" + uniqueId;
    const inputId = "i" + uniqueId;   

    //var gallery = document.getElementById('gallery');

    //var template = document.createElement('div');
    //template.className = "dz-preview dz-processing dz-image-preview dz-complete";
    //template.id = previewId;
    //var details = document.createElement('div');
    //details.className = "dz-details";

    //var thumbnail = document.createElement('div');
    //thumbnail.className = "dz-thumbnail";

    //var img = document.createElement('img');
    //img.id = imageBoxId;

    //var noPreview = document.createElement('span');
    //noPreview.className = "dz-nopreview";
    //noPreview.textContent = "No preview";

    //var successMark = document.createElement('div');
    //successMark.className = "dz-success-mark";

    //var errorMark = document.createElement('div');
    //errorMark.className = "dz-error-mark";

    //var errorMessage = document.createElement('div');
    //errorMessage.className = "dz-error-message";

    //var span = document.createElement('span');
    //span.setAttribute('data-dz-errormessage', '');

    //errorMessage.appendChild(span);

    //var progress = document.createElement('div');
    //progress.className = "progress";

    //var progressBar = document.createElement('div');
    //progressBar.className = "progress-bar progress-bar-primary";
    //progressBar.setAttribute('role', 'progressbar');
    //progressBar.setAttribute('aria-valuemin', '0');
    //progressBar.setAttribute('aria-valuemax', '100');
    //progressBar.setAttribute('data-dz-uploadprogress', '');
    //progressBar.style.width = "100%";

    //progress.appendChild(progressBar);

    //thumbnail.appendChild(img);
    //thumbnail.appendChild(noPreview);
    //thumbnail.appendChild(successMark);
    //thumbnail.appendChild(errorMark);
    //thumbnail.appendChild(errorMessage);
    //thumbnail.appendChild(progress);

    //details.appendChild(thumbnail);

    //var removeLink = document.createElement('a');
    //removeLink.className = "dz-remove";
    //removeLink.setAttribute('onclick', "clearFileInput('newsSecondImage','secondImageBox','secondPreview')");
    //removeLink.setAttribute('data-dz-remove', '');
    //removeLink.textContent = "Remove file";

    //template.appendChild(details);
    //template.appendChild(removeLink);

    //gallery.appendChild(template);


    var inputs = document.getElementById(divName);

    var div = document.createElement('div');
    div.className = "input-group";

    var input = document.createElement('input');   
    input.type = "file";    
    input.className = "form-control";
    input.name = inputName;
    input.id = inputId;
    


    var label = document.createElement('label');
    label.className = "input-group-text";
    label.htmlFor = "mainImage";
    label.textContent = "Upload";

    div.appendChild(input);
    div.appendChild(label);

    inputs.appendChild(div);      
}



// Function to generate a unique ID
function generateUniqueId() {
    return Math.random().toString(36).substr(2, 2);
}

