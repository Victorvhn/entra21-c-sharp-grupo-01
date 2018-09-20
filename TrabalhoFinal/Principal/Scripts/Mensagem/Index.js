$(function () {
    var id = 0.0;

    $('.showChat_inner').toggle('slide', {
        direction: 'right'
    }, 500);

    $("#mensagem-texto").on('keypress', function (evt) {
        if (evt.keyCode == 13) {
            enviarMensagem();
        }
    });

    $("#mensagem-enviar").on("click", function () {
        enviarMensagem();
    });

    function enviarMensagem() {
        id = Math.random();
        $.ajax({
            url: "/mensagem/enviar",
            method: 'post',
            data: {
                mensagem: $("#mensagem-texto").val(),
                id: id
            },
            success: function (result) {
                var resultado = JSON.parse(result);
                $("#pai").append('<div class="media chat-messages">\
                        <div class="media-body chat-menu-reply">\
                            <div class="">\
                                <p class="chat-cont">' + $("#mensagem-texto").val() + '</p >\
                                <p class="chat-time">' + resultado.dataHora + '</p>\
                            </div>\
                        </div>\
                        </div>');
                
                $("#mensagem-texto").val("");
            }
        })
    }

    var pusher = new Pusher('1ba12d6fac4afcac5b77', {
        cluster: 'us2',
        forceTLS: true
    });

    var channel = pusher.subscribe('my-channel');
    channel.bind('my-event', function (data) {
        if (id != data.id) {

        $("#pai").append('<div class="media chat-messages">\
                <div class="media-body chat-menu-content">\
                    <div class="">\
                        <p class="chat-cont">' + data.message + '</p>\
                        <p class="chat-time">' + data.dataHora + '</p>\
                    </div>\
                </div>\
                </div>');
        }
    });
})