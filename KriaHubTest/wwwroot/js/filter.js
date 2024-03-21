document.getElementById('filtroNome').addEventListener('input', function () {
    var filtro = this.value.toLowerCase();
    var repositorios = document.querySelectorAll('.repo');

    repositorios.forEach(function (repo) {
        var nomeRepositorio = repo.querySelector('.repo-info a').innerText.toLowerCase();

        if (nomeRepositorio.includes(filtro)) {
            repo.style.display = 'block';
        } else {
            repo.style.display = 'none';
        }
    });
});