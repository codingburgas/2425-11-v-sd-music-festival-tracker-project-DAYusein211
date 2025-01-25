document.addEventListener("mousemove", function (event) {
    let x = event.clientX / window.innerWidth * 100;
    let y = event.clientY / window.innerHeight * 100;
    document.documentElement.style.setProperty("--cursor-x", x + "%");
    document.documentElement.style.setProperty("--cursor-y", y + "%");

    updateCircleTiltMoveAndScale(event.clientX, event.clientY);
});

