// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("click", function (e) {

    for (let i = 0; i < 12; i++) {
        const particle = document.createElement("div");
        particle.className = "firework";

        const x = (Math.random() - 0.5) * 200 + "px";
        const y = (Math.random() - 0.5) * 200 + "px";

        particle.style.left = e.clientX + "px";
        particle.style.top = e.clientY + "px";
        particle.style.setProperty("--x", x);
        particle.style.setProperty("--y", y);

        document.body.appendChild(particle);

        setTimeout(() => particle.remove(), 800);
    }
});