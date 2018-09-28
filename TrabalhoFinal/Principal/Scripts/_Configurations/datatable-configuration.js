window.dataTableLanguage = {
    "sEmptyTable": "Nenhum registro encontrado",
    "sInfo": "Mostrando página _PAGE_ de _PAGES_",
    "sInfoEmpty": "Nenhum registro disponível",
    "sInfoFiltered": "(Filtrados de _MAX_ registros)",
    "sInfoPostFix": "",
    "sInfoThousands": ".",
    "sLengthMenu": '<span>Apresentar:</span> _MENU_',
    "sLoadingRecords": "Carregando...",
    "sZeroRecords": "Nenhum registro encontrado",
    "sSearch": "Pesquisa:  ",
    "oPaginate": {
        "sNext": "Próximo",
        "sPrevious": "Anterior",
        "sFirst": "Primeiro",
        "sLast": "Último"
    },
    "oAria": {
        "sSortAscending": ": Ordenar colunas de forma ascendente",
        "sSortDescending": ": Ordenar colunas de forma descendente"
    },
    "processing": '   <div class="loader-full">\
        <div class="preloader3 loader-block loader-interno">\
        <div class= "circ1 loader-primary" ></div>\
            <div class="circ2 loader-primary"></div>\
            <div class="circ3 loader-primary"></div>\
            <div class="circ4 loader-primary"></div>\
                                                                </div>\
                                                                </div>'
};

$(function () {
    $.extend($.fn.dataTable.defaults, {
        language: window.dataTableLanguage,
        responsive: true,
        autoWidth: false,
        processing: true,
        serverSide: true
    });
});