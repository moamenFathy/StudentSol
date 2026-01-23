// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Password Toggle Functionality
const passwordInputs = document.querySelectorAll(".pass-input");
const toggleButtons = document.querySelectorAll(".show-pass");

toggleButtons.forEach((button, index) => {
    button.addEventListener("click", () => {
        const input = passwordInputs[index];
        const icon = button.querySelector("i");
        
        // Toggle password visibility
        const type = input.getAttribute("type") === "password" ? "text" : "password";
        input.setAttribute("type", type);
        
        // Toggle eye icon
        if (icon) {
            if (type === "text") {
                icon.classList.remove("bi-eye");
                icon.classList.add("bi-eye-slash");
            } else {
                icon.classList.remove("bi-eye-slash");
                icon.classList.add("bi-eye");
            }
        }
    });
});