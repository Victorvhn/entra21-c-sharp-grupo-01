$(function () {
    $('#form-modal-cadastro-guia').blur(function () {
        var cep = $.trim($('#endereco.Cep').val().replace('-', ''));
        $("#input-custom-field6").focus();

        $.getScript("http://cep.republicavirtual.com.br/web_cep.php?formato=javascript&cep=" + cep, function () {
            if (resultadoCEP["resultado"] == "1") {
                $('#endereco.Logradouro').val(unescape(resultadoCEP["tipo_logradouro"]) + " " + unescape(resultadoCEP["logradouro"]));
                $('#input-address-2').val(unescape(resultadoCEP["bairro"]));
               
                    $.ajax({
                        url: 'index.php?route=account/account/country&country_id=30',
                        dataType: 'json',
                        beforeSend: function () {
                            $('#input-country').after('<span class="wait">&nbsp;<img src="catalog/view/theme/graveagudo2012/image/loading.gif" alt="" /></span>');
                        },
                        complete: function () {
                            $('.wait').remove();
                        },
                        success: function (json) {
                            if (json['postcode_required'] == '1') {
                                $('input[name=\'postcode\']').parent().parent().addClass('required');
                            } else {
                                $('input[name=\'postcode\']').parent().parent().removeClass('required');
                            }

                            var html = '<option value=""><?php echo $text_select; ?></option>';

                            if (json['zone'] != '') {
                                for (i = 0; i < json['zone'].length; i++) {
                                    html += '<option value="' + json['zone'][i]['zone_id'] + '"';

                                    if (json['zone'][i]['zone_id'] == zone_id) {
                                        html += ' selected="selected"';
                                    }

                                    html += '>' + json['zone'][i]['name'] + '</option>';
                                }
                            } else {
                                html += '<option value="0" selected="selected"><?php echo $text_none; ?></option>';
                            }

                            $('#input-zone').html(html);
                        }
                    });
                });
            }
        });
    });

});	