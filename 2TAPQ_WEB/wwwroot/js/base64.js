
document.querySelector("#FilePond").addEventListener("change", function () {
    const reader = new FileReader();

    reader.addEventListener("load", () => {

        document.getElementById("imgp").src = reader.result;
        document.getElementById("ImagePond").value = reader.result;
        console.log(reader.result);
    });

    reader.readAsDataURL(this.files[0]);
});

