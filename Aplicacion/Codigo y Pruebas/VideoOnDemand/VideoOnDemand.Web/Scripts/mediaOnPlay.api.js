function cargarModal(id) {
    $('#auxId').val(id);
    consultarMediaOnPlay();
}

function cerrarModal() {
    var value = document.getElementById('auxId').value;
    agregar(value);
}

function agregar(id) {
    var url = $("#hdnUrlAgregarOnPlay").val();
    var vid = document.getElementById("videoOnPlay");
    var time = (vid.currentTime) * 1000;

    $('#videoOnPlay').trigger('pause');

    var mediaOnPlay = { MediaId: id, Milisegundo: time };
    $.ajax({
        url: url,
        datatype: 'JSON',
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify(mediaOnPlay),
        success: function (data) {
            if (data.Success) {
            }
            else { }


        }
    });
}

function consultarMediaOnPlay() {
    var url = $("#hdnUrlGetOnPlay").val();
    var id = $("#auxId").val();
    console.log(url + "/" + id);
    $.ajax({
        url: url + "/" + id,
        type: 'GET',
        dataType: 'JSON',
        data: null,
        success: function (data) {
            if (data.Success) {
                var playing = JSON.parse(data.MediaOnPlay);
                var time = (playing.Milisegundo) / 1000;
                ajustarVideo(time);
            }

        }
    });
}

function ajustarVideo(time) {
    var vid = document.getElementById("videoOnPlay");
    vid.currentTime = time;
}
