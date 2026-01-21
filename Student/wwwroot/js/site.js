// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const password = document.querySelectorAll(".pass-input");
const togglePassword = document.querySelectorAll(".show-pass");

togglePassword.forEach((button, index) => {
    button.addEventListener("click", () => {
        const type = password[index].getAttribute("type") === "password" ? "text" : "password";
        password[index].setAttribute("type", type);
    });
});