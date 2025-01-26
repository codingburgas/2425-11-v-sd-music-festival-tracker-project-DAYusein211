document.addEventListener("mousemove", function (event) {
    let x = event.clientX / window.innerWidth * 100;
    let y = event.clientY / window.innerHeight * 100;
    document.documentElement.style.setProperty("--cursor-x", x + "%");
    document.documentElement.style.setProperty("--cursor-y", y + "%");
    
});
let currentX = 50; // Default gradient start X
let currentY = 50; // Default gradient start Y
const smoothingFactor = 0.1; // Adjust for smoother effect

document.addEventListener("mousemove", function (event) {
    let x = (event.clientX / window.innerWidth) * 100;  // Convert to percentage
    let y = (event.clientY / window.innerHeight) * 100;

    // Smooth transition effect
    currentX = currentX * (1 - smoothingFactor) + x * smoothingFactor;
    currentY = currentY * (1 - smoothingFactor) + y * smoothingFactor;

    // Apply smooth shifting to CSS variables
    document.documentElement.style.setProperty("--gradient-x", `${currentX}%`);
    document.documentElement.style.setProperty("--gradient-y", `${currentY}%`);
});
