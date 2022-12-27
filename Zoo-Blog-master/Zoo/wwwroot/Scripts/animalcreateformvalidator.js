//Validate creation form file

document.addEventListener("DOMContentLoaded", () => {
  let maxFileSize = 10 * 1024 * 1024; // 10MB
  let fileInput = document.querySelector("input#ImageFile");
  const fileTypes = [
    "image/apng",
    "image/bmp",
    "image/gif",
    "image/jpeg",
    "image/pjpeg",
    "image/png",
    "image/svg+xml",
    "image/tiff",
    "image/webp",
    "image/x-icon",
  ];

  function validFileType(file) {
    return fileTypes.includes(file.type);
  }

  fileInput.addEventListener("change", function () {
    let fileSize = this.files[0].size;
    if (this.files[0] === undefined || fileSize === undefined) {
      this.setCustomValidity("Please enter file");
      this.reportValidity();
      return;
    }
    if (fileSize > maxFileSize) {
      this.setCustomValidity("This file is greater than 10mb");
      this.value = "";
      this.reportValidity();
      return;
    }
    if (!validFileType(this.files[0])) {
      this.setCustomValidity("This is not image file");
      this.value = "";
      this.reportValidity();
      return;
    }
    if (fileSize < maxFileSize) {
      this.setCustomValidity("");
      this.reportValidity();
    }
  });
});
