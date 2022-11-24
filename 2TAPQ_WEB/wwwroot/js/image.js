$('input[type=file]#fileUpload').change(function () {

    const reader = new FileReader();

    reader.addEventListener("load", () => {

        document.getElementById("img").src = reader.result;
        document.getElementById("Imagef").value = reader.result;
        console.log(reader.result);
    });

    reader.readAsDataURL(this.files[0]);
});

$('input[type=file]#imagepond').change(function () {
    const reader = new FileReader();

    reader.addEventListener("load", () => {

        document.getElementById("imagepond1").src = reader.result;
        document.getElementById("Imagep").value = reader.result;
        console.log(reader.result);
    });

    reader.readAsDataURL(this.files[0]);

});

$('input[type=file]#FilePond').change(function () {

    const reader = new FileReader();

    reader.addEventListener("load", () => {

        document.getElementById("imgp").src = reader.result;
        document.getElementById("ImagePond").value = reader.result;
        console.log(reader.result);
    });

    reader.readAsDataURL(this.files[0]);
});