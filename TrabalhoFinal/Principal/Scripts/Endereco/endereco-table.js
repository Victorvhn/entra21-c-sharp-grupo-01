$(function () {
    $.ajax({
        url: "/Endereco/ObterTodosPorJSON",
        method: "get",
        success: function (resultado) {
            var registros = JSON.parse(resultado);
            for (var i = 0; i < registros.length; i++) {
                var id = registros[i].Id;
                var cep = registros[i].Cep;
                var logradouro = registros[i].Logradouro;
                var numero = registros[i].Numero;
                $registro = "<tr>";
                $registro += "<td>" + id + "</td>";
                $registro += "<td>" + cep + "</td>";
                $registro += "<td>" + logradouro + "</td>";
                $registro += "<td>" + numero + "</td>";
                $registro += "<td><a href=\"/Endereco/Editar?id=" + id + "\" >Editar</a></td>";
                $registro += "</tr>";
                $("#table-endereco").append($registro);

            }
        }
    })
});