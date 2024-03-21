document.querySelectorAll('.close-alert').forEach(function (element) {
    element.addEventListener('click', function () {
        document.querySelectorAll('.alert').forEach(function (alertElement) {
            alertElement.style.display = 'none';
        });
    });
});

function limparHistoricoNavegacao() {
    window.history.pushState({}, document.title, "/");
}