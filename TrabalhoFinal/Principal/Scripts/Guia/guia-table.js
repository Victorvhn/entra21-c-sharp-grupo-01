$(function () {
    $.ajax({
        url: "/Guia/ObterTodosPorJSON",
        method: "get",
        success: function (resultado) {
            var registros = JSON.parse(resultado);
            for (var i = 0; i < registros.length; i++) {
                var id = registros[i].Id;
                var nome = registros[i].Nome;
                var sobrenome = registros[i].Sobrenome;
                var dataNascimento = registros[i].DataNascimento;
                var cpf = registros[i].Cpf;
                var rank = registros[i].Rank;
                $registro = "<tr>";
                $registro += "<td>" + id + "</td>";
                $registro += "<td>" + nome + "</td>";
                $registro += "<td>" + sobrenome + "</td>";
                $registro += "<td>" + dataNascimento + "</td>";
                $registro += "<td>" + cpf + "</td>";
                $registro += "<td>" + rank + "</td>";
                $registro += "<td><a href=\"/Guia/Editar?id=" + id + "\" >Editar</a></td>";
                $registro += "</tr>";
                $("#table-guia").append($registro);
            }

        }
    })
});