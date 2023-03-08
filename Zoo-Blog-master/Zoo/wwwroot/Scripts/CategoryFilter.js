document.addEventListener("DOMContentLoaded", () => {
    let animalsContainers;
    let categoryButtons = document.querySelectorAll("button.btn.btn-warning.m-2");
    function getAnimals() {
        animalsContainers = document.querySelectorAll("div.container.animal");
    }
    categoryButtons.forEach((a) =>
        a.addEventListener("click", (event) => {
            filter(a.innerText);
        })
    );

    function filter(category) {
        getAnimals();
        animalsContainers.forEach((a) => {
            if (category === "All categories") {
                a.classList.remove("d-none");
            } else if (!a.classList.contains(category)) {
                a.classList.add("d-none");
            } else {
                a.classList.remove("d-none");
            }
        });
    }
});
