$(function () {
    $.ajax({
        url: "/Continente/ObterTodosPorJSON",
        method: "get",
        success: function (resultado) {
            var registros = JSON.parse(resultado);
            for (var i = 0; i < registros.length; i++) {
                var id = registros[i].Id;
                var nome = registros[i].Nome;
                $registro = "<tr>";
                $registro += "<td>" + id + "</td>";
                $registro += "<td>" + nome + "</td>";
                $registro += "<td><a href=\"/Continente/Editar?id=" + id + "\" >Editar</a></td>";
                $registro += "</tr>";
                $("#table-continente").append($registro);
            }
        }
    })
});