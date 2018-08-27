$(document).ready(function () {
    $('#select-cadastro-pais-continente').select2({
        ajax: {
            url: '/Continente/ObterTodosPorJSONToSelect2',
            dataType: 'json',
        }
    });
    /* FALOW VLW
    $.ajax({
        type: "post",
        url: "/Pais/ObterTodosPorJSONSemStart",
        data: { continente: $("#select-cadastro-pais-continente").val() },
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (obj) {
            if (obj != null) {
                var data = obj.data;
                var selectbox = $('#select-cadastro-pais-continente');
                selectbox.find('option').remove();
                $.each(data, function (i, d) {
                    $('<option>').val(d.Id).text(d.Nome).appendTo(selectbox);
                });
            }
        }
    });*/
});
