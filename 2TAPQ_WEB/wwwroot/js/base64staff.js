document.querySelector("#FileAvata").addEventListener("change", function () {
    const reader = new FileReader();

    reader.addEventListener("load", () => {

        document.getElementById("imgA").src = reader.result;
        document.getElementById("ImageA").value = reader.result;
        console.log(reader.result);
    });

    reader.readAsDataURL(this.files[0]);
});