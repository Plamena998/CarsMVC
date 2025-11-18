
//function openCarModal(make, model, year, carClass, drive, fuel, transmission, imageUrl) {

//    document.getElementById("modalTitle").innerText = `${make} ${model}`;
//    document.getElementById("modalImage").src = imageUrl;
//    document.getElementById("modalDetails").innerHTML = `
//    <p><strong>Year:</strong> ${year}</p>
//    <p><strong>Class:</strong> ${carClass}</p>
//    <p><strong>Drive:</strong> ${drive}</p>
//    <p><strong>Fuel:</strong> ${fuel}</p>
//    <p><strong>Transmission:</strong> ${transmission}</p>`;
//    const modal = document.getElementById("carModal");
//    modal.style.display = "flex";
//    setTimeout(() => modal.classList.add("show"), 10);
//}

//function closeCarModal() {
//    const modal = document.getElementById("carModal");
//    modal.classList.remove("show");
//    setTimeout(() => { modal.style.display = "none"; }, 300);
//}

//window.onclick = function (event) {
//    const modal = document.getElementById("carModal");
//    if (event.target === modal) {
//        closeCarModal();
//    }
//}